using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilkFlo.Web.Models.Business
{
    public partial class Idea
    {
        public Data.Core.IUnitOfWork UnitOfWork { get; set; }

        public decimal? GetHumanAutomationPotentialAfter(decimal? automationPotential)
        {
            if (automationPotential == null || automationPotential < 0)
                automationPotential = 0;

            if (automationPotential > 100)
                automationPotential = 100;


            return 100 - automationPotential ?? 0;
        }




        public decimal? GetVolumePerYear(decimal? automationPotential,
                                         string automationType = "")
        {
            decimal? humanVolumePerYear = null;

            if (ActivityVolumeAverage == null)
                return null;

            if (string.IsNullOrWhiteSpace(automationType))
                automationType = Data.Core.Enumerators.AutomationType.Unattended.ToString();


            if (automationPotential == null || automationPotential < 0)
                automationPotential = 0;

            if (automationPotential > 100)
                automationPotential = 100;

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.BiAnnually.ToString())
            {
                // Bi-annually
                humanVolumePerYear = ActivityVolumeAverage * 2;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Quarterly.ToString())
            {
                // Quarterly
                humanVolumePerYear = ActivityVolumeAverage * 4;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Monthly.ToString())
            {
                // Monthly
                humanVolumePerYear = ActivityVolumeAverage * 12;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Weekly.ToString())
            {
                // Weekly
                humanVolumePerYear = ActivityVolumeAverage * 52;
            }

            if (TaskFrequencyId == Data.Core.Enumerators.TaskFrequency.Daily.ToString())
            {
                // Daily
                if (AverageWorkingDay == null)
                    humanVolumePerYear = 0;
                else
                    humanVolumePerYear = ActivityVolumeAverage * (decimal)AverageWorkingDay;
            }

            decimal value = humanVolumePerYear ?? 0;
            if (automationType == Data.Core.Enumerators.AutomationType.Unattended.ToString()
                && automationPotential != 100)
                value = value / 100 * automationPotential??0;

            return value;
        }

        public decimal? GetVolumePerMonth(decimal? automationPotential)
        {
            return GetVolumePerYear(automationPotential) / 12;
        }





        public decimal? HumanTotalHoursPerYearBeforeValue => GetTotalTimeNeededToPerformWorkWithoutAutomation();
        public string HumanTotalHoursPerYearBefore => HumanTotalHoursPerYearBeforeValue?.ToString("#,##0.00");


        public decimal? GetTotalHoursPerYearAfter(decimal? automationPotential)
        {
            if (automationPotential == null || automationPotential < 0)
                automationPotential = 0;

            if (automationPotential > 100)
                automationPotential = 100;


            HumanTotalHoursPerYearAfterValue = GetTotalTimeNeededToPerformWorkWithoutAutomation() / 100 * automationPotential;
            HumanTotalHoursPerYearAfter = HumanTotalHoursPerYearAfterValue?.ToString("#,##0.00");

            return HumanTotalHoursPerYearAfterValue;
        }

        public decimal? HumanTotalHoursPerYearAfterValue { get; private set; }
        public string HumanTotalHoursPerYearAfter { get; private set; }

        public decimal GetHumanRunningCostYear(decimal? automationPotential)
        {
            if (automationPotential == null || automationPotential < 0)
                automationPotential = 0;

            if (automationPotential > 100)
                automationPotential = 100;


            decimal value = GetCostPerYearForProcessBeforeAutomation();


            value = value / 100 * automationPotential??0;

            return value;
        }

        public decimal GetHumanRunningCostMonth(decimal? automationPotential)
        {
            return GetHumanRunningCostYear(automationPotential) / 12;
        }

        public decimal? GetHumanRunningCostTransaction(decimal? automationPotential)
        {
            var cost = GetHumanRunningCostYear(automationPotential);
            var volume = GetVolumePerYear(100);

            if (volume == null || volume == 0)
                return cost;

            return cost / volume;
        }



        /*
            // Required:
            IdeaOtherRunningCosts
            IdeaOtherRunningCosts[0..n].OtherRunningCost

            // Run this code if getting the data from the database.
            await _unitOfWork.BusinessIdeaOtherRunningCosts.GetForIdeaAsync(model.GetCore());
            await _unitOfWork.BusinessOtherRunningCosts.GetOtherRunningCostForAsync(model.GetCore().IdeaOtherRunningCosts);
         */
        public void GetOtherRunningCostPerYearAndPerMonthAndTransaction()
        {
            decimal totalYear = 0;
            decimal totalMonth = 0;


            foreach (var ideaOtherRunningCost in IdeaOtherRunningCosts)
            {
                if (!ideaOtherRunningCost.OtherRunningCost.IsLive)
                    continue;

                decimal rowTotalYear = ideaOtherRunningCost.OtherRunningCost.Cost;
                decimal rowTotalMonth = ideaOtherRunningCost.OtherRunningCost.Cost;
                if (ideaOtherRunningCost.OtherRunningCost.FrequencyId == "Monthly")
                    rowTotalYear *= 12;
                else
                    rowTotalMonth /= 12;

                totalYear += rowTotalYear;
                totalMonth += rowTotalMonth;
            }

            OtherRunningCostPerYearValue = totalYear;
            OtherRunningCostPerMonthValue = totalMonth;
        }

        public static decimal GetOtherRunningCostTransaction(
            Models.Business.Idea idea,
            decimal otherRunningCostPerYearTotal)
        {
            var processVolumetryPerYear = idea.GetVolumePerYear(100)??0;

            if (processVolumetryPerYear == 0)
                return 0;

            return otherRunningCostPerYearTotal / processVolumetryPerYear;
        }


        public decimal OtherRunningCostPerYearValue { get; private set; }
        public string OtherRunningCostPerYear => OtherRunningCostPerYearValue.ToString("#,##0.00");

        public decimal OtherRunningCostPerMonthValue { get; private set; }
        public string OtherRunningCostPerMonth => OtherRunningCostPerMonthValue.ToString("#,##0.00");


        public void UpdateAHTRobotPerTransaction()
        {
            if (AHTRobot != null)
            {
                // The user has manually applied a value to RobotSpeedMultiplier
                // ∴ do not calculate.
                return;
            }

            AHTRobot = GetAverageHandlingTimeEmployeePerTransaction();

            if (RobotSpeedMultiplier != null
                && RobotSpeedMultiplier != 0)
                AHTRobot /= RobotSpeedMultiplier;
        }

        public decimal? GetTotalFTR(string automationTypeId, int employeeCount)
        {
            if (automationTypeId == Data.Core.Enumerators.AutomationType.Attended.ToString())
                return employeeCount;

            if (automationTypeId == Data.Core.Enumerators.AutomationType.Unattended.ToString())
                return 0;

            return null;
        }

        public decimal? GetTotalFTEAfter(string automationTypeId, decimal? automationPotential = 100)
        {
            if (automationTypeId == Data.Core.Enumerators.AutomationType.Attended.ToString())
                return EmployeeCount;

            if (automationTypeId == Data.Core.Enumerators.AutomationType.Unattended.ToString())
            {
                if (automationPotential == null || automationPotential < 0)
                    automationPotential = 0;

                if (automationPotential > 100)
                    automationPotential = 100;

                var value = GetVolumePerYear(100) * automationPotential * AHTRobot
                            / 100 / RobotWorkDayYear / RobotWorkHourDay / 60;
                return value;
            }

            return null;
        }

        public string GetTotalHoursPerYear(string automationTypeId, decimal? automationPotential = 100)
        {
            if (automationTypeId == Data.Core.Enumerators.AutomationType.Attended.ToString())
                return "";

            if (automationTypeId == Data.Core.Enumerators.AutomationType.Unattended.ToString())
            {
                if (automationPotential == null || automationPotential < 0)
                    automationPotential = 0;

                if (automationPotential > 100)
                    automationPotential = 100;

                var value = GetVolumePerYear(100) * automationPotential * AHTRobot
                            / 100 / 60;
                return value?.ToString("#,##0.00");
            }

            return "";
        }
    }
}
