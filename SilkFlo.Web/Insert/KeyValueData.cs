using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SilkFlo.Web.Insert
{
    public class KeyValueData
    {
        public static async Task SharedCostTypeAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.CostType.Infrastructure.ToString();
            var name = "Infrastructure";
            var core = await unitOfWork.SharedCostTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.CostType { Id = id, Name = name };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.CostType.Other.ToString();
            name = "Other";
            core = await unitOfWork.SharedCostTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.CostType { Id = id, Name = name };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.CostType.SoftwareLicence.ToString();
            name = "Software Licence";
            core = await unitOfWork.SharedCostTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.CostType { Id = id, Name = name };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.CostType.Support.ToString();
            name = "Support";
            core = await unitOfWork.SharedCostTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.CostType { Id = id, Name = name };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }


        public static async Task SharedAutomationTypeAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.AutomationType.Attended.ToString();
            var name = "Attended";
            var core = await unitOfWork.SharedAutomationTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AutomationType { Id = id, Name = name };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.AutomationType.Unattended.ToString();
            name = "Unattended";
            core = await unitOfWork.SharedAutomationTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AutomationType { Id = id, Name = name };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }


        public static async Task SharedPeriodAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.Period.Annual.ToString();
            var name = Data.Core.Enumerators.Period.Annual.ToString();
            var core = await unitOfWork.SharedPeriods.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Period { Id = id, Name = name, NamePlural = "Yearly", MonthCount = 12, CancelPeriodInDay = 30, CancelPeriodInDayNoRenew = 90 };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.Period.Monthly.ToString();
            name = Data.Core.Enumerators.Period.Monthly.ToString();
            core = await unitOfWork.SharedPeriods.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Period { Id = id, Name = name, NamePlural = "Monthly", MonthCount = 1, CancelPeriodInDay = 14, CancelPeriodInDayNoRenew = 90 };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }





        public static async Task SharedIndustriesAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            string id = "AdvertisingMedia";
            var core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Advertising, Media" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "AgricultureForestryFishing";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Agriculture, Forestry & Fishing" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "CallCentreCustomerServices";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Call Centre & Customer Services" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "CommunitySport";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Community & Sport" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "ConstructionCivilEngineering";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Construction & Civil Engineering" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "ConsultingCorporateStrategy";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Consulting & Corporate Strategy" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "DefenceMilitaryArmedForces";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Defence, Military & Armed Forces" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Education";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Education" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Engineering";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Engineering" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Accounting";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Accounting" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Banking";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Banking" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Finance";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Finance" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "FoodHospitality";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Food & Hospitality" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "HealthcareMedical";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Healthcare & Medical" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "HRRecruitment";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "HR & Recruitment" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "ITTelecommunications";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "IT & Telecommunications" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Legal";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Legal" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Manufacturing";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Manufacturing" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "MiningEnergyOilGas";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Mining, Energy, Oil & Gas" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "OfficeAdministration";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Office & Administration" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Real EstateProperty";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Real Estate & Property" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "RetailConsumerProducts";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Retail & Consumer Products" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "SalesMarketing";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Sales & Marketing" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "ScienceBiotechPharmaceuticals";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Science, Bio-Tech & Pharmaceuticals" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "SoftwareEngineering";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Software Engineering" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }



            id = "TransportLogistics";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Transport & Logistics" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "TravelTourism";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Travel & Tourism" };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }


            id = "Other";
            core = await unitOfWork.SharedIndustries.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Industry { Id = id, Name = "Other", Sort = 1 };
                await unitOfWork.SharedIndustries.AddAsync(core);
                core.Id = id;
            }
        }


        //Client Administrator
        public static async Task AverageNumberOfStepAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "MoreThan150Steps";
            string name = "More than 150 steps";
            string shortName = "> 150";
            decimal weighting = 0;
            var core = await unitOfWork.SharedAverageNumberOfSteps.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AverageNumberOfStep { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)", ShortName = shortName };
                await unitOfWork.SharedAverageNumberOfSteps.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Between100_150Steps";
            name = "Between 100-150 steps";
            shortName = "100-150";
            weighting = 0.25m;
            core = await unitOfWork.SharedAverageNumberOfSteps.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AverageNumberOfStep { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", ShortName = shortName };
                await unitOfWork.SharedAverageNumberOfSteps.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Between50_100Steps";
            name = "Between 50-100 steps";
            shortName = "50-100";
            weighting = 0.5m;
            core = await unitOfWork.SharedAverageNumberOfSteps.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AverageNumberOfStep { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", ShortName = shortName };
                await unitOfWork.SharedAverageNumberOfSteps.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id="Between20_50Steps";
            name = "Between 20-50 steps";
            shortName = "20-50";
            weighting = 0.75m;
            core = await unitOfWork.SharedAverageNumberOfSteps.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AverageNumberOfStep { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", ShortName = shortName };
                await unitOfWork.SharedAverageNumberOfSteps.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "LessThan20Steps";
            name = "Less than 20 steps";
            shortName = "< 20";
            weighting = 1;
            core = await unitOfWork.SharedAverageNumberOfSteps.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AverageNumberOfStep { Name = name, Weighting = weighting, Colour = "var(--slider-colour4)", ShortName = shortName };
                await unitOfWork.SharedAverageNumberOfSteps.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }


        public static async Task ApplicationStabilityAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "SignificantChange";
            var name = "Significant change";
            decimal weighting = 0;
            var rating = 1;
            var core = await unitOfWork.SharedApplicationStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ApplicationStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)", Rating = rating };
                await unitOfWork.SharedApplicationStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "MediumChange";
            name = "Medium change";
            weighting = 0.25m;
            rating = 2;
            core = await unitOfWork.SharedApplicationStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ApplicationStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", Rating = rating };
                await unitOfWork.SharedApplicationStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "SomeChange";
            name = "Some change";
            weighting = 0.5m;
            rating = 3;
            core = await unitOfWork.SharedApplicationStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ApplicationStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", Rating = rating };
                await unitOfWork.SharedApplicationStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "SmallChange";
            name = "Small change";
            weighting = 0.75m;
            rating = 4;
            core = await unitOfWork.SharedApplicationStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ApplicationStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", Rating = rating };
                await unitOfWork.SharedApplicationStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "NoChangeExpected";
            name = "No change expected";
            weighting = 1;
            rating = 5;
            core = await unitOfWork.SharedApplicationStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ApplicationStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour4)", Rating = rating };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task AutomationGoalAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "Cost";
            var name = "Cost";
            var sort = 0;
            var core = await unitOfWork.SharedAutomationGoals.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AutomationGoal { Name = name, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "Quality";
            name = "Quality";
            sort++;
            core = await unitOfWork.SharedAutomationGoals.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AutomationGoal { Name = name, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "Productivity";
            name = "Productivity";
            sort++;
            core = await unitOfWork.SharedAutomationGoals.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AutomationGoal { Name = name, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "EmployeeSatisfaction";
            name = "Employee Satisfaction";
            sort++;
            core = await unitOfWork.SharedAutomationGoals.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AutomationGoal { Name = name, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "CustomerSatisfaction";
            name = "Customer Satisfaction";
            sort++;
            core = await unitOfWork.SharedAutomationGoals.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.AutomationGoal { Name = name, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task CountriesAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            await CountryAsync("Afghanistan", unitOfWork, 0);
            await CountryAsync("Aland Islands", unitOfWork, 0);
            await CountryAsync("Albania", unitOfWork, 0);
            await CountryAsync("Algeria", unitOfWork, 0);
            await CountryAsync("American Samoa", unitOfWork, 0);
            await CountryAsync("Andorra", unitOfWork, 0);
            await CountryAsync("Angola", unitOfWork, 0);
            await CountryAsync("Anguilla", unitOfWork, 0);
            await CountryAsync("Antarctica", unitOfWork, 0);
            await CountryAsync("Antigua And Barbuda", unitOfWork, 0);
            await CountryAsync("Argentina", unitOfWork, 0);
            await CountryAsync("Armenia", unitOfWork, 0);
            await CountryAsync("Aruba", unitOfWork, 0);
            await CountryAsync("Australia", unitOfWork, 0);
            await CountryAsync("Austria", unitOfWork, 0);
            await CountryAsync("Azerbaijan", unitOfWork, 0);
            await CountryAsync("Bahamas", unitOfWork, 0);
            await CountryAsync("Bahrain", unitOfWork, 0);
            await CountryAsync("Bangladesh", unitOfWork, 0);
            await CountryAsync("Barbados", unitOfWork, 0);
            await CountryAsync("Belarus", unitOfWork, 0);
            await CountryAsync("Belgium", unitOfWork, 0);
            await CountryAsync("Belize", unitOfWork, 0);
            await CountryAsync("Benin", unitOfWork, 0);
            await CountryAsync("Bermuda", unitOfWork, 0);
            await CountryAsync("Bhutan", unitOfWork, 0);
            await CountryAsync("Bolivia", unitOfWork, 0);
            await CountryAsync("Bosnia and Herzegovina", unitOfWork, 0);
            await CountryAsync("Botswana", unitOfWork, 0);
            await CountryAsync("Brazil", unitOfWork, 0);
            await CountryAsync("British Indian Ocean Territory", unitOfWork, 0);
            await CountryAsync("Brunei Darussalam", unitOfWork, 0);
            await CountryAsync("Bulgaria", unitOfWork, 0);
            await CountryAsync("Burkina Faso", unitOfWork, 0);
            await CountryAsync("Burundi", unitOfWork, 0);
            await CountryAsync("Cambodia", unitOfWork, 0);
            await CountryAsync("Cameroon", unitOfWork, 0);
            await CountryAsync("Canada", unitOfWork, 0);
            await CountryAsync("Cape Verde", unitOfWork, 0);
            await CountryAsync("Cayman Islands", unitOfWork, 0);
            await CountryAsync("Central African Republic", unitOfWork, 0);
            await CountryAsync("Chad", unitOfWork, 0);
            await CountryAsync("Chile", unitOfWork, 0);
            await CountryAsync("China", unitOfWork, 0);
            await CountryAsync("Christmas Island", unitOfWork, 0);
            await CountryAsync("Cocos (Keeling) Islands", unitOfWork, 0);
            await CountryAsync("Colombia", unitOfWork, 0);
            await CountryAsync("Comoros", unitOfWork, 0);
            await CountryAsync("Congo", unitOfWork, 0);
            await CountryAsync("Congo, the Democratic Republic of the", unitOfWork, 0);
            await CountryAsync("Cook Islands", unitOfWork, 0);
            await CountryAsync("Costa Rica", unitOfWork, 0);
            await CountryAsync("Cote d'Ivoire", unitOfWork, 0);
            await CountryAsync("Croatia", unitOfWork, 0);
            await CountryAsync("Curacao", unitOfWork, 0);
            await CountryAsync("Cyprus", unitOfWork, 0);
            await CountryAsync("Czech Republic", unitOfWork, 0);
            await CountryAsync("Denmark", unitOfWork, 0);
            await CountryAsync("Djibouti", unitOfWork, 0);
            await CountryAsync("Dominica", unitOfWork, 0);
            await CountryAsync("Dominican Republic", unitOfWork, 0);
            await CountryAsync("Ecuador", unitOfWork, 0);
            await CountryAsync("Egypt", unitOfWork, 0);
            await CountryAsync("El Salvador", unitOfWork, 0);
            await CountryAsync("Equatorial Guinea", unitOfWork, 0);
            await CountryAsync("Eritrea", unitOfWork, 0);
            await CountryAsync("Estonia", unitOfWork, 0);
            await CountryAsync("Ethiopia", unitOfWork, 0);
            await CountryAsync("Falkland Islands (Malvinas)", unitOfWork, 0);
            await CountryAsync("Faroe Islands", unitOfWork, 0);
            await CountryAsync("Fiji", unitOfWork, 0);
            await CountryAsync("Finland", unitOfWork, 0);
            await CountryAsync("France", unitOfWork, 0);
            await CountryAsync("French Guiana", unitOfWork, 0);
            await CountryAsync("French Polynesia", unitOfWork, 0);
            await CountryAsync("French Southern Territories", unitOfWork, 0);
            await CountryAsync("Gabon", unitOfWork, 0);
            await CountryAsync("Gambia", unitOfWork, 0);
            await CountryAsync("Georgia", unitOfWork, 0);
            await CountryAsync("Germany", unitOfWork, 0);
            await CountryAsync("Ghana", unitOfWork, 0);
            await CountryAsync("Gibraltar", unitOfWork, 0);
            await CountryAsync("Greece", unitOfWork, 0);
            await CountryAsync("Greenland", unitOfWork, 0);
            await CountryAsync("Grenada", unitOfWork, 0);
            await CountryAsync("Guadeloupe", unitOfWork, 0);
            await CountryAsync("Guam", unitOfWork, 0);
            await CountryAsync("Guatemala", unitOfWork, 0);
            await CountryAsync("Guernsey", unitOfWork, 0);
            await CountryAsync("Guinea", unitOfWork, 0);
            await CountryAsync("Guinea-Bissau", unitOfWork, 0);
            await CountryAsync("Guyana", unitOfWork, 0);
            await CountryAsync("Haiti", unitOfWork, 0);
            await CountryAsync("Heard Island and McDonald Islands", unitOfWork, 0);
            await CountryAsync("Holy See (Vatican City State)", unitOfWork, 0);
            await CountryAsync("Honduras", unitOfWork, 0);
            await CountryAsync("Hong Kong", unitOfWork, 0);
            await CountryAsync("Hungary", unitOfWork, 0);
            await CountryAsync("Iceland", unitOfWork, 0);
            await CountryAsync("India", unitOfWork, 0);
            await CountryAsync("Indonesia", unitOfWork, 0);
            await CountryAsync("Iraq", unitOfWork, 0);
            await CountryAsync("Ireland", unitOfWork, 0);
            await CountryAsync("Isle of Man", unitOfWork, 0);
            await CountryAsync("Israel", unitOfWork, 0);
            await CountryAsync("Italy", unitOfWork, 0);
            await CountryAsync("Jamaica", unitOfWork, 0);
            await CountryAsync("Japan", unitOfWork, 0);
            await CountryAsync("Jersey", unitOfWork, 0);
            await CountryAsync("Jordan", unitOfWork, 0);
            await CountryAsync("Kazakhstan", unitOfWork, 0);
            await CountryAsync("Kenya", unitOfWork, 0);
            await CountryAsync("Kiribati", unitOfWork, 0);
            await CountryAsync("Korea, Republic of", unitOfWork, 0);
            await CountryAsync("Kosovo", unitOfWork, 0);
            await CountryAsync("Kuwait", unitOfWork, 0);
            await CountryAsync("Kyrgyzstan", unitOfWork, 0);
            await CountryAsync("Lao People's Democratic Republic", unitOfWork, 0);
            await CountryAsync("Latvia", unitOfWork, 0);
            await CountryAsync("Lebanon", unitOfWork, 0);
            await CountryAsync("Lesotho", unitOfWork, 0);
            await CountryAsync("Liberia", unitOfWork, 0);
            await CountryAsync("Libyan Arab Jamahiriya", unitOfWork, 0);
            await CountryAsync("Liechtenstein", unitOfWork, 0);
            await CountryAsync("Lithuania", unitOfWork, 0);
            await CountryAsync("Luxembourg", unitOfWork, 0);
            await CountryAsync("Macao", unitOfWork, 0);
            await CountryAsync("Macedonia, the former Yugoslav Republic of", unitOfWork, 0);
            await CountryAsync("Madagascar", unitOfWork, 0);
            await CountryAsync("Malawi", unitOfWork, 0);
            await CountryAsync("Malaysia", unitOfWork, 0);
            await CountryAsync("Maldives", unitOfWork, 0);
            await CountryAsync("Mali", unitOfWork, 0);
            await CountryAsync("Malta", unitOfWork, 0);
            await CountryAsync("Marshall Islands", unitOfWork, 0);
            await CountryAsync("Martinique", unitOfWork, 0);
            await CountryAsync("Mauritania", unitOfWork, 0);
            await CountryAsync("Mauritius", unitOfWork, 0);
            await CountryAsync("Mayotte", unitOfWork, 0);
            await CountryAsync("Mexico", unitOfWork, 0);
            await CountryAsync("Micronesia, Federated States of", unitOfWork, 0);
            await CountryAsync("Moldova, Republic of", unitOfWork, 0);
            await CountryAsync("Monaco", unitOfWork, 0);
            await CountryAsync("Mongolia", unitOfWork, 0);
            await CountryAsync("Montenegro", unitOfWork, 0);
            await CountryAsync("Montserrat", unitOfWork, 0);
            await CountryAsync("Morocco", unitOfWork, 0);
            await CountryAsync("Mozambique", unitOfWork, 0);
            await CountryAsync("Myanmar", unitOfWork, 0);
            await CountryAsync("Namibia", unitOfWork, 0);
            await CountryAsync("Nauru", unitOfWork, 0);
            await CountryAsync("Nepal", unitOfWork, 0);
            await CountryAsync("Netherlands", unitOfWork, 0);
            await CountryAsync("Netherlands Antilles", unitOfWork, 0);
            await CountryAsync("New Caledonia", unitOfWork, 0);
            await CountryAsync("New Zealand", unitOfWork, 0);
            await CountryAsync("Nicaragua", unitOfWork, 0);
            await CountryAsync("Niger", unitOfWork, 0);
            await CountryAsync("Nigeria", unitOfWork, 0);
            await CountryAsync("Niue", unitOfWork, 0);
            await CountryAsync("Norfolk Island", unitOfWork, 0);
            await CountryAsync("Northern Mariana Islands", unitOfWork, 0);
            await CountryAsync("Norway", unitOfWork, 0);
            await CountryAsync("Oman", unitOfWork, 0);
            await CountryAsync("Pakistan", unitOfWork, 0);
            await CountryAsync("Palau", unitOfWork, 0);
            await CountryAsync("Palestinian Territory, Occupied", unitOfWork, 0);
            await CountryAsync("Panama", unitOfWork, 0);
            await CountryAsync("Papua New Guinea", unitOfWork, 0);
            await CountryAsync("Paraguay", unitOfWork, 0);
            await CountryAsync("Peru", unitOfWork, 0);
            await CountryAsync("Philippines", unitOfWork, 0);
            await CountryAsync("Pitcairn", unitOfWork, 0);
            await CountryAsync("Poland", unitOfWork, 0);
            await CountryAsync("Portugal", unitOfWork, 0);
            await CountryAsync("Puerto Rico", unitOfWork, 0);
            await CountryAsync("Qatar", unitOfWork, 0);
            await CountryAsync("Reunion", unitOfWork, 0);
            await CountryAsync("Romania", unitOfWork, 0);
            await CountryAsync("Russian Federation", unitOfWork, 0);
            await CountryAsync("Rwanda", unitOfWork, 0);
            await CountryAsync("Saint Barthelemy", unitOfWork, 0);
            await CountryAsync("Saint Helena", unitOfWork, 0);
            await CountryAsync("Saint Kitts and Nevis", unitOfWork, 0);
            await CountryAsync("Saint Lucia", unitOfWork, 0);
            await CountryAsync("Saint Martin (French part)", unitOfWork, 0);
            await CountryAsync("Saint Pierre and Miquelon", unitOfWork, 0);
            await CountryAsync("Saint Vincent and the Grenadines", unitOfWork, 0);
            await CountryAsync("Samoa", unitOfWork, 0);
            await CountryAsync("San Marino", unitOfWork, 0);
            await CountryAsync("Sao Tome and Principe", unitOfWork, 0);
            await CountryAsync("Saudi Arabia", unitOfWork, 0);
            await CountryAsync("Senegal", unitOfWork, 0);
            await CountryAsync("Serbia", unitOfWork, 0);
            await CountryAsync("Seychelles", unitOfWork, 0);
            await CountryAsync("Sierra Leone", unitOfWork, 0);
            await CountryAsync("Singapore", unitOfWork, 0);
            await CountryAsync("Sint Maarten", unitOfWork, 0);
            await CountryAsync("Slovakia", unitOfWork, 0);
            await CountryAsync("Slovenia", unitOfWork, 0);
            await CountryAsync("Solomon Islands", unitOfWork, 0);
            await CountryAsync("Somalia", unitOfWork, 0);
            await CountryAsync("South Africa", unitOfWork, 0);
            await CountryAsync("South Georgia and the South Sandwich Islands", unitOfWork, 0);
            await CountryAsync("South Sudan", unitOfWork, 0);
            await CountryAsync("Spain", unitOfWork, 0);
            await CountryAsync("Sri Lanka", unitOfWork, 0);
            await CountryAsync("Suriname", unitOfWork, 0);
            await CountryAsync("Svalbard and Jan Mayen", unitOfWork, 0);
            await CountryAsync("Swaziland", unitOfWork, 0);
            await CountryAsync("Sweden", unitOfWork, 0);
            await CountryAsync("Switzerland", unitOfWork, 0);
            await CountryAsync("Taiwan", unitOfWork, 0);
            await CountryAsync("Tajikistan", unitOfWork, 0);
            await CountryAsync("Tanzania, United Republic of", unitOfWork, 0);
            await CountryAsync("Thailand", unitOfWork, 0);
            await CountryAsync("Timor-Leste", unitOfWork, 0);
            await CountryAsync("Togo", unitOfWork, 0);
            await CountryAsync("Tokelau", unitOfWork, 0);
            await CountryAsync("Tonga", unitOfWork, 0);
            await CountryAsync("Trinidad and Tobago", unitOfWork, 0);
            await CountryAsync("Tunisia", unitOfWork, 0);
            await CountryAsync("Turkey", unitOfWork, 0);
            await CountryAsync("Turkmenistan", unitOfWork, 0);
            await CountryAsync("Turks and Caicos Islands", unitOfWork, 0);
            await CountryAsync("Tuvalu", unitOfWork, 0);
            await CountryAsync("Uganda", unitOfWork, 0);
            await CountryAsync("Ukraine", unitOfWork, 0);
            await CountryAsync("United Arab Emirates", unitOfWork, 0);
            await CountryAsync("United Kingdom", unitOfWork, -101);
            await CountryAsync("United States", unitOfWork, -100);
            await CountryAsync("United States Minor Outlying Islands", unitOfWork, 0);
            await CountryAsync("Uruguay", unitOfWork, 0);
            await CountryAsync("Uzbekistan", unitOfWork, 0);
            await CountryAsync("Vanuatu", unitOfWork, 0);
            await CountryAsync("Venezuela", unitOfWork, 0);
            await CountryAsync("Viet Nam", unitOfWork, 0);
            await CountryAsync("Virgin Islands, British", unitOfWork, 0);
            await CountryAsync("Virgin Islands, U.S.", unitOfWork, 0);
            await CountryAsync("Western Sahara", unitOfWork, 0);
            await CountryAsync("Yemen", unitOfWork, 0);
            await CountryAsync("Zambia", unitOfWork, 0);
            await CountryAsync("Zimbabwe", unitOfWork, 0);
        }

        public static async Task CountryAsync(
            string name,
            Data.Core.IUnitOfWork unitOfWork,
            int sort)
        {
            var core = await unitOfWork
                .SharedCountries
                .SingleOrDefaultAsync(x => string.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Country
                {
                    Name = name,
                    Sort = sort,
                };

                await unitOfWork.AddAsync(core);
                core.Id = name;
            }
        }

        public static async Task CurrenciesAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "gbp";
            var symbol = "£";
            var core = await unitOfWork.ShopCurrencies.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shop.Currency { Symbol = symbol };
                await unitOfWork.ShopCurrencies.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task DataInputPercentOfStructuredAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "LesThan40";

            string name = "< 40 %";
            decimal weighting = 0;
            var core = await unitOfWork.SharedDataInputPercentOfStructureds.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DataInputPercentOfStructured { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Between40And60";
            name = "40-60 %";
            weighting = 0.33m;
            core = await unitOfWork.SharedDataInputPercentOfStructureds.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DataInputPercentOfStructured { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "Between60And80";
            name = "60-80 %";
            weighting = 0.66m;
            core = await unitOfWork.SharedDataInputPercentOfStructureds.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DataInputPercentOfStructured { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "GreaterThen80";
            name = "> 80 %";
            weighting = 1;
            core = await unitOfWork.SharedDataInputPercentOfStructureds.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DataInputPercentOfStructured { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        // DecisionCount
        public static async Task DecisionCountAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "GreaterThen5";
            var name = "> 5";
            decimal weighting = 0;
            var core = await unitOfWork.SharedDecisionCounts.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DecisionCount { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Between4And5";
            name = "4-5";
            weighting = 0.33m;
            core = await unitOfWork.SharedDecisionCounts.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DecisionCount { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Between2And3";
            name = "2-3";
            weighting = 0.66m;
            core = await unitOfWork.SharedDecisionCounts.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DecisionCount { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "One";
            name = "1";
            weighting = 1;
            core = await unitOfWork.SharedDecisionCounts.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DecisionCount { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)" };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        // DecisionDifficulty
        public static async Task DecisionDifficultyAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "ComplexDecisions";
            var name = "The process involves complex decisions";
            var shortName = "complex";
            decimal weighting = 0;
            var core = await unitOfWork.SharedDecisionDifficulties.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DecisionDifficulty { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", ShortName = shortName};
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "SimpleDecisions";
            name = "The process involves simple decisions (yes/no type)";
            weighting = 0.5m;
            shortName = "simple";
            core = await unitOfWork.SharedDecisionDifficulties.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DecisionDifficulty { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", ShortName = shortName };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Linear";
            name = "The process is linear - there are no decisions to be taken";
            weighting = 1;
            shortName = "linear";
            core = await unitOfWork.SharedDecisionDifficulties.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DecisionDifficulty { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", ShortName = shortName };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }


        public static async Task DocumentationPresentAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "No";
            var name = "No";
            decimal weighting = 0;
            var rating = 1;
            var core = await unitOfWork.SharedDocumentationPresents.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DocumentationPresent { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", Rating = rating };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "SomeExists";
            name = "Some exists";
            weighting = 0.5m;
            rating = 2;
            core = await unitOfWork.SharedDocumentationPresents.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DocumentationPresent { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", Rating = rating };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Yes";
            name = "Yes";
            weighting = 1;
            rating = 3;
            core = await unitOfWork.SharedDocumentationPresents.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.DocumentationPresent { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", Rating = rating };
                await unitOfWork.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task LanguagesAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            string id = "en-gb";
            string name = "English (British)";
            string locale = "en-gb";
            var core = await unitOfWork.SharedLanguages.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Language { Name = name, Locale = locale };
                await unitOfWork.SharedLanguages.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "en_us";
            name = "English (United States)";
            locale = "en_US";
            core = await unitOfWork.SharedLanguages.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Language { Name = name, Locale = locale };
                await unitOfWork.SharedLanguages.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task ProcessStabilityAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "SignificantChange";
            var name = "Significant change";
            decimal weighting = 0;
            var rating = 1;
            var core = await unitOfWork.SharedProcessStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)", Rating = rating };
                await unitOfWork.SharedProcessStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "MediumChange";
            name = "Medium change";
            weighting = 0.25m;
            rating = 2;
            core = await unitOfWork.SharedProcessStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", Rating = rating };
                await unitOfWork.SharedProcessStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "SomeChange";
            name = "Some change";
            weighting = 0.5m;
            rating = 3;
            core = await unitOfWork.SharedProcessStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", Rating = rating };
                await unitOfWork.SharedProcessStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "SmallChange";
            name = "Small change";
            weighting = 0.75m;
            rating = 4;
            core = await unitOfWork.SharedProcessStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", Rating = rating };
                await unitOfWork.SharedProcessStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "NoChangeExpected";
            name = "No change expected";
            weighting = 1;
            rating = 5;
            core = await unitOfWork.SharedProcessStabilities.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessStability { Name = name, Weighting = weighting, Colour = "var(--slider-colour4)", Rating = rating };
                await unitOfWork.SharedProcessStabilities.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task InputAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "VeryMuchPaperBased";
            var name = "Very much paper based";
            decimal weighting = 0;
            var rating = 1;
            var core = await unitOfWork.SharedInputs.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Input { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)", Rating = rating };
                await unitOfWork.SharedInputs.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "PrettyMuchPaperBased";
            name = "Pretty much paper based";
            weighting = 0.25m;
            rating = 2;
            core = await unitOfWork.SharedInputs.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Input { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", Rating = rating };
                await unitOfWork.SharedInputs.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "MixBetweenDigitalAndPaperBased";
            name = "A mix between digital and paper based";
            weighting = 0.5m;
            rating = 3;
            core = await unitOfWork.SharedInputs.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Input { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", Rating = rating };
                await unitOfWork.SharedInputs.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "PrettyMuchDigital";
            name = "Pretty much digital";
            weighting = 0.75m;
            rating = 4;
            core = await unitOfWork.SharedInputs.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Input { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", Rating = rating };
                await unitOfWork.SharedInputs.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "VeryMuchDigital";
            name = "Very much digital";
            weighting = 1;
            rating = 5;
            core = await unitOfWork.SharedInputs.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Input { Name = name, Weighting = weighting, Colour = "var(--slider-colour4)", Rating = rating };
                await unitOfWork.SharedInputs.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task InputDataStructureAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "VeryMuchUnstructured";
            var name = "Very much unstructured";
            decimal weighting = 0;
            var rating = 1;
            var core = await unitOfWork.SharedInputDataStructures.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.InputDataStructure { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)", Rating = rating };
                await unitOfWork.SharedInputDataStructures.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "PrettyMuchUnstructured";
            name = "Pretty much unstructured";
            weighting = 0.25m;
            rating = 2;
            core = await unitOfWork.SharedInputDataStructures.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.InputDataStructure { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", Rating = rating };
                await unitOfWork.SharedInputDataStructures.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "MixBetweenStructuredAndUnstructured";
            name = "A mix between structured and unstructured";
            weighting = 0.5m;
            rating = 3;
            core = await unitOfWork.SharedInputDataStructures.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.InputDataStructure { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", Rating = rating };
                await unitOfWork.SharedInputDataStructures.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "PrettyMuchStructured";
            name = "Pretty much structured";
            weighting = 0.75m;
            rating = 4;
            core = await unitOfWork.SharedInputDataStructures.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.InputDataStructure { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", Rating = rating };
                await unitOfWork.SharedInputDataStructures.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "VeryMuchStructured";
            name = "Very much structured";
            weighting = 1;
            rating = 5;
            core = await unitOfWork.SharedInputDataStructures.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.InputDataStructure { Name = name, Weighting = weighting, Colour = "var(--slider-colour4)", Rating = rating };
                await unitOfWork.SharedInputDataStructures.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task ProcessPeakAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            string id = "ThereAreNoPeaks";
            string name = "There are no peaks";
            decimal weighting = 0;
            var core = await unitOfWork.SharedProcessPeaks.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessPeak { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)" };
                await unitOfWork.SharedProcessPeaks.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "RareAndUnpredictable";
            name = "Rare and unpredictable";
            weighting = 0.33m;
            core = await unitOfWork.SharedProcessPeaks.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessPeak { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)" };
                await unitOfWork.SharedProcessPeaks.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "RareButPredictableEvent";
            name = "Rare but predictable event (summer/winter holidays)";
            weighting = 0.66m;
            core = await unitOfWork.SharedProcessPeaks.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessPeak { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)" };
                await unitOfWork.SharedProcessPeaks.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Regular";
            name = "Regular (e.g. month closing)";
            weighting = 1;
            core = await unitOfWork.SharedProcessPeaks.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ProcessPeak { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)" };
                await unitOfWork.SharedProcessPeaks.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task NumberOfWaysToCompleteProcessAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = "MoreThan8Ways";
            var name = "There are more than 8 ways to complete the process";
            var shortName = "> 8";
            decimal weighting = 0;
            var core = await unitOfWork.SharedNumberOfWaysToCompleteProcesses.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)", ShortName = shortName,};
                await unitOfWork.SharedNumberOfWaysToCompleteProcesses.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Between5_8Ways";
            name = "There are between 5-8 ways to complete the process";
            shortName = "5-8";
            weighting = 0.33m;
            core = await unitOfWork.SharedNumberOfWaysToCompleteProcesses.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", ShortName = shortName, };
                await unitOfWork.SharedNumberOfWaysToCompleteProcesses.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "Between2_5Ways";
            name = "There are between 2-5 ways to complete the process";
            shortName = "2-5";
            weighting = 0.66m;
            core = await unitOfWork.SharedNumberOfWaysToCompleteProcesses.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", ShortName = shortName, };
                await unitOfWork.SharedNumberOfWaysToCompleteProcesses.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "SameWay";
            name = "Process is completed the same way every time";
            shortName = "Same Way";
            weighting = 1;
            core = await unitOfWork.SharedNumberOfWaysToCompleteProcesses.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.NumberOfWaysToCompleteProcess { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", ShortName = shortName, };
                await unitOfWork.SharedNumberOfWaysToCompleteProcesses.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task RuleAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            string id = "VeryCreative";
            string name = "Very Creative";
            var core = await unitOfWork.SharedRules.SingleOrDefaultAsync(x => x.Id == id);
            decimal weighting = 0;
            int rating = 1;
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Rule { Name = name, Weighting = weighting, Colour = "var(--slider-colour0)", Rating = rating };
                await unitOfWork.SharedRules.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "PrettyCreative";
            name = "Pretty Creative";
            weighting = 0.25m;
            rating = 2;
            core = await unitOfWork.SharedRules.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Rule { Name = name, Weighting = weighting, Colour = "var(--slider-colour1)", Rating = rating };
                await unitOfWork.SharedRules.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "Mix";
            name = "A mix between rules and creativity";
            weighting = 0.5m;
            rating = 3;
            core = await unitOfWork.SharedRules.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Rule { Name = name, Weighting = weighting, Colour = "var(--slider-colour2)", Rating = rating };
                await unitOfWork.SharedRules.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }


            id = "PrettyRuleBased";
            name = "Pretty Rule Based";
            weighting = 0.75m;
            rating = 4;
            core = await unitOfWork.SharedRules.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Rule { Name = name, Weighting = weighting, Colour = "var(--slider-colour3)", Rating = rating };
                await unitOfWork.SharedRules.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = "VeryRuleBased";
            name = "Very Rule Based";
            weighting = 1;
            rating = 5;
            core = await unitOfWork.SharedRules.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.Rule { Name = name, Weighting = weighting, Colour = "var(--slider-colour4)", Rating = rating };
                await unitOfWork.SharedRules.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task StageAndStatusAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var reviewStageGroup = await AddStageGroupAsync(Data.Core.Enumerators.StageGroup.n00_Review,
                                                 0,
                                                 "Review",
                                                 "Overview of all automations in the Idea stage, awaiting review.",
                                                 unitOfWork);

            var assessStageGroup = await AddStageGroupAsync(Data.Core.Enumerators.StageGroup.n01_Assess,
                                                 1,
                                                 "Assess",
                                                 "Overview of all automations in the Assess stage, awaiting review.",
                                                 unitOfWork);

            var decisionStageGroup = await AddStageGroupAsync(Data.Core.Enumerators.StageGroup.n02_Decision,
                                                     2,
                                                     "Decision",
                                                     "Overview of all automations in the Qualify stage awaiting a decision to be built - or not!",
                                                     unitOfWork);

            var buildStageGroup = await AddStageGroupAsync(Data.Core.Enumerators.StageGroup.n03_Build,
                                                 3,
                                                 "Build",
                                                 "Overview of all automations currently in Development.",
                                                 unitOfWork);

            var deployedStageGroup = await AddStageGroupAsync(Data.Core.Enumerators.StageGroup.n04_Deployed,
                                                     4,
                                                     "Deployed",
                                                     "Overview of all automations currently deployed and live.",
                                                     unitOfWork);

            await unitOfWork.CompleteAsync();

            var ideaStage = await AddStageAsync(Data.Core.Enumerators.Stage.n00_Idea, 0, "Idea", true,false, false, reviewStageGroup, unitOfWork);
            var assessStage = await AddStageAsync(Data.Core.Enumerators.Stage.n01_Assess, 1, "Assess", true, false, false, assessStageGroup, unitOfWork);
            var qualifyStage = await AddStageAsync(Data.Core.Enumerators.Stage.n02_Qualify, 2, "Qualify", true, false, false, decisionStageGroup, unitOfWork);
            var analysisStage = await AddStageAsync(Data.Core.Enumerators.Stage.n03_Analysis, 3, "Analysis", false, true, false, buildStageGroup, unitOfWork);
            var solutionDesignStage = await AddStageAsync(Data.Core.Enumerators.Stage.n04_SolutionDesign, 4, "Solution Design", false, true, false, buildStageGroup, unitOfWork);
            var developmentStage = await AddStageAsync(Data.Core.Enumerators.Stage.n05_Development, 5, "Development", false, true, false, buildStageGroup, unitOfWork);
            var testingStage = await AddStageAsync(Data.Core.Enumerators.Stage.n06_Testing, 6, "Testing", false, true, false, buildStageGroup, unitOfWork);
            var deployedStage = await AddStageAsync(Data.Core.Enumerators.Stage.n07_Deployed, 7, "Deployed", false, false, true, deployedStageGroup, unitOfWork);

            await unitOfWork.CompleteAsync();


            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n00_Idea_AwaitingReview, 0, "Awaiting Review", "pill-warning", "text-warning", ideaStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n01_Idea_Duplicate, 1, "Duplicate", "pill-dark", "text-dark disabled", ideaStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n02_Idea_Rejected, 2, "Rejected", "pill-danger", "text-danger", ideaStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n03_Idea_Archived, 3, "Archived", "pill-dark", "text-dark disabled", ideaStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n04_Assess_AwaitingReview, 4, "Awaiting Review", "pill-warning", "text-warning", assessStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n05_Assess_NotStarted, 5, "Not Started", "pill-warning", "text-warning", assessStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n06_Assess_InProgress, 6, "In Progress", "pill-info", "text-info", assessStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n07_Assess_OnHold, 7, "On Hold", "pill-warning", "text-warning", assessStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n08_Assess_Postponed, 8, "Postponed", "pill-dark", "text-dark disabled", assessStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n09_Assess_Rejected, 9, "Rejected", "pill-danger", "text-danger", assessStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n10_Assess_Archived, 10, "Archived", "pill-dark", "text-dark disabled", assessStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n11_Qualify_AwaitingReview, 11, "Awaiting Review", "pill-warning", "text-warning", qualifyStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n12_Qualify_OnHold, 12, "On Hold", "pill-warning", "text-warning", qualifyStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n13_Qualify_Approved, 13, "Approved", "pill-success", "text-success", qualifyStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n14_Qualify_Rejected, 14, "Rejected", "pill-danger", "text-danger", qualifyStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n15_Qualify_Archived, 15, "Archived", "pill-dark", "text-dark disabled", qualifyStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n16_Analysis_NotStarted, 16, "Not Started", "pill-warning", "text-warning", analysisStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n17_Analysis_InProgress, 17, "In Progress", "pill-info", "text-info", analysisStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n18_Analysis_OnHold, 18, "On Hold", "pill-warning", "text-warning", analysisStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n19_Analysis_Cancelled, 19, "Cancelled", "pill-danger", "text-danger", analysisStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n20_Analysis_AtRisk, 20, "At Risk", "pill-danger", "text-danger", analysisStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n21_Analysis_Delayed, 21, "Delayed", "pill-warning", "text-warning", analysisStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n22_Analysis_Completed, 22, "Completed", "pill-info", "text-info", analysisStage, unitOfWork, true, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n23_Analysis_Archived, 23, "Archived", "pill-dark", "text-dark disabled", analysisStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n24_SolutionDesign_NotStarted, 24, "Not Started", "pill-warning", "text-warning", solutionDesignStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n25_SolutionDesign_InProgress, 25, "In Progress", "pill-info", "text-info", solutionDesignStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n26_SolutionDesign_OnHold, 26, "On Hold", "pill-warning", "text-warning", solutionDesignStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n27_SolutionDesign_Cancelled, 27, "Cancelled", "pill-danger", "text-danger", solutionDesignStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n28_SolutionDesign_AtRisk, 28, "At Risk", "pill-danger", "text-danger", solutionDesignStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n29_SolutionDesign_Delayed, 29, "Delayed", "pill-warning", "text-warning", solutionDesignStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n30_SolutionDesign_Completed, 30, "Completed", "pill-info", "text-info", solutionDesignStage, unitOfWork, true, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n31_SolutionDesign_Archived, 31, "Archived", "pill-dark", "text-dark disabled", solutionDesignStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n32_Development_NotStarted, 32, "Not Started", "pill-warning", "text-warning", developmentStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n33_Development_InProgress, 33, "In Progress", "pill-info", "text-info", developmentStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n34_Development_OnHold, 34, "On Hold", "pill-warning", "text-warning", developmentStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n35_Development_Cancelled, 35, "Cancelled", "pill-danger", "text-danger", developmentStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n36_Development_AtRisk, 36, "At Risk", "pill-danger", "text-danger", developmentStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n37_Development_Delayed, 37, "Delayed", "pill-warning", "text-warning", developmentStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n38_Development_Completed, 38, "Completed", "pill-info", "text-info", developmentStage, unitOfWork, true, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n39_Development_Archived, 39, "Archived", "pill-dark", "text-dark disabled", developmentStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n40_Testing_NotStarted, 40, "Not Started", "pill-warning", "text-warning", testingStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n41_Testing_InProgress, 41, "In Progress", "pill-info", "text-info", testingStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n42_Testing_OnHold, 42, "On Hold", "pill-warning", "text-warning", testingStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n43_Testing_Cancelled, 43, "Cancelled", "pill-danger", "text-danger", testingStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n44_Testing_AtRisk, 44, "At Risk", "pill-danger", "text-danger", testingStage, unitOfWork, false, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n45_Testing_Delayed, 45, "Delayed", "pill-warning", "text-warning", testingStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n46_Testing_Completed, 46, "Completed", "pill-info", "text-info", testingStage, unitOfWork, true, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n47_Testing_Archived, 47, "Archived", "pill-dark", "text-dark disabled", testingStage, unitOfWork, true, false);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n48_Deployed_ReadyForProduction, 48, "Ready for Production", "pill-warning", "text-warning", deployedStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n49_Deployed_HyperCare, 49, "Hypercare", "pill-info", "text-info", deployedStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n50_Deployed_OnHold, 50, "On Hold", "pill-warning", "text-warning", deployedStage, unitOfWork, false, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n51_Deployed_InProduction, 51, "In Production", "pill-info", "text-info", deployedStage, unitOfWork, true, true);
            await AddStatusAsync(Data.Core.Enumerators.IdeaStatus.n52_Deployed_Archived, 52, "Archived", "pill-dark", "text-dark disabled", deployedStage, unitOfWork, true, false);
        }


        private static async Task<Data.Core.Domain.Shared.StageGroup> AddStageGroupAsync(
            Data.Core.Enumerators.StageGroup id,
            int sort,
            string name,
            string description,
            Data.Core.IUnitOfWork unitOfWork)
        {
            var core = await unitOfWork.SharedStageGroups.SingleOrDefaultAsync(x => x.Id == id.ToString());
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.StageGroup { Name = name, Sort = sort, Description = description };
                await unitOfWork.SharedStageGroups.AddAsync(core);
                core.Id = id.ToString(); // Overwrite the generated Id.
            }

            return core;
        }


        private static async Task<Data.Core.Domain.Shared.Stage> AddStageAsync(
            Data.Core.Enumerators.Stage id,
            int sort,
            string name,
            bool setDateAutomatically,
            bool canAssignCost,
            bool isMileStone,
            Data.Core.Domain.Shared.StageGroup stageGroup,
            Data.Core.IUnitOfWork unitOfWork)
        {
            var core = await unitOfWork.SharedStages.SingleOrDefaultAsync(x => x.Id == id.ToString());
            if (core != null)
                return core;


            core = new Data.Core.Domain.Shared.Stage
            {
                Name = name, Sort = sort,
                SetDateAutomatically = setDateAutomatically,
                CanAssignCost = canAssignCost,
                IsMileStone = isMileStone,
                StageGroupId = stageGroup.Id
            };
            await unitOfWork.SharedStages.AddAsync(core);
            core.Id = id.ToString(); // Overwrite the generated Id.

            return core;
        }

        private static async Task AddStatusAsync(Data.Core.Enumerators.IdeaStatus id,
                                                 int sort,
                                                 string name,
                                                 string buttonClass,
                                                 string textClass,
                                                 Data.Core.Domain.Shared.Stage stage,
                                                 Data.Core.IUnitOfWork unitOfWork,
                                                 bool requireIdeaStageField,
                                                 bool isPathToSuccess)
        {
            var core = await unitOfWork.SharedIdeaStatuses.SingleOrDefaultAsync(x => x.Id == id.ToString());
            if (core != null)
                return;

            core = new Data.Core.Domain.Shared.IdeaStatus
            {
                Name = name,
                ButtonClass = buttonClass,
                TextClass = textClass,
                Sort = sort,
                Stage = stage,
                RequireIdeaStageField = requireIdeaStageField,
                IsPathToSuccess = isPathToSuccess,
            };
            await unitOfWork.SharedIdeaStatuses.AddAsync(core);
            core.Id = id.ToString(); // Overwrite the generated Id.
        }


        public static async Task SubmissionPathsAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.SubmissionPath.StandardUser.ToString();
            var name = "Standard User";
            var core = await unitOfWork.SharedSubmissionPaths
                                                  .SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.SubmissionPath { Name = name };
                await unitOfWork.SharedSubmissionPaths.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = Data.Core.Enumerators.SubmissionPath.COEUser.ToString();
            name = "CoE User";
            core = await unitOfWork.SharedSubmissionPaths.SingleOrDefaultAsync(x => x.Id == id);

            if (core == null)
            {
                core = new Data.Core.Domain.Shared.SubmissionPath { Name = name };
                await unitOfWork.SharedSubmissionPaths.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public static async Task TaskFrequencyAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.TaskFrequency.Daily.ToString();
            var name = "Daily";
            var hour = 24;
            var sort = 0;
            var core = await unitOfWork.SharedTaskFrequencies.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.TaskFrequency { Name = name, Hour = hour, Sort = sort };
                await unitOfWork.SharedTaskFrequencies.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = Data.Core.Enumerators.TaskFrequency.Weekly.ToString();
            name = "Weekly";
            hour = 168;
            sort++;
            core = await unitOfWork.SharedTaskFrequencies.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.TaskFrequency { Name = name, Hour = hour, Sort = sort };
                await unitOfWork.SharedTaskFrequencies.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = Data.Core.Enumerators.TaskFrequency.Monthly.ToString();
            name = "Monthly";
            hour = 720;
            sort++;
            core = await unitOfWork.SharedTaskFrequencies.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.TaskFrequency { Name = name, Hour = hour, Sort = sort };
                await unitOfWork.SharedTaskFrequencies.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = Data.Core.Enumerators.TaskFrequency.Quarterly.ToString();
            name = "Quarterly";
            hour = 2160;
            sort++;
            core = await unitOfWork.SharedTaskFrequencies.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.TaskFrequency { Name = name, Hour = hour, Sort = sort };
                await unitOfWork.SharedTaskFrequencies.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = Data.Core.Enumerators.TaskFrequency.BiAnnually.ToString();
            name = "Bi-annually";
            hour = 4320;
            sort++;
            core = await unitOfWork.SharedTaskFrequencies.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.TaskFrequency { Name = name, Hour = hour, Sort = sort };
                await unitOfWork.SharedTaskFrequencies.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }

            id = Data.Core.Enumerators.TaskFrequency.Yearly.ToString();
            name = "Yearly";
            hour = 8640;
            sort++;
            core = await unitOfWork.SharedTaskFrequencies.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.TaskFrequency { Name = name, Hour = hour, Sort = sort };
                await unitOfWork.SharedTaskFrequencies.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
        }

        public async Task<bool> InsertSystemRolesAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var description = "<p>The first user of the client automatically receives the Account Owner role, allowing them to edit the client Settings page.</p>" +
                "<h3 style=\"margin-top:0 !important;\">16 Policies:</h3>" +
                "<div><ul>" +
                "<li>View Ideas</li>" +
                "<li>Share Employee Driven Ideas</li>" +
                "<li>Submit CoE Driven Ideas</li>" +
                "<li>Review New Ideas</li>" +
                "<li>Review Assessed Ideas</li>" +
                "<li>Edit All Idea Fields</li>" +
                "<li>Edit Ideas Stage and Status</li>" +
                "<li>Assign Process Owner</li>" +
                "<li>Archive Ideas</li>" +
                "<li>Delete Ideas</li>" +
                "<li>View Reports</li>" +
                "<li>View Tenant Dashboards</li>" +
                "<li>View Cost Info in Automation Pipeline</li>" +
                "<li>Manage Tenant Settings</li>" +
                "<li>Manage Tenant Users</li>" +
                "<li>Manage Tenant User Roles</li>" +
                "</ul></div>";

            var core = await unitOfWork.InsertRoleAsync( //Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.AccountOwner).ToString(),
                "Account Owner",
                description,
                10);

            core.PolicyCount = 16;


      
            description = "<p>Program Managers can view and edit the ideas, decide what ideas should progress in the implementation stages.</p>" +
                "<h3 style=\"margin-top:0 !important;\">14 Features</h3>" +
                "<div><ul>" +
                "<li>View Ideas</li>" +
                "<li>Share Employee Driven Ideas</li>" +
                "<li>Submit CoE Driven Ideas</li>" +
                "<li>Review New Ideas</li>" +
                "<li>Review Assessed Ideas</li>" +
                "<li>Edit All Idea Fields</li>" +
                "<li>Edit Ideas Stage and Status</li>" +
                "<li>Assign Process Owner</li>" +
                "<li>Archive Ideas</li>" +
                "<li>Delete Ideas</li>" +
                "<li>View Reports</li>" +
                "<li>View Tenant Dashboards</li>" +
                "<li>View Cost Info in Automation Pipeline</li>" +
                "</ul></div>";

            core = await unitOfWork.InsertRoleAsync( //Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.ProgramManager).ToString(),
                "Program Manager",
                description,
                20);

            core.PolicyCount = 14;



            description = "<p>Idea Approvers push forward ideas by identifying duplicates and approving ideas they endorse for a detailed assessment.</p>" +
                "<h3 style=\"margin-top:0 !important;\">4 Features</h3>" +
                "<div><ul>" +
                "<li>View Ideas</li>" +
                "<li>Share Employee Driven Ideas</li>" +
                "<li>Review New Ideas</li>" +
                "<li>Assign Process Owner</li>" +
                "</ul></div>";

            core = await unitOfWork.InsertRoleAsync( //Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.IdeaApprover).ToString(),
                "Idea Approver",
                description,
                30);

            core.PolicyCount = 4;



            description = "<p>Authorised Users target Subject Matter Experts, Process Owners, Automation Business Analysts, Process Consultants who know the process in detail, and have access to Submit a CoE-driven idea, in order to submit their idea by directly filling in the detailed assessment.</p>" +
                "<h3 style=\"margin-top:0 !important;\">3 Features</h3>" +
                "<div><ul>" +
                "<li>View Ideas</li>" +
                "<li>Share Employee Driven Ideas</li>" +
                "<li>Submit CoE Driven Ideas</li>" +
                "</ul></div>";

            core = await unitOfWork.InsertRoleAsync( //Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.AuthorisedUser).ToString(),
                "Authorised User",
                description,
                40);

            core.PolicyCount = 3;



            description = "<p>Automation Sponsors have read-only rights for viewing all the information around the ideas and access to the built-in Dashboards and Reports.</p>" +
                "<h3 style=\"margin-top:0 !important;\">4 Features</h3>" +
                "<div><ul>" +
                "<li>View Ideas</li>" +
                "<li>Share Employee Driven Ideas</li>" +
                "<li>View Reports</li>" +
                "<li>View Tenant Dashboards</li>" +
                "</ul></div>";

            core = await unitOfWork.InsertRoleAsync(//Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.RPASponsor).ToString(),
                "Automation Sponsor",
                description,
                50);

            core.PolicyCount = 4;




            description = "<p>It provides access to submitting ideas, as well as exploring the pipeline of ideas, and the enterprise community of SilkFlo users.</p>" +
                          "<p>All users have this permission assigned by default after the account is created.</p>" +
                          "<h3 style=\"margin-top:0 !important;\">2 Features</h3>" +
                            "<div><ul>" +
                            "<li>View Ideas</li>" +
                            "<li>Share Employee Driven Ideas</li>" +
                            "</ul></div>";

            core = await unitOfWork.InsertRoleAsync( //Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.StandardUser).ToString(),
                "Standard User",
                description,
                60);

            core.PolicyCount = 2;


            description = "<p>Has complete access to the data for all the organisations assigned to them.</p>" +
                          "<h3 style=\"margin-top:0 !important;\">17 Features</h3>" +
                            "<div><ul>" +
                            "<li>View Ideas</li>" +
                            "<li>Share Employee Driven Ideas</li>" +
                            "<li>Submit CoE Driven Ideas</li>" +
                            "<li>Review New Ideas</li>" +
                            "<li>Review Assessed Ideas</li>" +
                            "<li>Edit All Idea Fields</li>" +
                            "<li>Edit Ideas Stage and Status</li>" +
                            "<li>Assign Process Owner</li>" +
                            "<li>Archive Ideas</li>" +
                            "<li>Delete Ideas</li>" +
                            "<li>View Reports</li>" +
                            "<li>View Tenant Dashboards</li>" +
                            "<li>View Cost Info in Automation Pipeline</li>" +
                            "<li>Manage Tenant Settings</li>" +
                            "<li>Manage Tenant Users</li>" +
                            "<li>Manage Tenant User Roles</li>" +
                            "<li>Can Select a client</li>" +
                            "</ul></div>";

            core = await unitOfWork.InsertRoleAsync( //Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.AgencyUser).ToString(),
                "Agency User",
                description,
            70);

            core.PolicyCount = 17;


            description = "<p>Has complete access to the data for all the organisations managed by the agency.</p>" +
                           "<h3 style=\"margin-top:0 !important;\">2 Policies:</h3>" +
                            "<div><ul>" +
                            "<li>Manage the Agency Settings</li>" +
                            "<li>Manage Agency Users</li>" +
                            "</ul></div>";


            core = await unitOfWork.InsertRoleAsync( //Data.Persistence.UnitOfWork.InsertRoleAsync(
                unitOfWork,
                ((int)Data.Core.Enumerators.Role.AgencyAdministrator).ToString(),
                "Agency Administrator",
                description,
            80);

            core.PolicyCount = 2;

            return true;
        }


        public static async Task PagesAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.Page.PageNotFound.ToString();
            var url = id;
            var sort = 10;
            var name = "Page Not Found";
            var text = "<html>\r\n<head>\r\n    <script>\r\n        window.addEventListener('load', (event) =>\r\n        {\r\n            var Analytics = {\r\n\r\n                GetLanguage: function ()\r\n                {\r\n\r\n                    return (navigator.languages && navigator.languages.length) ? navigator.languages[0] : navigator.language;\r\n                },\r\n\r\n                PostURL: function (action)\r\n                {\r\n\r\n                    //https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_nav_all\r\n\r\n\r\n                    if (action === undefined)\r\n                    {\r\n                        action = '';\r\n                    }\r\n\r\n                    const date = new Date();\r\n                    const dateTime = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate() + 'T' + date.getTime() + 'Z';\r\n                    document.cookie = \"DateTime =\" + dateTime;\r\n\r\n                    const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;\r\n                    const language = this.GetLanguage();\r\n                    const platform = navigator.platform;\r\n                    const userAgent = navigator.userAgent;\r\n\r\n\r\n                    // Get or create a user tracker\r\n                    const userTrackerName = \"UserTracker\";\r\n                    let userTracker = this.GetCookie(userTrackerName);\r\n                    if (userTracker.length === 0)\r\n                    {\r\n                        userTracker = this.GUID();\r\n                        document.cookie = userTrackerName + \"=\" + userTracker + \"; expires=Fri, 31 Dec 9999 23:59:59 GMT\";\r\n                    }\r\n\r\n                    // Get or create a session tracker\r\n                    const sessionTrackerName = \"SessionTracker\";\r\n                    let sessionTracker = this.GetCookie(sessionTrackerName);\r\n                    if (sessionTracker.length === 0)\r\n                    {\r\n                        sessionTracker = this.GUID();\r\n                        document.cookie = sessionTrackerName + \"=\" + sessionTracker;\r\n                    }\r\n\r\n\r\n\r\n                    // Create JSON and send\r\n                    const json = '{\"dateTime\":\"' + dateTime + '\", \"url\":\"' + window.location.href + '\", \"action\": \"' + action + '\", \"userTracker\":\"' + userTracker + '\", \"sessionTracker\":\"' + sessionTracker + '\", \"timeZone\":\"' + timeZone + '\", \"language\":\"' + language + '\", \"platform\":\"' + platform + '\", \"userAgent\":\"' + userAgent + '\"}';\r\n\r\n\r\n                    const url = \"/api/analytic/post\";\r\n                    const http = new XMLHttpRequest();\r\n                    http.open(\r\n                        'POST',\r\n                        url,\r\n                        true);\r\n                    http.send(json);\r\n                },\r\n\r\n                Clear: function ()\r\n                {\r\n\r\n                    if (confirm(\"Are you want to delete all the analytics?\"))\r\n                    {\r\n\r\n                        let url = \"/api/analytic/clearAll\";\r\n\r\n                        let http = new XMLHttpRequest();\r\n                        http.open('DELETE', url, true);\r\n                        http.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');\r\n                        http.onreadystatechange = function ()\r\n                        {\r\n                            return function ()\r\n                            {\r\n                                if (http.readyState === XMLHttpRequest.DONE\r\n                                    && http.status === 200)\r\n                                {\r\n\r\n                                    window.location.href = '../Administration/analytic';\r\n                                }\r\n                            };\r\n                        }(this);\r\n                        http.send();\r\n                    }\r\n                },\r\n\r\n                // Pretty good GUID creator, but not cryptographic quality\r\n                GUID: function ()\r\n                {\r\n\r\n                    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c)\r\n                    {\r\n                        let r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);\r\n                        return v.toString(16);\r\n                    });\r\n                },\r\n\r\n                GetCookie: function (name)\r\n                {\r\n                    name = name + \"=\";\r\n                    let decodedCookie = decodeURIComponent(document.cookie);\r\n                    let ca = decodedCookie.split(';');\r\n                    for (let i = 0; i < ca.length; i++)\r\n                    {\r\n                        let c = ca[i];\r\n                        while (c.charAt(0) === ' ')\r\n                        {\r\n                            c = c.substring(1);\r\n                        }\r\n                        if (c.indexOf(name) === 0)\r\n                        {\r\n                            return c.substring(name.length, c.length);\r\n                        }\r\n                    }\r\n                    return \"\";\r\n                },\r\n            };\r\n\r\n            SilkFlo.Analytics.PostURL('Page not found. 404');\r\n        })\r\n    </script>\r\n    <style>\r\n        body {\r\n            background-color: black;\r\n        }\r\n\r\n        p {\r\n            color: red;\r\n            text-align: center;\r\n            font-family: monospace;\r\n            font-size: 16px\r\n        }\r\n\r\n        div.solid {\r\n            border-style: solid;\r\n            border-color: red;\r\n            padding: 10px;\r\n            border-width: 5px;\r\n        }\r\n    </style>\r\n</head>\r\n    <body>\r\n        <div class=\"solid\">\r\n            <p>HTTP Page not found. 404&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Press back button to continue.</p>\r\n            <p>Guru Meditation #22000006.8400000E</p>\r\n        <div>\r\n    </body>\r\n</html>";

            var core = await unitOfWork.ApplicationPages.SingleOrDefaultAsync(x => x.URL == url);
            if (core == null)
            {
                core = new Data.Core.Domain.Application.Page
                {
                    URL = url,
                    Name = name,
                    Sort = sort,
                    IsMenuItem = false,
                    Text = text,
                };
                await unitOfWork.ApplicationPages.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
            else
                core.Id = id;


            id = Data.Core.Enumerators.Page.UnderConstruction.ToString();
            url = id;
            sort += 10;
            name = "Under Construction";
            text = "<div class=\"container\">\r\n    <h1>Under Construction</h1>\r\n    <img src=\"/Images/Land Rover.png\" style=\"width: 50%; margin-left:70px; \" />\r\n</div>";

            core = await unitOfWork.ApplicationPages.SingleOrDefaultAsync(x => x.URL == url);
            if (core == null)
            {
                core = new Data.Core.Domain.Application.Page
                {
                    URL = url,
                    Name = name,
                    Sort = sort,
                    IsMenuItem = false,
                    Text = text,
                };
                await unitOfWork.ApplicationPages.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
            else
                core.Id = id;


            id = Data.Core.Enumerators.Page.PlatformTerms.ToString();
            url = id;
            sort += 10;
            name = "Terms of Service";
            text = "<div><style><!-- /* Font Definitions */ @font-face{font-family:Wingdings;panose-1:5 0 0 0 0 0 0 0 0 0;}@font-face{font-family:\"Cambria Math\";panose-1:2 4 5 3 5 4 6 3 2 4;}@font-face{font-family:Calibri;panose-1:2 15 5 2 2 2 4 3 2 4;}@font-face{font-family:\"Arial Unicode MS\";panose-1:2 11 6 4 2 2 2 2 2 4;}@font-face{font-family:Corbel;panose-1:2 11 5 3 2 2 4 2 2 4;}@font-face{font-family:\"\\@Arial Unicode MS\";panose-1:2 11 6 4 2 2 2 2 2 4;} /* Style Definitions */ p.MsoNormal, li.MsoNormal, div.MsoNormal{margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:0cm;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.MsoHeader, li.MsoHeader, div.MsoHeader{margin-top:6.0pt;margin-right:0cm;margin-bottom:0cm;margin-left:0cm;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.MsoFooter, li.MsoFooter, div.MsoFooter{mso-style-link:\"Footer Char\";margin-top:6.0pt;margin-right:0cm;margin-bottom:0cm;margin-left:0cm;text-align:center;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.MsoBodyText, li.MsoBodyText, div.MsoBodyText{margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:0cm;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.MsoBodyText2, li.MsoBodyText2, div.MsoBodyText2{margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:3.0cm;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.MsoBodyText3, li.MsoBodyText3, div.MsoBodyText3{margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:4.0cm;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.BodyText1, li.BodyText1, div.BodyText1{mso-style-name:\"Body Text 1\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:42.5pt;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Definition, li.Definition, div.Definition{mso-style-name:Definition;margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:3.0cm;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.IntroHeading, li.IntroHeading, div.IntroHeading{mso-style-name:\"Intro Heading\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:0cm;text-align:justify;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level1Heading, li.Level1Heading, div.Level1Heading{mso-style-name:\"Level 1 Heading\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:42.5pt;text-align:justify;text-indent:-42.5pt;page-break-after:avoid;font-size:11.0pt;font-family:\"Arial\",sans-serif;font-variant:small-caps;font-weight:bold;}p.Level2Number, li.Level2Number, div.Level2Number{mso-style-name:\"Level 2 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:3.0cm;text-align:justify;text-indent:-42.5pt;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level3Number, li.Level3Number, div.Level3Number{mso-style-name:\"Level 3 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:4.0cm;text-align:justify;text-indent:-1.0cm;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level4Number, li.Level4Number, div.Level4Number{mso-style-name:\"Level 4 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:5.0cm;text-align:justify;text-indent:-1.0cm;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level5Number, li.Level5Number, div.Level5Number{mso-style-name:\"Level 5 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:6.0cm;text-align:justify;text-indent:-1.0cm;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level6Number, li.Level6Number, div.Level6Number{mso-style-name:\"Level 6 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:7.0cm;text-align:justify;text-indent:-1.0cm;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level7Number, li.Level7Number, div.Level7Number{mso-style-name:\"Level 7 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:8.0cm;text-align:justify;text-indent:-1.0cm;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level8Number, li.Level8Number, div.Level8Number{mso-style-name:\"Level 8 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:9.0cm;text-align:justify;text-indent:-1.0cm;font-size:10.0pt;font-family:\"Arial\",sans-serif;}p.Level9Number, li.Level9Number, div.Level9Number{mso-style-name:\"Level 9 Number\";margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:10.0cm;text-align:justify;text-indent:-1.0cm;font-size:10.0pt;font-family:\"Arial\",sans-serif;}span.FooterChar{mso-style-name:\"Footer Char\";mso-style-link:Footer;}span.DefTerm{mso-style-name:DefTerm;font-family:\"Arial\",sans-serif;color:black;font-weight:bold;}p.DefinedTermPara, li.DefinedTermPara, div.DefinedTermPara{mso-style-name:\"Defined Term Para\";margin-top:0cm;margin-right:0cm;margin-bottom:6.0pt;margin-left:72.0pt;text-align:justify;text-indent:-36.0pt;line-height:15.0pt;font-size:11.0pt;font-family:\"Calibri\",sans-serif;}p.DefinedTermNumber, li.DefinedTermNumber, div.DefinedTermNumber{mso-style-name:\"Defined Term Number\";margin-top:0cm;margin-right:0cm;margin-bottom:6.0pt;margin-left:113.7pt;text-align:justify;text-indent:0cm;line-height:15.0pt;font-size:11.0pt;font-family:\"Calibri\",sans-serif;}.MsoChpDefault{font-size:10.0pt;font-family:\"Arial\",sans-serif;} /* Page Definitions */ @page WordSection1{size:21.0cm 842.0pt;margin:11.35pt 2.0cm 11.35pt 2.0cm;}div.WordSection1{page:WordSection1;} /* List Definitions */ ol{margin-bottom:0cm;}ul{margin-bottom:0cm;}--></style><p class=MsoBodyText align=center style='text-align:center'><b><u><span style='font-size:9.0pt'>SILKFLO PLATFORM TERMS</span></u></b></p><p class=MsoNormal style='margin-left:.1pt;text-indent:-.1pt;border:none'><span style='font-size:9.0pt'>This agreement sets out the terms on which SilkFloLimited, a company incorporated and registered in England and Wales withcompany number 13433009 whose registered office is at 74 Sidney Street, London,England, E1 2EU (<b>SilkFlo</b>), will grant you, the entity whose details youhave submitted to the sign up page (<b>Customer</b>) the right to access theServices (as defined below).</span></p><p class=MsoNormal style='margin-left:.1pt;text-indent:-.1pt;border:none'><span style='font-size:9.0pt'>By clicking on the “accept” button you agree to theseterms which will bind you.</span></p><p class=IntroHeading><b><span style='font-size:9.0pt;text-transform:uppercase'>Itis hereby agreed</span></b></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>1.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Interpretation</span></p><p class=Level2Number><span style='font-size:9.0pt'>1.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The definitions and rules ofinterpretation in this clause apply in this agreement.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Administrative Users:</span></b><span style='font-size:9.0pt'> those</span> <span style='font-size:9.0pt'>employees,agents and independent contractors of the Customer who are authorised by theCustomer to use the Services and the Documentation as “Account Owner”, “ProgramManager”, “Idea Approver”, and “Authorised User” each of which has the accessrights of a Standard User, as well as additional access to greaterfunctionality than a Standard User, but each of which also has its ownrespective usage restrictions,</span> <span style='font-size:9.0pt'>as furtherdescribed in clause 2.2(d)</span></p><p class=Definition><b><span style='font-size:9.0pt'>Business Day: </span></b><span style='font-size:9.0pt'>a day other than a Saturday, Sunday or public holidayin England when banks in London are open for business.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Change of Control: </span></b><span style='font-size:9.0pt'>the beneficial ownership of more than 50% of the issuedshare capital of a company or the legal power to direct or cause the directionof the general management of the company, and controls, controlled and theexpression change of control shall be construed accordingly.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Confidential Information: </span></b><span style='font-size:9.0pt'>information that is proprietary or confidential and iseither clearly labelled as such or identified as Confidential Information inclause </span><span style='font-size:9.0pt'>11.5</span><span style='font-size:9.0pt'> or clause </span><span style='font-size:9.0pt'>11.6</span><span style='font-size:9.0pt'>.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Controller</span></b><span style='font-size:9.0pt'>, <b>Processor</b>, <b>Data Subject</b>, <b>PersonalData</b>, <b>Personal Data Breach</b> and <b>Processing</b>: have the meaningsgiven to them in the Data Protection Legislation.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Customer Data: </span></b><span style='font-size:9.0pt'>the data inputted by the Customer, Users, or SilkFlo onthe Customer's behalf for the purpose of using the Services or facilitating theCustomer's use of the Services.</span></p><p class=DefinedTermPara style='margin-top:6.0pt;margin-right:0cm;margin-bottom:6.0pt;margin-left:3.0cm;text-indent:0cm;line-height:normal'><spanclass=DefTerm><span style='font-size:9.0pt'>Data Protection Legislation</span></span><span style='font-size:9.0pt;font-family:\"Arial\",sans-serif'>: all applicable dataprotection and privacy legislation in force from time to time in the UKincluding the UK GDPR; the Data Protection Act 2018; and the Privacy andElectronic Communications Regulations 2003 (SI 2003/2426) as amended.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Documentation: </span></b><span style='font-size:9.0pt'>the documentation made available to the Customer bySilkFlo online via www.SilkFlo.com or such other web address notified bySilkFlo to the Customer from time to time which sets out a description of theServices and the user instructions for the Services.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Effective Date: </span></b><span style='font-size:9.0pt'>the date of this agreement.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Heightened CybersecurityRequirements: </span></b><span style='font-size:9.0pt'>any laws, regulations,codes, guidance (from regulatory and advisory bodies. Whether mandatory ornot), international and national standards, industry schemes and sanctions,which are applicable to either the Customer or an User (but not SilkFlo)relating to security of network and information systems and security breach andincident reporting requirements, which may include the cybersecurity Directive((EU) 2016/1148), Commission Implementing Regulation ((EU) 2018/151), theNetwork and Information systems Regulations 2018 (SI 506/2018), all as amendedor updated from time to time.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Initial Subscription Term:</span></b><span style='font-size:9.0pt'>the initial term of this agreement asset out on the pricing page.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Normal Business Hours: </span></b><span style='font-size:9.0pt'>9.00 am to 5.00 pm local UK time, each Business Day.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Renewal Period: </span></b><span style='font-size:9.0pt'>the period described in clause </span><span style='font-size:9.0pt'>14.1</span><span style='font-size:9.0pt'>.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Services: </span></b><span style='font-size:9.0pt'>the subscription services provided by SilkFlo to theCustomer under this agreement via https://silkflo.com or any other website notified to the Customer by SilkFlo from time totime, as more particularly described in the Documentation.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Software: </span></b><span style='font-size:9.0pt'>the software applications provided by SilkFlo as partof the Services.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Standard User:</span></b><span style='font-size:9.0pt'> those employees, agents and independent contractors ofthe Customer who are authorised by the Customer to use the Services and theDocumentation with restrictions on the functions they can access, and only haveaccess to the Services in order to submit automation ideas, view the Customer’sideas pipeline and profiles of other Users, and interact with the Customer’sleader-board.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Subscription Fees: </span></b><span style='font-size:9.0pt'>the subscription fees payable by the Customer toSilkFlo for the User Subscriptions, as set out on the pricing page.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Subscription Term: </span></b><span style='font-size:9.0pt'>has the meaning given in clause </span><span style='font-size:9.0pt'>14.1</span><span style='font-size:9.0pt'> (being theInitial Subscription Term together with any subsequent Renewal Periods).</span></p><p class=Definition><b><span style='font-size:9.0pt'>Users: </span></b><span style='font-size:9.0pt'>Administrative Users and Standard Users.</span></p><p class=Definition><b><span style='font-size:9.0pt'>User Subscriptions: </span></b><span style='font-size:9.0pt'>the user subscriptions purchased by the Customerpursuant to clause </span><span style='font-size:9.0pt'>9.1</span><span style='font-size:9.0pt'> which entitle AdministrativeUsers to access and use the Services and the Documentation in accordance withthis agreement.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Virus: </span></b><span style='font-size:9.0pt'>any thing or device (including any software, code, fileor programme) which may: prevent, impair or otherwise adversely affect theoperation of any computer software, hardware or network, any telecommunicationsservice, equipment or network or any other service or device; prevent, impairor otherwise adversely affect access to or the operation of any programme ordata, including the reliability of any programme or data (whether byre-arranging, altering or erasing the programme or data in whole or part orotherwise); or adversely affect the user experience, including worms, trojanhorses, viruses and other similar things or devices.</span></p><p class=Definition><b><span style='font-size:9.0pt'>Vulnerability: </span></b><span style='font-size:9.0pt'>a weakness in the computational logic (for example,code) found in software and hardware components that when exploited, results ina negative impact to the confidentiality, integrity, or availability, and theterm Vulnerabilities shall be construed accordingly.</span></p><p class=Level2Number><span style='font-size:9.0pt'>1.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Clause headings shall not affectthe interpretation of this agreement. References to clauses are to the clausesof this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>1.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>A person includes an individual,corporate or unincorporated body (whether or not having separate legalpersonality) and that person's legal and personal representatives, successorsor permitted assigns. A reference to a company shall include any company,corporation or other body corporate, wherever and however incorporated orestablished.</span></p><p class=Level2Number><span style='font-size:9.0pt'>1.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Unless the context otherwiserequires, words in the singular shall include the plural and in the pluralshall include the singular. Unless the context otherwise requires, a referenceto one gender shall include a reference to the other genders. A reference towriting or written includes faxes but not e-mail.</span></p><p class=Level2Number><span style='font-size:9.0pt'>1.5<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>A reference to a statute orstatutory provision is a reference to it as it is in force from time to time. Areference to a statute or statutory provision shall include all subordinatelegislation made from time to time under that statute or statutory provision.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>2.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>User subscriptions</span></p><p class=Level2Number><span style='font-size:9.0pt'>2.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Subject to the Customer purchasingthe User Subscriptions in accordance with clause </span><span style='font-size:9.0pt'>3.3</span><span style='font-size:9.0pt'>, clause </span><span style='font-size:9.0pt'>3.4</span><span style='font-size:9.0pt'> and clause </span><span style='font-size:9.0pt'>9.1</span><span style='font-size:9.0pt'>, therestrictions set out in this clause </span><span style='font-size:9.0pt'>2</span><span style='font-size:9.0pt'> and the otherterms and conditions of this agreement, SilkFlo hereby grants to the Customer anon-exclusive, non-transferable right, without the right to grant sublicences,to permit both the Administrative Users (not exceeding the number of UserSubscriptions purchased) and the Standard Users to use the Services and theDocumentation during the Subscription Term solely for the Customer's internalbusiness operations, in accordance with the restrictions of the relevantsubscription package purchased as set out on the pricing page. The number ofUser Subscriptions initially purchased by the Customer shall be agreed and setout on the pricing page.</span></p><p class=Level2Number><span style='font-size:9.0pt'>2.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>In relation to the Users, theCustomer undertakes that:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the maximum number of AdministrativeUsers that it authorises to access and use the Services and the Documentationshall not exceed the number of User Subscriptions it has purchased from time totime;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>it will not allow or suffer anyUser Subscription to be used by more than one individual Administrative Userunless it has been reassigned in its entirety to another individual AdministrativeUser, in which case the prior Administrative User shall no longer have anyright to access or use the Services and/or Documentation;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>each User shall keep a securepassword for their use of the Services and Documentation, that such passwordshall be changed regularly (including upon request of SilkFlo, and SilkFloreserves the right to disable any User’s ability to use and access the Serviceswhere it fails or refuses to do so) and that each User shall keep theirpassword confidential;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>it shall maintain a written, up todate list of current Users and provide such list to SilkFlo within 5 BusinessDays of SilkFlo's written request at any time or times;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(e)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>it shall permit SilkFlo orSilkFlo's designated auditor to audit the Services in order to establish thename and password of each User and the Customer's data processing facilities toaudit compliance with this agreement. Each such audit may be conducted no morethan once per quarter, at SilkFlo's expense, and this right shall be exercisedwith reasonable prior notice, in such a manner as not to substantiallyinterfere with the Customer's normal conduct of business;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(f)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>if any of the audits referred to inclause </span><span style='font-size:9.0pt'>2.2(e)</span><span style='font-size:9.0pt'> reveal thatany password has been provided to any individual who is not an User, thenwithout prejudice to SilkFlo's other rights, the Customer shall promptlydisable such passwords and SilkFlo shall not issue any new passwords to anysuch individual; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(g)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>if any of the audits referred to inclause </span><span style='font-size:9.0pt'>2.2(e)</span><span style='font-size:9.0pt'> reveal thatthe Customer has underpaid Subscription Fees to SilkFlo, then without prejudiceto SilkFlo's other rights, the Customer shall pay to SilkFlo an amount equal tosuch underpayment within 10 Business Days of the date of the relevant audit.</span></p><p class=Level2Number><span style='font-size:9.0pt'>2.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall not access,store, distribute or transmit any Viruses, or any material during the course ofits use of the Services that:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>is unlawful, harmful, threatening,defamatory, obscene, infringing, harassing or racially or ethnically offensive;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>facilitates illegal activity;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>depicts sexually explicit images;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>promotes unlawful violence;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(e)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>is discriminatory based on race,gender, colour, religious belief, sexual orientation, disability; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(f)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>is otherwise illegal or causesdamage or injury to any person or property;</span></p><p class=MsoBodyText2><span style='font-size:9.0pt'>and SilkFlo reserves theright, without liability or prejudice to its other rights to the Customer, todisable the Customer's access to any material that breaches the provisions ofthis clause.</span></p><p class=Level2Number><span style='font-size:9.0pt'>2.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall not:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>except as may be allowed by anyapplicable law which is incapable of exclusion by agreement between the partiesand except to the extent expressly permitted under this agreement:</span></p><p class=Level4Number><span style='font-size:9.0pt'>(i)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>attempt to copy, modify, duplicate,create derivative works from, frame, mirror, republish, download, display,transmit, or distribute all or any portion of the Software and/or Documentation(as applicable) in any form or media or by any means; or</span></p><p class=Level4Number><span style='font-size:9.0pt'>(ii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>attempt to de-compile, reversecompile, disassemble, reverse engineer or otherwise reduce to human-perceivableform all or any part of the Software; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>access all or any part of theServices and Documentation in order to build a product or service whichcompetes with the Services and/or the Documentation; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>use the Services and/orDocumentation to provide services to third parties; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>subject to clause </span><span style='font-size:9.0pt'>22.1</span><span style='font-size:9.0pt'>, license,sell, rent, lease, transfer, assign, distribute, display, disclose, orotherwise commercially exploit, or otherwise make the Services and/orDocumentation available to any third party except the Users, or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(e)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>attempt to obtain, or assist thirdparties in obtaining, access to the Services and/or Documentation, other thanas provided under this clause </span><span style='font-size:9.0pt'>2</span><span style='font-size:9.0pt'>; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(f)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>introduce or permit theintroduction of, any Virus or Vulnerability into SilkFlo's network andinformation systems.</span></p><p class=Level2Number><span style='font-size:9.0pt'>2.5<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall use all reasonableendeavours to prevent any unauthorised access to, or use of, the Servicesand/or the Documentation and, in the event of any such unauthorised access oruse, promptly notify SilkFlo.</span></p><p class=Level2Number><span style='font-size:9.0pt'>2.6<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The rights provided under thisclause </span><span style='font-size:9.0pt'>2</span><span style='font-size:9.0pt'> are granted tothe Customer only, and shall not be considered granted to any subsidiary orholding company of the Customer.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>3.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Additional user subscriptions</span></p><p class=Level2Number><span style='font-size:9.0pt'>3.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Subject to clause </span><span style='font-size:9.0pt'>3.2</span><span style='font-size:9.0pt'> and clause </span><span style='font-size:9.0pt'>3.3</span><span style='font-size:9.0pt'>, the Customermay, from time to time during any Subscription Term, purchase additional UserSubscriptions in excess of the number already purchased and SilkFlo shall grantaccess to the Services and the Documentation to such additional AdministrativeUsers in accordance with the provisions of this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>3.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>If the Customer wishes to purchaseadditional User Subscriptions, the Customer shall notify SilkFlo in writing.SilkFlo shall evaluate such request for additional User Subscriptions andrespond to the Customer with approval or rejection of the request (suchapproval not to be unreasonably withheld). Where SilkFlo approves the request,SilkFlo shall activate the additional User Subscriptions within 7 days of itsapproval of the Customer's request.</span></p><p class=Level2Number><span style='font-size:9.0pt'>3.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>If SilkFlo approves the Customer'srequest to purchase additional User Subscriptions, the Customer shall, within30 days of the date of SilkFlo's invoice, pay to SilkFlo the relevant fees forsuch additional User Subscriptions as are notified to it by SilkFlo and, ifsuch additional User Subscriptions are purchased by the Customer part waythrough the Initial Subscription Term or any Renewal Period (as applicable),such fees shall be pro-rated from the date of activation by SilkFlo for theremainder of the Initial Subscription Term or then current Renewal Period (asapplicable).</span></p><p class=Level2Number><span style='font-size:9.0pt'>3.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Alternatively, where SilkFloimplements such functionality, the Customer may purchase additional UserSubscriptions via the Services directly. In such cases, the Customer will berequired to pay the relevant fees for such User Subscriptions in advance, andupon doing so will be given access to the additional User Subscriptions.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>4.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Services</span></p><p class=Level2Number><span style='font-size:9.0pt'>4.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo shall, during theSubscription Term, provide the Services and make available the Documentation tothe Customer on and subject to the terms of this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>4.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo shall use commerciallyreasonable endeavours to make the Services available 24 hours a day, seven daysa week, except for planned and unscheduled maintenance performed outside NormalBusiness Hours, provided that SilkFlo has used reasonable endeavours to givethe Customer at least 6 Normal Business Hours' notice in advance.</span></p><p class=Level2Number><span style='font-size:9.0pt'>4.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo will, as part of theServices and at no additional cost to the Customer, provide the Customer withSilkFlo's standard customer support services whereby the Customer may submit supportqueries to SilkFlo during Normal Business Hours by emailing support@silkflo.com.SilkFlo may amend the nature of its support services in its sole and absolutediscretion from time to time. The Customer may purchase enhanced supportservices separately at SilkFlo's then current rates.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>5.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Data protection</span></p><p class=Level2Number><span style='font-size:9.0pt'>5.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Both parties will comply with allapplicable requirements of the Data Protection Legislation. This clause </span><span style='font-size:9.0pt'>5</span><span style='font-size:9.0pt'> is in additionto, and does not relieve, remove or replace, a party's obligations under theData Protection Legislation. </span></p><p class=Level2Number><span style='font-size:9.0pt'>5.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The parties acknowledge that forthe purposes of the Data Protection Legislation, the Customer is the datacontroller and SilkFlo is the data processor (where <span class=DefTerm>DataController</span> and <span class=DefTerm>Data Processor</span> have themeanings as defined in the Data Protection Legislation). The following tablesets out the scope, nature and purpose of processing by SilkFlo, the durationof the processing and the types of Personal Data and categories of DataSubject:</span></p><table class=MsoNormalTable border=1 cellspacing=0 cellpadding=0 style='margin-left:84.8pt;border-collapse:collapse;border:none'> <tr>  <td width=525 colspan=2 style='width:394.0pt;border:solid windowtext 1.0pt;  background:#BFBFBF;padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal align=center style='text-align:center;text-autospace:none'><b><span  style='font-size:9.0pt'>Data Processing Details</span></b></p>  </td> </tr> <tr>  <td width=65 style='width:48.45pt;border:solid windowtext 1.0pt;border-top:  none;padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><b><span style='font-size:  9.0pt'>Subject-matter</span></b></p>  </td>  <td width=461 style='width:345.55pt;border-top:none;border-left:none;  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;  padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><span style='font-size:9.0pt'>The  processing of personal data resulting from the provision of services by  SilkFlo under this agreement.</span></p>  </td> </tr> <tr>  <td width=65 style='width:48.45pt;border:solid windowtext 1.0pt;border-top:  none;padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><b><span style='font-size:  9.0pt'>Nature and purpose</span></b></p>  </td>  <td width=461 style='width:345.55pt;border-top:none;border-left:none;  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;  padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><span style='font-size:9.0pt'>The  personal data will be processed in the course of the operation of the  Services. </span></p>  </td> </tr> <tr>  <td width=65 style='width:48.45pt;border:solid windowtext 1.0pt;border-top:  none;padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><b><span style='font-size:  9.0pt'>Duration</span></b></p>  </td>  <td width=461 style='width:345.55pt;border-top:none;border-left:none;  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;  padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><span style='font-size:9.0pt'>The  duration of this agreement</span></p>  </td> </tr> <tr>  <td width=65 style='width:48.45pt;border:solid windowtext 1.0pt;border-top:  none;padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><b><span style='font-size:  9.0pt'>Types of personal data</span></b></p>  </td>  <td width=461 style='width:345.55pt;border-top:none;border-left:none;  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;  padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><span style='font-size:9.0pt'>Names  and email addresses of Users.</span></p>  </td> </tr> <tr>  <td width=65 style='width:48.45pt;border:solid windowtext 1.0pt;border-top:  none;padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><b><span style='font-size:  9.0pt'>Categories of Data Subject</span></b></p>  </td>  <td width=461 style='width:345.55pt;border-top:none;border-left:none;  border-bottom:solid windowtext 1.0pt;border-right:solid windowtext 1.0pt;  padding:0cm 5.4pt 0cm 5.4pt'>  <p class=MsoNormal style='text-autospace:none'><span style='font-size:9.0pt'>Users.</span></p>  </td> </tr></table><p class=Level2Number><span style='font-size:9.0pt'>5.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;color:white'>Without prejudice tothe generalit</span><span style='font-size:9.0pt'>y of clause </span><span style='font-size:9.0pt'>5.1</span><span style='font-size:9.0pt'>, SilkFlo shall,in relation to any Personal Data processed in connection with the performanceby SilkFlo of its obligations under this agreement:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>process that Personal Data only onthe written instructions of the Customer unless SilkFlo is required by DataProtection Legislation to otherwise process that Personal Data. Where SilkFlois relying on Data Protection Legislation as the basis for processing PersonalData, SilkFlo shall promptly notify the Customer of this before performing theprocessing required by Data Protection Legislation unless the Data ProtectionLegislation prohibits SilkFlo from so notifying the Customer;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>ensure that it has in placeappropriate technical and organisational measures, to protect againstunauthorised or unlawful processing of Personal Data and against accidentalloss or destruction of, or damage to, Personal Data, appropriate to the harmthat might result from the unauthorised or unlawful processing or accidentalloss, destruction or damage and the nature of the data to be protected, havingregard to the state of technological development and the cost of implementingany measures (those measures may include, where appropriate, pseudonymising andencrypting Personal Data, ensuring confidentiality, integrity, availability andresilience of its systems and services, ensuring that availability of andaccess to Personal Data can be restored in a timely manner after an incident,and regularly assessing and evaluating the effectiveness of the technical andorganisational measures adopted by it);</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>not transfer any Personal Dataoutside the United Kingdom or the EEA unless the following conditions are fulfilled:</span></p><p class=Level4Number><span style='font-size:9.0pt'>(i)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer or SilkFlo hasprovided appropriate safeguards in relation to the transfer;</span></p><p class=Level4Number><span style='font-size:9.0pt'>(ii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Data Subject has enforceablerights and effective legal remedies;</span></p><p class=Level4Number><span style='font-size:9.0pt'>(iii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo complies with itsobligations under the Data Protection Legislation by providing an adequatelevel of protection to any Personal Data that is transferred; and</span></p><p class=Level4Number><span style='font-size:9.0pt'>(iv)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo complies with reasonableinstructions notified to it in advance by the Customer with respect to theprocessing of the Personal Data;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>assist the Customer, at theCustomer's cost, in responding to any request from a Data Subject and inensuring compliance with its obligations under the Data Protection Legislationwith respect to security, breach notifications, impact assessments andconsultations with supervisory authorities or regulators;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(e)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>notify the Customer without unduedelay on becoming aware of a Personal Data breach;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(f)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>at the written direction of theCustomer, delete or return Personal Data and copies thereof to the Customer ontermination of the agreement unless required by Data Protection Legislation tostore the Personal Data; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(g)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>maintain complete and accuraterecords and information to demonstrate its compliance with this clause </span><span style='font-size:9.0pt'>5</span><span style='font-size:9.0pt'>.</span></p><p class=Level2Number><span style='font-size:9.0pt'>5.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer consents generally toSilkFlo appointing third-party processors of Personal Data under thisagreement. SilkFlo confirms that it has entered or (as the case may be) willenter with the third-party processor into a written agreement incorporatingterms which are substantially similar to those set out in this clause </span><span style='font-size:9.0pt'>5</span><span style='font-size:9.0pt'>. As between theCustomer and SilkFlo, SilkFlo shall remain fully liable for all acts oromissions of any third-party processor appointed by it pursuant to this clause </span><span style='font-size:9.0pt'>5</span><span style='font-size:9.0pt'>.</span></p><p class=Level2Number><span style='font-size:9.0pt'>5.5<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo may, at any time on notless than 30 days’ notice, revise this clause </span><span style='font-size:9.0pt'>5</span><span style='font-size:9.0pt'> by replacing itwith any applicable controller to processor standard clauses or similar termsforming party of an applicable certification scheme (which shall apply whenreplaced by attachment to this agreement).</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>6.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Third party providers</span></p><p class=BodyText1><span style='font-size:9.0pt'>The Customer acknowledges that the Services may enable or assist it to access the website content of,correspond with, and purchase products and services from, third parties via third-party websites and that it does so solely at its own risk. SilkFlo makes no representation, warranty or commitment and shall have no liability or obligation whatsoever in relation to the content or use of, or correspondence with, any such third-party website, or any transactions completed, and any contract entered into by the Customer, with any such third party. Any contract entered into and any transaction completed via any third-party website is between the Customer and the relevant third party, and not SilkFlo. SilkFlo recommends that the Customer refers to the third party's website terms and conditions and privacy policy prior to using the relevant third-party website.SilkFlo does not endorse or approve any third-party website nor the content of any of the third-party website made available via the Services.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>7.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Supplier's obligations</span></p><p class=Level2Number><span style='font-size:9.0pt'>7.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo undertakes that theServices will be performed substantially in accordance with the Documentation and with reasonable skill and care.</span></p><p class=Level2Number><span style='font-size:9.0pt'>7.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The undertaking at clause </span><span style='font-size:9.0pt'>7.1</span><span style='font-size:9.0pt'> shall not apply to the extent of any non-conformance which is caused by use of the Services contrary to SilkFlo's instructions, or modification or alteration of theServices by any party other than SilkFlo or SilkFlo's duly authorised contractors or agents. If the Services do not conform with the foregoing undertaking, Supplier will, at its expense, use all reasonable commercial endeavours to correct any such non-conformance promptly, or provide theCustomer with an alternative means of accomplishing the desired performance.Such correction or substitution constitutes the Customer's sole and exclusive remedy for any breach of the undertaking set out in clause </span><span style='font-size:9.0pt'>7.1</span><span style='font-size:9.0pt'>.</span></p><p class=Level2Number><span style='font-size:9.0pt'>7.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>does not warrant that:</span></p><p class=Level4Number><span style='font-size:9.0pt'>(i)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer's use of the Services will be uninterrupted or error-free; </span></p><p class=Level4Number><span style='font-size:9.0pt'>(ii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Services, Documentation and/or the information obtained by the Customer through the Services will meet the Customer's requirements; </span></p><p class=Level4Number><span style='font-size:9.0pt'>(iii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Software or the Services willbe free from Vulnerabilities or Viruses; or</span></p><p class=Level4Number><span style='font-size:9.0pt'>(iv)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Software, Documentation orServices will comply with any Heightened Cyber security Requirements.</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>is not responsible for any delays,delivery failures, or any other loss or damage resulting from the transfer of data over communications networks and facilities, including the internet, and the Customer acknowledges that the Services and Documentation may be subject to limitations, delays and other problems inherent in the use of such communications facilities.</span></p><p class=Level2Number><span style='font-size:9.0pt'>7.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>This agreement shall not preventSilkFlo from entering into similar agreements with third parties, or from independently developing, using, selling or licensing documentation, products and/or services which are similar to those provided under this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>7.5<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo warrants that it has and will maintain all necessary licences, consents, and permissions necessary for the performance of its obligations under this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>7.6<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo shall follow its archiving procedures for Customer Data by scheduling back-ups on a regular basis, as dictated by SilkFlo in its sole discretion from time to time. In the event of any loss or damage to Customer Data, the Customer's sole and exclusive remedy againstSilkFlo shall be for SilkFlo to use reasonable commercial endeavours to restore the lost or damaged Customer Data from the latest back-up of such Customer Data maintained by SilkFlo in accordance with its archiving procedures. SilkFlo shall not be responsible for any loss, destruction, alteration or disclosure ofCustomer Data caused by any third party (except those third parties sub-contracted by SilkFlo to perform services related to Customer Data maintenance and back-up for which it shall remain fully liable).</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>8.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Customer's obligations</span></p><p class=Level2Number><span style='font-size:9.0pt'>8.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>provide SilkFlo with:</span></p><p class=Level4Number><span style='font-size:9.0pt'>(i)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>all necessary co-operation inrelation to this agreement; and</span></p><p class=Level4Number><span style='font-size:9.0pt'>(ii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>all necessary access to such information as may be required by SilkFlo;</span></p><p class=MsoBodyText3><span style='font-size:9.0pt'>in order to provide theServices, including but not limited to Customer Data, security access information and configuration services;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>without affecting its other obligations under this agreement, comply with all applicable laws and regulations with respect to its activities under this agreement;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>carry out all other Customer responsibilities set out in this agreement in a timely and efficient manner. In the event of any delays in the Customer's provision of such assistance as agreed by the parties, SilkFlo may adjust any agreed timetable or delivery schedule as reasonably necessary;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>ensure that the Users use theServices and the Documentation in accordance with the terms and conditions of this agreement and shall be responsible for any User's breach of this agreement;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(e)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>insofar as it has the capacity todo so, obtain and shall maintain all necessary licences, consents, and permissions necessary for SilkFlo, its contractors and agents to perform their obligations under this agreement, including without limitation the Services;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(f)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>ensure that its network and systems comply with the relevant specifications provided by SilkFlo from time to time;and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(g)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>be, to the extent permitted by law and except as otherwise expressly provided in this agreement, solely responsible for procuring, maintaining and securing its network connections and telecommunications links from its systems to SilkFlo's data centres, and all problems, conditions, delays, delivery failures and all other loss or damage arising from or relating to the Customer's network connections or telecommunications links or caused by the internet.</span></p><p class=Level2Number><span style='font-size:9.0pt'>8.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall own all right,title and interest in and to all of the Customer Data that is not personal data and shall have sole responsibility for the legality, reliability, integrity,accuracy and quality of all such Customer Data. The Customer grants to SilkFlo a perpetual, royalty-free, sub-licenseable, transferable, worldwide licence to anonymise all Customer Data and use such anonymised Customer Data for the purposes of improving the Services and Software and creating machine learning models to be used within its Services and Software.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>9.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Charges and payment</span></p><p class=Level2Number><span style='font-size:9.0pt'>9.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall pay theSubscription Fees to SilkFlo for the User Subscriptions in accordance with this clause </span><span style='font-size:9.0pt'>9</span><span style='font-size:9.0pt'>.</span></p><p class=Level2Number><span style='font-size:9.0pt'>9.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall on the EffectiveDate provide to SilkFlo valid, up-to-date and complete payment method details and/orapproved purchase order information acceptable to SilkFlo and any otherrelevant valid, up-to-date and complete contact and billing details and, if theCustomer provides:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>its payment method details toSilkFlo, the Customer hereby authorises SilkFlo to bill such payment method:</span></p><p class=Level4Number><span style='font-size:9.0pt'>(i)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>on the Effective Date for theSubscription Fees payable in respect of the Initial Subscription Term; and</span></p><p class=Level4Number><span style='font-size:9.0pt'>(ii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>subject to clause </span><span style='font-size:9.0pt'>14.1</span><span style='font-size:9.0pt'>, on eachanniversary of the Effective Date for the Subscription Fees payable in respectof the next Renewal Period;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>its approved purchase orderinformation to SilkFlo, SilkFlo shall invoice the Customer:</span></p><p class=Level4Number><span style='font-size:9.0pt'>(i)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>on the Effective Date for theSubscription Fees payable in respect of the Initial Subscription Term; and</span></p><p class=Level4Number><span style='font-size:9.0pt'>(ii)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>subject to clause </span><span style='font-size:9.0pt'>14.1</span><span style='font-size:9.0pt'>, prior to oron each anniversary of the Effective Date for the Subscription Fees payable inrespect of the next Renewal Period,</span></p><p class=MsoBodyText3><span style='font-size:9.0pt'>and the Customer shall payeach invoice on the date of such invoice.</span></p><p class=Level2Number><span style='font-size:9.0pt'>9.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>If SilkFlo has not received paymentwithin 14 days after the due date, and without prejudice to any other rightsand remedies of SilkFlo:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo may, without liability tothe Customer, disable the Customer's password, account and access to all orpart of the Services and SilkFlo shall be under no obligation to provide any orall of the Services while the invoice(s) concerned remain unpaid; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>interest shall accrue on a dailybasis on such due amounts at an annual rate equal to 4% over the then currentbase lending rate of SilkFlo's bankers in the UK from time to time, commencingon the due date and continuing until fully paid, whether before or afterjudgment.</span></p><p class=Level2Number><span style='font-size:9.0pt'>9.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>All amounts and fees stated orreferred to in this agreement:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>shall be payable in poundssterling;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>are, subject to clause </span><span style='font-size:9.0pt'>13.3(b)</span><span style='font-size:9.0pt'>,non-cancellable and non-refundable;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>are exclusive of value added tax,which shall be added to SilkFlo's invoice(s) at the appropriate rate.</span></p><p class=Level2Number><span style='font-size:9.0pt'>9.5<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo shall be entitled toincrease the Subscription Fees and the fees payable in respect of theadditional User Subscriptions purchased pursuant to clause </span><span style='font-size:9.0pt'>3.3</span><span style='font-size:9.0pt'> or clause </span><span style='font-size:9.0pt'>3.4</span><span style='font-size:9.0pt'> at any time bygiving at least 45 days’ prior notice, such increase to take effect from thestart of the next Renewal Period.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>10.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Proprietary rights</span></p><p class=Level2Number><span style='font-size:9.0pt'>10.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer acknowledges andagrees that SilkFlo and/or its licensors own all intellectual property rightsin the Services and the Documentation. Except as expressly stated herein, thisagreement does not grant the Customer any rights to, under or in, any patents,copyright, database right, trade secrets, trade names, trade marks (whetherregistered or unregistered), or any other rights or licences in respect of theServices or the Documentation.</span></p><p class=Level2Number><span style='font-size:9.0pt'>10.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo confirms that it has allthe rights in relation to the Services and the Documentation that are necessaryto grant all the rights it purports to grant under, and in accordance with, theterms of this agreement.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>11.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Confidentiality</span></p><p class=Level2Number><span style='font-size:9.0pt'>11.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Each party may be given access toConfidential Information from the other party in order to perform itsobligations under this agreement. A party's Confidential Information shall notbe deemed to include information that:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>is or becomes publicly known otherthan through any act or omission of the receiving party;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>was in the other party's lawfulpossession before the disclosure;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>is lawfully disclosed to thereceiving party by a third party without restriction on disclosure; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>is independently developed by thereceiving party, which independent development can be shown by writtenevidence.</span></p><p class=Level2Number><span style='font-size:9.0pt'>11.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Subject to clause </span><span style='font-size:9.0pt'>11.4</span><span style='font-size:9.0pt'>, each partyshall hold the other's Confidential Information in confidence and not make theother's Confidential Information available to any third party, or use theother's Confidential Information for any purpose other than the implementationof this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>11.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Each party shall take allreasonable steps to ensure that the other's Confidential Information to whichit has access is not disclosed or distributed by its employees or agents inviolation of the terms of this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>11.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>A party may disclose ConfidentialInformation to the extent such Confidential Information is required to bedisclosed by law, by any governmental or other regulatory authority or by acourt or other authority of competent jurisdiction, provided that, to theextent it is legally permitted to do so, it gives the other party as muchnotice of such disclosure as possible and, where notice of disclosure is notprohibited and is given in accordance with this clause </span><span style='font-size:9.0pt'>11.4</span><span style='font-size:9.0pt'>, it takes intoaccount the reasonable requests of the other party in relation to the contentof such disclosure.</span></p><p class=Level2Number><span style='font-size:9.0pt'>11.5<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer acknowledges thatdetails of the Services, and the results of any performance tests of theServices, constitute SilkFlo's Confidential Information.</span></p><p class=Level2Number><span style='font-size:9.0pt'>11.6<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo acknowledges that theCustomer Data is the Confidential Information of the Customer.</span></p><p class=Level2Number><span style='font-size:9.0pt'>11.7<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The above provisions of this clause</span><span style='font-size:9.0pt'>11</span><span style='font-size:9.0pt'>shall survive termination of this agreement, however arising.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>12.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Indemnity</span></p><p class=Level2Number><span style='font-size:9.0pt'>12.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall defend,indemnify and hold harmless SilkFlo against claims, actions, proceedings,losses, damages, expenses and costs (including without limitation court costsand reasonable legal fees) arising out of or in connection with the Customer'suse of the Services and/or Documentation, provided that:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer is given prompt noticeof any such claim;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo provides reasonableco-operation to the Customer in the defence and settlement of such claim, atthe Customer's expense; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer is given soleauthority to defend or settle the claim.</span></p><p class=Level2Number><span style='font-size:9.0pt'>12.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo shall defend the Customer,its officers, directors and employees against any claim that the Customer's useof the Services or Documentation in accordance with this agreement infringesany United Kingdom patent effective as of the Effective Date, copyright, trademark, database right or right of confidentiality, and shall indemnify theCustomer for any amounts awarded against the Customer in judgment or settlementof such claims, provided that:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo is given prompt notice ofany such claim;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer does not make anyadmission, or otherwise attempt to compromise or settle the claim and providesreasonable co-operation to SilkFlo in the defence and settlement of such claim,at SilkFlo's expense; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo is given sole authority todefend or settle the claim.</span></p><p class=Level2Number><span style='font-size:9.0pt'>12.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>In the defence or settlement of anyclaim, SilkFlo may procure the right for the Customer to continue using theServices, replace or modify the Services so that they become non-infringing or,if such remedies are not reasonably available, terminate this agreement on 2Business Days' notice to the Customer without any additional liability orobligation to pay liquidated damages or other additional costs to the Customer.</span></p><p class=Level2Number><span style='font-size:9.0pt'>12.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>In no event shall SilkFlo, its employees,agents and sub-contractors be liable to the Customer to the extent that thealleged infringement is based on:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>a modification of the Services orDocumentation by anyone other than SilkFlo; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer's use of the Servicesor Documentation in a manner contrary to the instructions given to the Customerby SilkFlo; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer's use of the Servicesor Documentation after notice of the alleged or actual infringement fromSilkFlo or any appropriate authority.</span></p><p class=Level2Number><span style='font-size:9.0pt'>12.5<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The foregoing and clause </span><span style='font-size:9.0pt'>13.3(b)</span><span style='font-size:9.0pt'> state theCustomer's sole and exclusive rights and remedies, and SilkFlo's (includingSilkFlo's employees', agents' and sub-contractors') entire obligations andliability, for infringement of any patent, copyright, trade mark, databaseright or right of confidentiality.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>13.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Limitation of liability</span></p><p class=Level2Number><span style='font-size:9.0pt'>13.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Except as expressly andspecifically provided in this agreement:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Customer assumes soleresponsibility for results obtained from the use of the Services and theDocumentation by the Customer, and for conclusions drawn from such use. SilkFloshall have no liability for any damage caused by errors or omissions in anyinformation, instructions or scripts provided to SilkFlo by the Customer inconnection with the Services, or any actions taken by SilkFlo at the Customer'sdirection;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>all warranties, representations,conditions and all other terms of any kind whatsoever implied by statute orcommon law are, to the fullest extent permitted by applicable law, excludedfrom this agreement; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the Services and the Documentationare provided to the Customer on an &quot;as is&quot; basis.</span></p><p class=Level2Number><span style='font-size:9.0pt'>13.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Nothing in this agreement excludesthe liability of SilkFlo:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>for death or personal injury causedby SilkFlo's negligence; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>for fraud or fraudulentmisrepresentation.</span></p><p class=Level2Number><span style='font-size:9.0pt'>13.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Subject to clause </span><span style='font-size:9.0pt'>13.1</span><span style='font-size:9.0pt'> and clause </span><span style='font-size:9.0pt'>13.2</span><span style='font-size:9.0pt'>:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo shall not be liable whetherin tort (including for negligence or breach of statutory duty), contract,misrepresentation, restitution or otherwise for any loss of profits, loss ofbusiness, depletion of goodwill and/or similar losses or loss or corruption ofdata or information, or pure economic loss, or for any special, indirect orconsequential loss, costs, damages, charges or expenses however arising underthis agreement; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo's total aggregate liabilityin contract (including in respect of the indemnity at clause </span><span style='font-size:9.0pt'>12.2</span><span style='font-size:9.0pt'>), tort(including negligence or breach of statutory duty), misrepresentation,restitution or otherwise, in any 12 month period starting on the Effective Dateor any anniversary of it (each a <b>Contract Year</b>), arising in connectionwith the performance or contemplated performance of this agreement, shall belimited to the total Subscription Fees paid by the Customer to SilkFlo during thatContract Year.</span></p><p class=Level2Number><span style='font-size:9.0pt'>13.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Nothing in this agreement excludesthe liability of the Customer for any breach, infringement or misappropriationof SilkFlo’s Intellectual Property Rights.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>14.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Term and termination</span></p><p class=Level2Number><span style='font-size:9.0pt'>14.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>This agreement shall, unless otherwise terminated as provided in this clause </span><span style='font-size:9.0pt'>14</span><span style='font-size:9.0pt'>, commence on the Effective Date and shall continue for the Initial Subscription Term and,thereafter, this agreement shall be automatically renewed for successive periods equal to the Initial Subscription Term (each a <b>Renewal Period</b>),unless:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>either party notifies the other party of termination, in writing, at least 30 days before the end of theInitial Subscription Term or any Renewal Period, in which case this agreement shall terminate upon the expiry of the applicable Initial Subscription Term orRenewal Period; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>otherwise terminated in accordance with the provisions of this agreement;</span></p><p class=MsoBodyText2><span style='font-size:9.0pt'>and the InitialSubscription Term together with any subsequent Renewal Periods shall constitute the <b>Subscription Term</b>.</span></p><p class=Level2Number><span style='font-size:9.0pt'>14.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Without affecting any other right or remedy available to it, either party may terminate this agreement with immediate effect by giving written notice to the other party if:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the other party fails to pay any amount due under this agreement on the due date for payment and remains in default not less than 14 days after being notified in writing to make such payment;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the other party commits a material breach of any other term of this agreement and (if such breach is remediable)fails to remedy that breach within a period of 14 days after being notified in writing to do so;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the other party suspends, or threatens to suspend, payment of its debts or is unable to pay its debts as they fall due or admits inability to pay its debts or is deemed unable to pay its debts within the meaning of section 123 of the Insolvency Act 1986;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the other party commences negotiations with all or any class of its creditors with a view to rescheduling any of its debts, or makes a proposal for or enters into any compromise or arrangement with its creditors other than for the sole purpose of a scheme fora solvent amalgamation of that other party with one or more other companies or the solvent reconstruction of that other party;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(e)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the other party applies to court for, or obtains, a moratorium under Part A1 of the Insolvency Act 1986;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(f)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>a petition is filed, a notice is given, a resolution is passed, or an order is made, for or in connection with the winding up of that other party other than for the sole purpose of a scheme for a solvent amalgamation of that other party with one or more other companies or the solvent reconstruction of that other party;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(g)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>an application is made to court, oran order is made, for the appointment of an administrator, or if a notice of intention to appoint an administrator is given or if an administrator is appointed, over the other party (being a company, partnership or limited liability partnership);</span></p><p class=Level3Number><span style='font-size:9.0pt'>(h)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the holder of a qualifying floating charge over the assets of that other party (being a company or limited liability partnership) has become entitled to appoint or has appointed an administrative receiver;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(i)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>a person becomes entitled to appoint a receiver over the assets of the other party or a receiver is appointed over the assets of the other party;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(j)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>a creditor or encumbrancer of the other party attaches or takes possession of, or a distress, execution,sequestration or other such process is levied or enforced on or sued against,the whole or any part of the other party's assets and such attachment or process is not discharged within 14 days;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(k)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>any event occurs, or proceeding is taken, with respect to the other party in any jurisdiction to which it is subject that has an effect equivalent or similar to any of the events mentioned in clause </span><span style='font-size:9.0pt'>14.2(c)</span><span style='font-size:9.0pt'> to <i>clause14.2(j)</i> (inclusive);</span></p><p class=Level3Number><span style='font-size:9.0pt'>(l)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the other party suspends or ceases,or threatens to suspend or cease, carrying on all or a substantial part of its business; or</span></p><p class=Level3Number><span style='font-size:9.0pt'>(m)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>the other party's financial position deteriorates so far as to reasonably justify the opinion that its ability to give effect to the terms of this agreement is in jeopardy.</span></p><p class=Level2Number><span style='font-size:9.0pt'>14.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>On termination of this agreement for any reason:</span></p><p class=Level3Number><span style='font-size:9.0pt'>(a)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>all licences granted under this agreement shall immediately terminate and the Customer shall immediately cease all use of the Services and/or the Documentation;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(b)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>each party shall return and make no further use of any equipment, property, Documentation and other items (and all copies of them) belonging to the other party;</span></p><p class=Level3Number><span style='font-size:9.0pt'>(c)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo may destroy or otherwise dispose of any of the Customer Data in its possession unless SilkFlo receives,no later than ten days after the effective date of the termination of this agreement, a written request for the delivery to the Customer of the then most recent back-up of the Customer Data. SilkFlo shall use reasonable commercial endeavours to either deliver or make available for download the back-up to theCustomer within 30 days of its receipt of such a written request, provided that the Customer has, at that time, paid all fees and charges outstanding at and resulting from termination (whether or not due at the date of termination). TheCustomer shall pay all reasonable expenses incurred by SilkFlo in returning or disposing of Customer Data; and</span></p><p class=Level3Number><span style='font-size:9.0pt'>(d)<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>any rights, remedies, obligations or liabilities of the parties that have accrued up to the date of termination,including the right to claim damages in respect of any breach of the agreement which existed at or before the date of termination shall not be affected or prejudiced.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>15.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Force majeure</span></p><p class=BodyText1><span style='font-size:9.0pt'>SilkFlo shall have no liability to the Customer under this agreement if it is prevented from or delayed in performing its obligations under this agreement, or from carrying on its business, by acts, events, omissions or accidents beyond its reasonable control, including, without limitation, strikes, lock-outs or other industrial disputes (whether involving the workforce of SilkFlo or any other party),failure of a utility service or transport or telecommunications network, act ofGod, war, riot, civil commotion, malicious damage, compliance with any law or governmental order, rule, regulation or direction, accident, breakdown of plant or machinery, fire, flood, storm or default of suppliers or sub-contractors,provided that the Customer is notified of such an event and its expected duration.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>16.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Conflict</span></p><p class=BodyText1><span style='font-size:9.0pt'>If there is an inconsistency between any of the provisions in the main body of this agreement and theSchedules, the provisions in the main body of this agreement shall prevail.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>17.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Variation</span></p><p class=BodyText1><span style='font-size:9.0pt'>No variation of this agreement shall be effective unless it is in writing and signed by the parties (or their authorised representatives).</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>18.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Waiver</span></p><p class=BodyText1><span style='font-size:9.0pt'>No failure or delay by a party to exercise any right or remedy provided under this agreement or by law shall constitute a waiver of that or any other right or remedy, nor shall it prevent or restrict the further exercise of that or any other right or remedy. No single or partial exercise of such right or remedy shall prevent or restrict the further exercise of that or any other right or remedy.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>19.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Rights and remedies</span></p><p class=BodyText1><span style='font-size:9.0pt'>Except as expressly provided in this agreement, the rights and remedies provided under this agreement are in addition to, and not exclusive of, any rights or remedies provided by law.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>20.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Severance</span></p><p class=Level2Number><span style='font-size:9.0pt'>20.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>If any provision or part-provision of this agreement is or becomes invalid, illegal or unenforceable, it shall be deemed deleted, but that shall not affect the validity and enforceability of the rest of this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>20.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>If any provision or part-provision of this agreement is deemed deleted under clause </span><span style='font-size:9.0pt'>20.1</span><span style='font-size:9.0pt'> the parties shall negotiate in good faith to agree a replacement provision that, to the greatest extent possible, achieves the intended commercial result of the original provision.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>21.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Entire agreement</span></p><p class=Level2Number><span style='font-size:9.0pt'>21.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>This agreement constitutes the entire agreement between the parties and supersedes and extinguishes all previous agreements, promises, assurances, warranties, representations and understandings between them, whether written or oral, relating to its subject matter.</span></p><p class=Level2Number><span style='font-size:9.0pt'>21.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Each party acknowledges that in entering into this agreement it does not rely on, and shall have no remedies in respect of, any statement, representation, assurance or warranty (whether made innocently or negligently) that is not set out in this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>21.3<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Each party agrees that it shall have no claim for innocent or negligent misrepresentation or negligent misstatement based on any statement in this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>21.4<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Nothing in this clause shall limit or exclude any liability for fraud.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>22.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Assignment</span></p><p class=Level2Number><span style='font-size:9.0pt'>22.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>The Customer shall not, without the prior written consent of SilkFlo, assign, transfer, charge, sub-contract ordeal in any other manner with all or any of its rights or obligations under this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>22.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>SilkFlo may at any time assign,transfer, charge, sub-contract or deal in any other manner with all or any of its rights or obligations under this agreement.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>23.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>No partnership or agency</span></p><p class=BodyText1><span style='font-size:9.0pt'>Nothing in this agreement is intended to or shall operate to create a partnership between the parties, or authorise either party to act as agent for the other, and neither party shall have the authority to act in the name or on behalf of or otherwise to bind the other in any way (including, but not limited to, the making of any representation or warranty, the assumption of any obligation or liability and the exercise of any right or power).</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>24.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Third party rights</span></p><p class=BodyText1><span style='font-size:9.0pt'>This agreement does not confer any rights on any person or party (other than the parties to this agreement and, where applicable, their successors and permitted assigns) pursuant to theContracts (Rights of Third Parties) Act 1999.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>25.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Notices</span></p><p class=Level2Number><span style='font-size:9.0pt'>25.1<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>Any notice required to be given under this agreement shall be in writing and shall be delivered by hand or sent by pre-paid first-class post or recorded delivery post to the other party at its address set out in this agreement, or such other address as may have been notified by that party for such purposes, or sent by fax to the other party's fax number as set out in this agreement.</span></p><p class=Level2Number><span style='font-size:9.0pt'>25.2<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt'>A notice delivered by hand shall be deemed to have been received when delivered (or if delivery is not in business hours, at 9 am on the first business day following delivery). A correctly addressed notice sent by pre-paid first-class post or recorded delivery post shall be deemed to have been received at the time at which it would have been delivered in the normal course of post. A notice sent by fax shall be deemed to have been received at the time of transmission (as shown by the timed printout obtained by the sender).</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>26.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Governing law</span></p><p class=BodyText1><span style='font-size:9.0pt'>This agreement and any dispute or claim arising out of or in connection with it or its subject matter or formation (including non-contractual disputes or claims) shall be governed by and construed in accordance with the law of England and Wales.</span></p><p class=Level1Heading><span style='font-size:9.0pt;font-variant:normal !important;text-decoration:none'>27.<span style='font:7.0pt \"Times New Roman\"'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></span><span style='font-size:9.0pt;font-variant:normal !important;text-transform:uppercase; text-decoration:underline;'>Jurisdiction</span></p><p class=BodyText1><span style='font-size:9.0pt'>Each party irrevocably agrees that the courts of England and Wales shall have exclusive jurisdiction to settle any dispute or claim arising out of or in connection with this agreement or its subject matter or formation (including non-contractual disputes or claims).</span></p></div>";

            core = await unitOfWork.ApplicationPages.SingleOrDefaultAsync(x => x.URL == url);
            if (core == null)
            {
                core = new Data.Core.Domain.Application.Page
                {
                    URL = url,
                    Name = name,
                    Sort = sort,
                    IsMenuItem = false,
                    Text = text,
                };
                await unitOfWork.ApplicationPages.AddAsync(core);
                core.Id = id; // Overwrite the generated Id.
            }
            else
                core.Id = id;

        }

        public static async Task<Data.Core.Domain.Business.Client> SilkFloClientAsync(
            Data.Core.IUnitOfWork unitOfWork,
            Data.Core.Domain.User powerUser)
        {
            try
            {
                var core = await unitOfWork.BusinessClients
                                           .SingleOrDefaultAsync(x => x.Name == Data.Core.Settings.ApplicationName);

                if (core == null)
                {
                    var currency = await unitOfWork.ShopCurrencies.SingleOrDefaultAsync(x => x.Symbol == "£");

                    core = new Data.Core.Domain.Business.Client
                    {
                        Name = Data.Core.Settings.ApplicationName,
                        LanguageId = "en-gb",
                        TypeId = Data.Core.Enumerators.ClientType.ResellerAgency45.ToString(),
                        CurrencyId = currency.Id,
                        Website = "SilkFlo.com",
                        Address1 = "1 SilkFlo Campus",
                        PostCode = "123",
                        AccountOwnerId = powerUser.Id,
                        IndustryId = "SoftwareEngineering",
                        IsActive = true
                    };



                    await PracticeData.CreatePracticeAccountAsync(
                        core,
                        unitOfWork, 
                        true,
                        powerUser);

                    await unitOfWork.BusinessClients.AddAsync(core);
                }

#if TEST || DEBUG
                var role = await unitOfWork.Roles.SingleOrDefaultAsync(x =>
                    x.Name.Equals("agency user", StringComparison.OrdinalIgnoreCase));

                if (role == null)
                    return core;


                var user = await PracticeData.AddUserAndRole(
                    Settings.PracticeAccountEmailSuffix,
                    core.Id,
                    "Agency",
                    "User",
                    new List<Data.Core.Domain.Role>() {role},
                    1,
                    unitOfWork);

                if (user != null)
                    await unitOfWork.AddAsync(user);
#endif

                return core;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public static async Task BuiltInBusinessRoleAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var name = "Employee Idea Submitter";
            var id = "Employee_Idea_Submitter";
            var sort = 0;
            var description = "Role automatically assigned to any user submitting an idea. Give access to contribute with documentation.";

            var core = await unitOfWork.BusinessRoles.GetAsync(id);
            if(core == null)
            {
                core = new Data.Core.Domain.Business.BusinessRole { Name = name, Id = id, Sort = sort, Description = description, IsBuiltIn = true, ClientId = ""};
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            name = "Process Owner";
            id = "Process_Owner";
            sort++;
            description = "";

            core = await unitOfWork.BusinessRoles.GetAsync(id);
            if (core == null)
            {
                core = new Data.Core.Domain.Business.BusinessRole { Name = name, Id = id, Sort = sort, Description = description, IsBuiltIn = true, ClientId = "" };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            name = "Project Manager";
            id = "Project_Manager";
            sort++;
            description = "";

            core = await unitOfWork.BusinessRoles.GetAsync(id);
            if (core == null)
            {
                core = new Data.Core.Domain.Business.BusinessRole { Name = name, Id = id, Sort = sort, Description = description, IsBuiltIn = true, ClientId = "" };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }

        public static async Task SharedIdeaAuthorizationAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.IdeaAuthorization.EditAbout.ToString();
            var name = "Edit the About description of an idea";
            var description = "<p>Access to the editable view of the About space of an idea or automation where you are assigned as a collaborator.</p><p>Here you can edit the information available in the Overview, High-level assessment, Detailed Assessment, Benefits, and Media sections related to the idea or automation.</p>";
            var sort = 0;
            var core = await unitOfWork.SharedIdeaAuthorisations.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.IdeaAuthorisation { Id = id, Name = name, Description = description, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.IdeaAuthorization.EditAdvancedSettings.ToString();
            name = "Edit the Advanced Settings of an idea";
            description = "<p>Access to edit the Priority and Development Type fields of an idea or automation where you are assigned as a collaborator. This action can be done in any phase/status by accessing the Idea or Automation profile page &gt; About section &gt; edit mode.</p>";
            sort++;
            core = await unitOfWork.SharedIdeaAuthorisations.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.IdeaAuthorisation { Id = id, Name = name, Description = description, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.IdeaAuthorization.StageAndStatus.ToString();
            name = "Update the Stage and Status of an idea during implementation";
            description = "<p>Access to update the Phase and Status of an idea or automation where you are assigned as a collaborator. This action can be done by accessing the Idea or Automation profile page &gt; About section &gt; edit mode.</p><p>Note: Updates can be applied after the idea is pushed in the Qualification Phase - status Approved.</p>";
            sort++;
            core = await unitOfWork.SharedIdeaAuthorisations.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.IdeaAuthorisation { Id = id, Name = name, Description = description, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            id = Data.Core.Enumerators.IdeaAuthorization.ViewCostBenefit.ToString();
            name = "View the Cost-Benefit Analysis of an idea";
            description = "<p>Access to view the dashboards and project plan displayed in the Cost-Benefit Analysis page of an idea or automation where you are assigned as a collaborator.</p>";
            sort++;
            core = await unitOfWork.SharedIdeaAuthorisations.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.IdeaAuthorisation { Id = id, Name = name, Description = description, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }


            id = Data.Core.Enumerators.IdeaAuthorization.EditCostBenefit.ToString();
            name = "Edit the Cost-Benefit Analysis of an idea";
            description = "<p>Access to edit the project plan and the cost types applied in the Cost-Benefit Analysis of an idea or automation where you are assigned as a collaborator.</p>";
            sort++;
            core = await unitOfWork.SharedIdeaAuthorisations.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.IdeaAuthorisation { Id = id, Name = name, Description = description, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }


            id = Data.Core.Enumerators.IdeaAuthorization.EditDocumentation.ToString();
            name = "Edit the documentation of an idea";
            description = "<p>Access to view existing documentation and also upload new files available in the Documentation space of an idea or automation where you are assigned as a collaborator.</p><p>Here you can add / embed / update / remove documents related to the idea or automation.</p>";
            sort++;
            core = await unitOfWork.SharedIdeaAuthorisations.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.IdeaAuthorisation { Id = id, Name = name, Description = description, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }


            id = Data.Core.Enumerators.IdeaAuthorization.ManageCollaborators.ToString();
            name = "Manage the Collaborators an idea";
            description = "<p>Access to the Collaborators space of an idea or automation where you are assigned as a collaborator.</p><p>Here you can add / update / remove collaborators related to the idea or automation.</p>";
            sort++;
            core = await unitOfWork.SharedIdeaAuthorisations.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.IdeaAuthorisation { Id = id, Name = name, Description = description, Sort = sort };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }


        public static async Task SharedClientTypeAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var id = Data.Core.Enumerators.ClientType.Client39.ToString();
            var name = "Client";
            var description = "";

            var core = await unitOfWork.SharedClientTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ClientType { Id = id, Name = name, Description = description };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }


            id = Data.Core.Enumerators.ClientType.ReferrerAgency41.ToString();
            name = "Referrer Agency";
            description = "Lesser agency - Has access to a practice account to demo SilkFlo to end users.";

            core = await unitOfWork.SharedClientTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ClientType { Id = id, Name = name, Description = description };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }


            id = Data.Core.Enumerators.ClientType.ResellerAgency45.ToString();
            name = "Reseller Agency";
            description = "Major partner/ally - Has access to a practice account to demo SilkFlo to end users. Also has access to their own Tenant so that they can use SilkFlo inhouse. Also has access to tenant management, agency dashboard.";

            core = await unitOfWork.SharedClientTypes.SingleOrDefaultAsync(x => x.Id == id);
            if (core == null)
            {
                core = new Data.Core.Domain.Shared.ClientType { Id = id, Name = name, Description = description };
                await unitOfWork.AddAsync(core);
                core.Id = id;
            }

            await unitOfWork.CompleteAsync();
        }


        public static async Task ShopDiscountsAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            var name = "Bronze";
            var core = await unitOfWork.ShopDiscounts.SingleOrDefaultAsync(x => x.Name == name);
            if (core == null)
            {
                core = new Data.Core.Domain.Shop.Discount
                {
                    Name = name,
                    PercentReseller = 10,
                    PercentReferrer = 5,
                    Max = 50000,
                    DescriptionReseller = "Up to £50K"
                };
                await unitOfWork.AddAsync(core);
            }

            name = "Silver";

            core = await unitOfWork.ShopDiscounts.SingleOrDefaultAsync(x => x.Name == name);
            if (core == null)
            {
                core = new Data.Core.Domain.Shop.Discount
                {
                    Name = name,
                    PercentReseller = 18,
                    PercentReferrer = 5,
                    Min = 50000,
                    Max = 100000,
                    DescriptionReseller = "After £50K"
                };
                await unitOfWork.AddAsync(core);
            }

            name = "Gold";

            core = await unitOfWork.ShopDiscounts.SingleOrDefaultAsync(x => x.Name == name);
            if (core == null)
            {
                core = new Data.Core.Domain.Shop.Discount
                {
                    Name = name,
                    PercentReseller = 25,
                    PercentReferrer = 10,
                    Min = 100000,
                    DescriptionReseller = "After £100K"
                };
                await unitOfWork.AddAsync(core);
            }
        }

        public async Task Insert(Data.Core.IUnitOfWork unitOfWork, Data.Core.Domain.User powerUser)
        {
            try
            {
                await CurrenciesAsync(unitOfWork); await unitOfWork.CompleteAsync();

                await SharedPeriodAsync(unitOfWork); await unitOfWork.CompleteAsync();
                var subscriptionData = new SubscriptionData();
                await subscriptionData.ShopProductsAsync(unitOfWork); await unitOfWork.CompleteAsync();

                //ToDo: Get Prices from Stripe
                //await SubscriptionData.UpdatePricesAsync(unitOfWork, Settings.StripeTestKey);
                //await SubscriptionData.UpdatePricesAsync(unitOfWork, Settings.StripeLiveKey);

                await PagesAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await CountriesAsync(unitOfWork); await unitOfWork.CompleteAsync();


                await SharedIdeaAuthorizationAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await SharedIndustriesAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await AverageNumberOfStepAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await ApplicationStabilityAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await AutomationGoalAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await DataInputPercentOfStructuredAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await ProcessStabilityAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await InputAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await InputDataStructureAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await ProcessPeakAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await RuleAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await StageAndStatusAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await SubmissionPathsAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await TaskFrequencyAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await NumberOfWaysToCompleteProcessAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await DecisionCountAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await DecisionDifficultyAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await DocumentationPresentAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await LanguagesAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await BuiltInBusinessRoleAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await SharedClientTypeAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await SharedCostTypeAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await SharedAutomationTypeAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await ShopDiscountsAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await CompanySizesAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await JobLevelsAsync(unitOfWork); await unitOfWork.CompleteAsync();
                await HotSpots(unitOfWork); await unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }




        public static async Task CompanySizesAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            await CompanySizeAsync(unitOfWork, "self", "Self Employed", 10);
            await CompanySizeAsync(unitOfWork, "1-10", "1-10", 20);
            await CompanySizeAsync(unitOfWork, "10-50", "10-50", 30);
            await CompanySizeAsync(unitOfWork, "50-100", "50-100", 40);
            await CompanySizeAsync(unitOfWork, "100-200", "100-200", 50);
            await CompanySizeAsync(unitOfWork, "200-500", "200-500", 60);
            await CompanySizeAsync(unitOfWork, "500-1000", "500-1000", 70);
            await CompanySizeAsync(unitOfWork, "1000-5000", "1000-5000", 80);
            await CompanySizeAsync(unitOfWork, "5000-10000", "5000-10000", 90);
            await CompanySizeAsync(unitOfWork, "10000+", "10000+", 100);
        }

        public static async Task CompanySizeAsync(
                Data.Core.IUnitOfWork unitOfWork,
                string id,
                string name,
                int sort)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(id));



            var core = await unitOfWork.CRMCompanySizes.GetAsync(id);
            if(core == null)
            {
                core = new Data.Core.Domain.CRM.CompanySize
                {
                    Name = name,
                    Sort = sort
                };

                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }



        public static async Task JobLevelsAsync(Data.Core.IUnitOfWork unitOfWork)
        {
            await JobLevelAsync(unitOfWork, "CXO", "CXO", 10);
            await JobLevelAsync(unitOfWork, "VP", "VP", 20);
            await JobLevelAsync(unitOfWork, "Director", "Director", 30);
            await JobLevelAsync(unitOfWork, "Manager", "Manager", 40);
            await JobLevelAsync(unitOfWork, "IndividualContributor", "Individual Contributor", 50);
            await JobLevelAsync(unitOfWork, "Developer", "Developer", 60);
            await JobLevelAsync(unitOfWork, "Student", "Student", 70);
            await JobLevelAsync(unitOfWork, "Other", "Other", 80);
        }

        public static async Task JobLevelAsync(
            Data.Core.IUnitOfWork unitOfWork,
            string id,
            string name,
            int sort)
        {
            if (unitOfWork == null)
                throw new ArgumentNullException(nameof(unitOfWork));

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentNullException(nameof(id));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(id));



            var core = await unitOfWork.CRMJobLevels.GetAsync(id);
            if (core == null)
            {
                core = new Data.Core.Domain.CRM.JobLevel
                {
                    Name = name,
                    Sort = sort
                };

                await unitOfWork.AddAsync(core);
                core.Id = id;
            }
        }


        /// <summary>
        /// Example: silkflo-hotspot="SpecialSilkFloSection"
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static async Task HotSpots(Data.Core.IUnitOfWork unitOfWork)
        {
            await HotSpot(unitOfWork, 
                "SubscriptionSettings", 
                "The client subscription can only be changed or replaced with a free trial on first sign in if the subscription was not purchased.", 
                "10rem");

            await HotSpot(unitOfWork,
                "SpecialSilkFloSection", 
                "This section is only visible to SilkFlo admin users.",
                "10rem");

            await HotSpot(unitOfWork,
                "SiderBar-Explore",
                "Explore all of the ideas, automations and people within your workspace.",
                "10rem");

            await HotSpot(unitOfWork,
                "SiderBar-Workshop",
                "Manage all of the ideas in your workspace and track them through to production.",
                "10rem");



            await HotSpot(unitOfWork,
                "Total-Ideas",
                "Total of ideas in the Review, Assess, Decide stages.",
                "10rem");

            await HotSpot(unitOfWork,
                "One-Time-Cost",
                "A summary of the initial costs of the implementation, e.g. total cost for the implementation, people costs, etc.",
                "10rem");

            await HotSpot(unitOfWork,
                "Running-Costs",
                "A summary of the costs per year for the entire lifetime of the automated idea, e.g. number of items/licenses, maintenance, infrastructure and support.",
                "10rem");

            await HotSpot(unitOfWork,
                "Benefits-Annual",
                "Estimated benefits of the automated process per year.",
                "10rem");

            await HotSpot(unitOfWork,
                "Time-to-ROI",
                "Time until the automation breaks-even, based on one-time costs and running costs.",
                "10rem");

            await HotSpot(unitOfWork,
                "Total-ideas-in-review",
                "Total of ideas in Review.",
                "10rem");

            await HotSpot(unitOfWork,
                "Total-ideas-in-access",
                "Total of ideas in Access.",
                "10rem");

            await HotSpot(unitOfWork,
                "Total-ideas-in-review-Awaiting-Review",
                "Total of ideas awaiting review.",
                "10rem");

            await HotSpot(unitOfWork,
                "Total-In-Build",
                "Total of ideas in the Build stage.",
                "10rem");

            await HotSpot(unitOfWork,
                "Total-Deployed",
                "Total of ideas deployed to production.",
                "10rem");

            await HotSpot(unitOfWork,
                "Deployed-Benefits",
                "Total estimated benefits of all automations Deployed.",
                "10rem");

            await HotSpot(unitOfWork,
                "My-Ideas",
                "Total ideas you have submitted.",
                "10rem");

            await HotSpot(unitOfWork,
                "My-Total-In-Build",
                "Total of your ideas that are being built.",
                "10rem");

            await HotSpot(unitOfWork,
                "My-Total-Deployed",
                "Total of your ideas that are live!",
                "10rem");

            await HotSpot(unitOfWork,
                "My-Collaborations",
                "Total of the ideas you are collaborating on.",
                "10rem");

            await HotSpot(unitOfWork,
                "Automation-Program-Performance",
                "Automation Program Performance chart shows the progression of the number of automations over a period of time.",
                "10rem");


            await HotSpot(unitOfWork,
                "My-Ideas-Cards",
                "All of the ideas you have submitted.",
                "10rem");



            await HotSpot(unitOfWork,
                "How-structured-is-the-input-data",
                "Structured Data: data stored in a standard, highly organized way and is presented in a predictable pattern, e.g. Excel.</br>Unstructured Data: data in various, complex forms, e.g emails, scanned images, and handwritten text.",
                "10rem");

            await HotSpot(unitOfWork,
                "Are-there-any-upcoming-changes-to-the-process-within-6-months",
                "Will there be any changes to the 'As Is' process and the way it is performed within the next 6 months?",
                "10rem");


            await HotSpot(unitOfWork,
                "How-satisfied-are-you-with-the-current-manual-process-for",
                "Feel free to express how the process makes you feel. From frustrated, to satisfied.",
                "10rem");

            await HotSpot(unitOfWork,
                "Pain-Point",
                "Describe why the process is inefficient or frustrating.",
                "10rem");

            await HotSpot(unitOfWork,
                "Negative-Impact",
                "Describe the negative impact this process is having.",
                "10rem");



            await HotSpot(unitOfWork,
                "Task-Process-Frequency",
                "How often is the task/process performed: Daily, weekly, monthly, yearly?",
                "10rem");

            await HotSpot(unitOfWork,
                "Activity-Volume-Average",
                "What is the average volume of the task per the frequency above?",
                "10rem");

            await HotSpot(unitOfWork,
                "Average-Processing-Time-Transaction",
                "What is the average time it takes to perform the process?",
                "10rem");

            await HotSpot(unitOfWork,
                "Average-Error-Rate",
                "What is the average error rate of the process, as a % of total volume?",
                "10rem");

            await HotSpot(unitOfWork,
                "Average-Rework-Time",
                "What is the average time it takes (in minutes) to rework an error, per transaction.",
                "10rem");

            await HotSpot(unitOfWork,
                "Average-Work-to-be-Reviewed",
                "What is the average amount of work to be reviewed/audited, as a % of total volume?",
                "10rem");

            await HotSpot(unitOfWork,
                "Average-Review-or-Audit-Time-Transaction",
                "What is the average time it takes (in minutes) to review/audit, per transaction",
                "10rem");

            await HotSpot(unitOfWork,
                "Allow-Guest-Sign-In",
                "When <i>Allow Guest Sign In</i> is selected user accounts <span class=\"text-warning\">other than {domain email}</span> can be used as sign in emails.<br><span class=\"text-warning\">Note:</span> Guest users can only be assigned to the <span class=\"text-info\">Standard User</span> role. ",
                "10rem");

            await HotSpot(unitOfWork,
                "Manage-Account-Owner",
                "Only users with email addresses suffixed with <span class=\"text-warning\">{domain email}</span> can become account managers.",
                "10rem");


            await HotSpot(unitOfWork,
                "Standard-User-Role",
                "You can have unlimited standard users who can submit ideas.",
                "10rem");


            await HotSpot(unitOfWork,
                "Feasibility Gauge",
                "Score based how feasible the process is for automation.",
                "10rem");



            await HotSpot(unitOfWork,
                "Readiness Gauge",
                "Score based on how ready the underlying data/systems are for automation.",
                "10rem");



            await HotSpot(unitOfWork,
                "Idea Score Gauge",
                "Overall score representing how good your idea is for automation.",
                "10rem");

            //
            //await HotSpot(unitOfWork,
            //    "",
            //    "",
            //    "10rem");
        }

        public static async Task HotSpot(
            Data.Core.IUnitOfWork unitOfWork,
            string name,
            string text,
            string width)
        {
            var core = await unitOfWork.ApplicationHotSpots.GetByNameAsync(name);
            if (core == null)
            {
                core = new Data.Core.Domain.Application.HotSpot
                {
                    Name = name,
                    Text = text,
                    Width = width
                };
            }
            else
            {
                core.Text = text;
                core.Width = width;
            }
            
            await unitOfWork.AddAsync(core);
        }
    }
}