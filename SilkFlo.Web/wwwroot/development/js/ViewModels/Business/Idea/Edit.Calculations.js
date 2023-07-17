// Add the model
if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Edit)
    SilkFlo.ViewModels.Business.Idea.Edit = {};


SilkFlo.ViewModels.Business.Idea.Edit.Calculations =
{
    GetTotals: function (parentElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetTotals: ';

        // Guard Clause
        if (!parentElementId)
        {
            console.log(logPrefix + 'parentElementId parameter is missing.');
            return;
        }

        const parent = document.getElementById(parentElementId);


        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${parentElementId} missing`);
            return;
        }


        const model = SilkFlo.Models.Business.Idea.GetModelFromParent(parent);
        model.IdeaApplicationVersions = SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetApplications(parent);

        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'The model is missing.');
            return;
        }


        // Total Processing Time
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetTotalProcessingTime',
            'TotalProcessingTime',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatToHoursPerYear);

        // Total Rework Time
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetTotalReworkTime',
            'TotalReworkTime',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatToHoursPerYear);

        // Total Review Time
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetTotalReviewTime',
            'TotalReviewTime',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatToHoursPerYear);



        // Total Time Needed To Perform Work Without Automation
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetTimeNeededToPerformWorkWithoutAutomation',
            'TotalTimeNeededToPerformWorkWithoutAutomation',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberTo2DecimalPlaces);


        // Full Time Equivalents Required
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetFullTimeEquivalentsRequired',
            'FullTimeEquivalentsRequired',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberTo2DecimalPlaces);


        // Cost Per Year
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetCostPerYear',
            'CostPerYear',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToCurrency);

        // Feasibility Score
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetFeasibilityScore',
            'FeasibilityScore',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToPercent);


        // Fitness Score
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetFitnessScore',
            'FitnessScore',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToPercent);


        // Idea Score
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetIdeaScore',
            'IdeaScore',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToPercent);


        // Estimated Benefit Per Employee (Hours)
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetEstimatedBenefitPerCompany_Hours',
            'EstimatedBenefitPerCompany_Hours',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberTo2DecimalPlaces);


        // Estimated Benefit Per Company (Currency)
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetEstimatedBenefitPerCompany_Currency',
            'BenefitPerCompany_Currency',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToCurrency);


        // Benefit Per Employee (FTE)
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetEstimatedBenefitPerCompany_FTE',
            'EstimatedBenefitPerEmployee_FTE',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberTo2DecimalPlaces);


        // Benefit Per Employee (Hours)
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetEstimatedBenefitPerEmployee_Hours',
            'EstimatedBenefitPerEmployee_Hours',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberTo2DecimalPlaces);


        // Benefit Per Employee (Currency)
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetEstimatedBenefitPerEmployee_Currency',
            'BenefitPerEmployee_Currency',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToCurrency);


        // Benefit Per Employee (FTE)
        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetBenefitPerEmployee_FTE',
            'BenefitPerEmployee_FTE',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberTo2DecimalPlaces);
    },

    AutomationPotential: function (parentElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Calculations.AutomationPotential: ';

        // Guard Clause
        if (!parentElementId)
        {
            console.log(logPrefix + 'parentElementId parameter is missing.');
            return;
        }

        const parent = document.getElementById(parentElementId);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${parentElementId} missing`);
            return;
        }


        const model = SilkFlo.Models.Business.Idea.GetModelFromParent(parent);
        model.IdeaApplicationVersions = SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetApplications(parent);

        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'The model is missing.');
            return;
        }


        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetAutomationPotential',
            'AutomationPotential',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToPercent);
    },

    EaseOfImplementation: function (parentElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Calculations.EaseOfImplementation: ';

        // Guard Clause
        if (!parentElementId)
        {
            console.log(logPrefix + 'parentElementId parameter is missing.');
            return;
        }

        const parent = document.getElementById(parentElementId);


        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${parentElementId} missing`);
            return;
        }


        const model = SilkFlo.Models.Business.Idea.GetModelFromParent(parent);
        model.IdeaApplicationVersions = SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetApplications(parent);


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'The model is missing.');
            return;
        }

        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetEaseOfImplementation',
            'EaseOfImplementation',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToPercent);
    },

    PrimedScore: function (parentElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Calculations.EaseOfImplementation: ';

        // Guard Clause
        if (!parentElementId)
        {
            console.log(logPrefix + 'parentElementId parameter missing.');
            return;
        }


        const parent = document.getElementById(parentElementId);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${parentElementId} missing`);
            return;
        }


        const model = SilkFlo.Models.Business.Idea.GetModelFromParent(parent);
        model.IdeaApplicationVersions = SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetApplications(parent);


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'The model is missing.');
            return;
        }


        SilkFlo.Models2.Business.Idea.GetCalculation(
            model,
            '/api/Business/Ideas/GetPrimedScore',
            'PrimedScore',
            SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToPercent);
    },

    GetApplications: function (parentElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetApplications: ';

        // Guard Clause
        if (!parentElement)
        {
            console.log(`${logPrefix}parentElement parameter missing`);
            return null;
        }

        const applicationParentElement = SilkFlo.GetElementInParent(
            parentElement,
            'Business.Applications');


        const modelApplications = SilkFlo.Models.Business.Application.GetModelCollectionFromParent(
            applicationParentElement,
            'Business.Application',
            [
                'Id', 'IsSelected', 'IsThinClient', 'LanguageId', 'VersionId'
            ]);

        if (!modelApplications)
            return null;

        const modelApplicationsSelected = [];



        const length = modelApplications.length;
        for (let i = 0; i < length; i++)
        {
            const modelApplication = modelApplications[i];
            if (modelApplication.IsSelected)
            {
                modelApplicationsSelected.push(modelApplication);
            }
        }


        return modelApplicationsSelected;
    }
};