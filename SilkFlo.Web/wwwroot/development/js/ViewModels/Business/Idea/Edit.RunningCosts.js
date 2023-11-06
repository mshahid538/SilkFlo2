if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Edit)
    SilkFlo.ViewModels.Business.Idea.Edit = {};

SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts = {

    GetCurrency: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.GetCurrency: ';


        const id = 'Business.Idea.Currency';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        if (!element.value)
        {
            console.log(`${logPrefix}currency symbol missing`);
            return '';
        }

        return element.value;
    },


    GetParent: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.GetParent: ';

        const id = 'Business.Idea.Edit.CostBenefit';

        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },



    UpdateTotals: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.UpdateTotals: ';

        const parent = SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.GetParent();
        if (!parent)
            return;


        // Get the year elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'RunningCosts',
                'HumanCosts',
                'ImplementationPeopleCosts',
                'RpaSoftwareCosts',
                'OtherSoftwareCosts',
                'SupportTeam',
                'Infrastructure',
                'OtherCosts',
                'Total'],
            'GrandTotal.Year.');

        const yearGrandTotalElements = parent.ModelElements;


        // Get the month elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'RunningCosts',
                'HumanCosts',
                'ImplementationPeopleCosts',
                'RpaSoftwareCosts',
                'OtherSoftwareCosts',
                'SupportTeam',
                'Infrastructure',
                'OtherCosts',
                'Total'],
            'GrandTotal.Month.');

        const monthGrandTotalElements = parent.ModelElements;



        const colElementYrTotals =
        {
            IdeaRunningCost: null,
            IdeaOtherRunningCost: null
        };

        const colElementMthTotals =
        {
            IdeaRunningCost: null,
            IdeaOtherRunningCost: null
        };


        const colYrTotal =
        {
            OtherSoftware: 0,
            Infrastructure: 0,
            OtherCosts: 0,
            SupportTeam: 0,
        };

        const colMthTotal =
        {
            OtherSoftware: 0,
            Infrastructure: 0,
            OtherCosts: 0,
            SupportTeam: 0,
        };



        let grandTotalCostPerYear = 0;
        let grandTotalCostPerMonth = 0;


        const id = 'Business.Idea.B5';
        const humanRunningCostYearAfterElement = document.getElementById(id);

        // Guard Clause
        if (!humanRunningCostYearAfterElement)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }
        let runningCostsGrandTotalYear = 0;
        let runningCostsGrandTotalMonth = 0;


        const humanRunningCostYearAfter = humanRunningCostYearAfterElement.getAttribute('value')*1;
        const humanRunningCostMonthAfter = humanRunningCostYearAfter / 12;

        grandTotalCostPerYear += humanRunningCostYearAfter;
        grandTotalCostPerMonth += humanRunningCostMonthAfter;

        let rpaSoftwareCostsGrandTotalCostPerYear = 0;
        let name = 'Business.IdeaRunningCost.TotalCostPerYear';
        colElementYrTotals.IdeaRunningCost = parent.querySelectorAll(`[name="${name}"]`);
        let length = colElementYrTotals.IdeaRunningCost.length;
        for (let i = 0; i < length; i++)
        {
            const element = colElementYrTotals.IdeaRunningCost[i];
            const value = element.getAttribute('value');
            rpaSoftwareCostsGrandTotalCostPerYear += value * 1;
            grandTotalCostPerYear += value * 1;
            runningCostsGrandTotalYear += value * 1;
        }



        name = 'Business.IdeaOtherRunningCost.TotalCostPerYear';
        colElementYrTotals.IdeaOtherRunningCost = parent.querySelectorAll(`[name="${name}"]`);


        length = colElementYrTotals.IdeaOtherRunningCost.length;
        for (let i = 0; i < length; i++)
        {
            const element = colElementYrTotals.IdeaOtherRunningCost[i];
            const value = element.getAttribute('value');
            const costTypeId = element.getAttribute('costTypeId');
            grandTotalCostPerYear += value * 1;
            runningCostsGrandTotalYear += value * 1;

            if (costTypeId === 'Infrastructure')
                colYrTotal.Infrastructure += value * 1;
            else if (costTypeId === 'Other')
                colYrTotal.OtherCosts += value * 1;
            else if (costTypeId === 'SoftwareLicence')
                colYrTotal.OtherSoftware += value * 1;
            else if (costTypeId === 'Support')
                colYrTotal.SupportTeam += value * 1;
        }




        let rpaSoftwareCostsGrandTotalCostPerMonth = 0;
        name = 'Business.IdeaRunningCost.TotalCostPerMonth';
        colElementMthTotals.IdeaRunningCost = parent.querySelectorAll(`[name="${name}"]`);
        length = colElementMthTotals.IdeaRunningCost.length;
        for (let i = 0; i < length; i++)
        {
            const element = colElementMthTotals.IdeaRunningCost[i];
            const value = element.getAttribute('value');
            rpaSoftwareCostsGrandTotalCostPerMonth += value * 1;
            grandTotalCostPerMonth += value * 1;
            runningCostsGrandTotalMonth += value * 1;
        }



        name = 'Business.IdeaOtherRunningCost.TotalCostPerMonth';
        colElementMthTotals.IdeaOtherRunningCost = parent.querySelectorAll(`[name="${name}"]`);

        length = colElementMthTotals.IdeaOtherRunningCost.length;
        for (let i = 0; i < length; i++)
        {
            const element = colElementMthTotals.IdeaOtherRunningCost[i];
            const value = element.getAttribute('value');
            const costTypeId = element.getAttribute('costTypeId');


            grandTotalCostPerMonth += value * 1;
            runningCostsGrandTotalMonth += value * 1;

            if (costTypeId === 'Infrastructure')
                colMthTotal.Infrastructure += value * 1;
            else if (costTypeId === 'Other')
                colMthTotal.OtherCosts += value * 1;
            else if (costTypeId === 'SoftwareLicence')
                colMthTotal.OtherSoftware += value * 1;
            else if (costTypeId === 'Support')
                colMthTotal.SupportTeam += value * 1;
        }


        name = 'OneTimeCostn03_AnalysisTotalvalue';
        const elementAnalysisTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementAnalysisTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }
        name = 'OneTimeCostn04_SolutionDesignTotalvalue';
        const elementSolutionDesignTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementSolutionDesignTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }
        name = 'OneTimeCostn05_DevelopmentTotalvalue';
        const elementDevelopmentTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementDevelopmentTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }
        name = 'OneTimeCostn06_TestingTotalvalue';
        const elementTestingTotal = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementTestingTotal)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        const totalImplementation = elementAnalysisTotal.value * 1 + elementSolutionDesignTotal.value * 1 + elementDevelopmentTotal.value * 1 + elementTestingTotal.value * 1;
        const totalImplementationMonth = totalImplementation / 12;
        grandTotalCostPerYear += totalImplementation;
        grandTotalCostPerMonth += totalImplementationMonth;


        const currency = SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.GetCurrency();


        yearGrandTotalElements.HumanCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(humanRunningCostYearAfter);
        monthGrandTotalElements.HumanCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(humanRunningCostMonthAfter);

        yearGrandTotalElements.ImplementationPeopleCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(totalImplementation);
        monthGrandTotalElements.ImplementationPeopleCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(totalImplementationMonth);


        yearGrandTotalElements.RunningCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(runningCostsGrandTotalYear);
        monthGrandTotalElements.RunningCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(runningCostsGrandTotalMonth);

        yearGrandTotalElements.RpaSoftwareCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(rpaSoftwareCostsGrandTotalCostPerYear);
        monthGrandTotalElements.RpaSoftwareCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(rpaSoftwareCostsGrandTotalCostPerMonth);


        yearGrandTotalElements.Infrastructure.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colYrTotal.Infrastructure);
        monthGrandTotalElements.Infrastructure.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colMthTotal.Infrastructure);
        yearGrandTotalElements.OtherCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colYrTotal.OtherCosts);
        monthGrandTotalElements.OtherCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colMthTotal.OtherCosts);
        yearGrandTotalElements.OtherSoftwareCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colYrTotal.OtherSoftware);
        monthGrandTotalElements.OtherSoftwareCosts.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colMthTotal.OtherSoftware);
        yearGrandTotalElements.SupportTeam.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colYrTotal.SupportTeam);
        monthGrandTotalElements.SupportTeam.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(colMthTotal.SupportTeam);

        yearGrandTotalElements.Total.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(grandTotalCostPerYear);
        monthGrandTotalElements.Total.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(grandTotalCostPerMonth);

        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateWorkloadSplitEstimations ();
    }
}