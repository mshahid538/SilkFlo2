if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Section)
    SilkFlo.ViewModels.Business.Idea.Section = {};


SilkFlo.ViewModels.Business.Idea.Section.About =
{
    ParentId: '',
    Parent: null,

    GetParent: function ()
    {
        const id = 'Business.Idea.Section.About.Content';

        const parent = document.getElementById(id);

        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.About.GetParent: ';
            console.log(`${logPrefix}Element with id "${id}" missing`);
            return null;
        }

        return parent;
    },


    // SilkFlo.ViewModels.Business.Idea.Section.About.Edit
    Edit: function (element, id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.About.Edit: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Guard Clause
        if (!id) {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }


        SilkFlo.DisableButton(element);

        const parent = SilkFlo.ViewModels.Business.Idea.Section.About.GetParent();

        if (!parent)
            return;


        const url = `/api/Business/Idea/Section/About/Edit/${id}`;
        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            parent,
            '',
            'GET',
            SilkFlo.ViewModels.Business.Idea.Section.About.Edit_Callback);
    },


    // SilkFlo.ViewModels.Business.Idea.Section.About.Edit_Callback
    Edit_Callback: function ()
    {
        SilkFlo.VieModels.Business.ManageStageAndStatus.SetDatePickers ();
    },

    // SilkFlo.ViewModels.Business.Idea.Section.About.GetModel
    GetModel: function (
        parentElement,
        allFields,
        includeProcessOwner)
    {
        // Guard Clause
        if (!parentElement)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.About.GetModel: ';
            console.log(`${logPrefix}parentElement parameter missing`);
            return null;
        }


        let fields;

        if (allFields)
        {
            fields = [
                'Id',
                'Name',
                'SubTitle',
                'Summary',
                'PainPointComment',
                'NegativeImpactComment',
                'DepartmentId',
                'TeamId',
                'ProcessId',
                'RuleId',
                'RuleComment',
                'InputId',
                'InputComment',
                'InputDataStructureId',
                'StructureComment',
                'ProcessStabilityId',
                'ProcessStabilityComment',
                'DocumentationPresentId',
                'DocumentationPresentComment',
                'BenefitExpected',
                'BenefitActual',
                'ChallengeExpected',
                'ChallengeActual',
                'LessenLearnt',
                'ApplicationStabilityId',
                'ApplicationStabilityComment',
                'AutomationGoalId',
                'AverageWorkingDay',
                'AverageWorkingDayComment',
                'WorkingHour',
                'WorkingHourComment',
                'AverageEmployeeFullCost',
                'AverageEmployeeFullCostComment',
                'EmployeeCount',
                'EmployeeCountComment',
                'TaskFrequencyId',
                'TaskFrequencyComment',
                'ActivityVolumeAverage',
                'ActivityVolumeAverageComment',
                'AverageProcessingTime',
                'AverageProcessingTimeComment',
                'AverageErrorRate',
                'AverageErrorRateComment',
                'AverageReworkTime',
                'AverageReworkTimeComment',
                'AverageWorkToBeReviewed',
                'AverageWorkToBeReviewedComment',
                'AverageReviewTime',
                'AverageReviewTimeComment',
                'ProcessPeakId',
                'ProcessPeakComment',
                'AverageNumberOfStepId',
                'AverageNumberOfStepComment',
                'DataInputPercentOfStructuredId',
                'DataInputPercentOfStructuredComment',
                'NumberOfWaysToCompleteProcessId',
                'NumberOfWaysToCompleteProcessComment',
                'DecisionCountId',
                'DecisionCountComment',
                'DecisionDifficultyId',
                'DecisionDifficultyComment',
                'PotentialFineAmount',
                'PotentialFineProbability',
                'IsDataInputScanned',
                'DataInputScannedComment',
                'IsDataSensitive',
                'IsHighRisk',
                'IsAlternative',
                'IsHostUpgrade'
            ];
        }
        else
        {
            fields = [
                'Id',
                'Name',
                'SubTitle',
                'Summary',
                'PainPointComment',
                'NegativeImpactComment',
                'DepartmentId',
                'TeamId',
                'RuleComment',
                'ProcessId',
                'RuleId',
                'RuleComment',
                'InputId',
                'InputComment',
                'InputDataStructureId',
                'StructureComment',
                'ProcessStabilityId',
                'ProcessStabilityComment',
                'DocumentationPresentId',
                'DocumentationPresentComment',
                'BenefitExpected',
                'BenefitActual',
                'ChallengeExpected',
                'ChallengeActual',
                'LessenLearnt'
            ];
        }

        if (includeProcessOwner)
        {
            fields.push('ProcessOwnerId');
            fields.push('ProcessOwnerFullname');

        }

        const model = SilkFlo.Models.Business.Idea.GetModelFromParent
            (
                parentElement,
                fields);


        // Get the selected applications
        if (allFields)
            model.IdeaApplicationVersions = SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetApplications(parentElement);


        // Get the Manage Stage and Status
        const name = 'ManageStageAndStatus';
        const element = parentElement.querySelector(`[name="${name}"]`);
        if (element)
        {
            if (!SilkFlo.VieModels.Business.ManageStageAndStatus.Validate ( parentElement ))
                return model;

            model.ManageStageAndStatus = parentElement.Model;
        }


        SilkFlo.ViewModels.Business.Idea.Section.About.Model = model;
        return model;
    },





    // Save the About Section
    // SilkFlo.ViewModels.Business.Idea.Section.About.Save
    Save: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Section.About.GetParent ();

        if (!parent)
            return;

        let allField = false;
        let name = 'EditAllIdeaFields';
        let element = parent.querySelector(`[name="${name}"]`);
        if (element)
            allField = true;

        let includeProcessOwner = false;
        name = 'AssignProcessOwner';
        element = parent.querySelector(`[name="${name}"]`);
        if (element)
            includeProcessOwner = true;



        let api = '/api/Business/Idea/Section/Overview/Put';
        if (allField)
        {
            api = '/api/Business/Idea/Section/DetailedAssessment/Put';
        }

        const model = SilkFlo.ViewModels.Business.Idea.Section.About.GetModel(parent, allField, includeProcessOwner);

        // Guard Clause
        if (!model)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.About.Save: ';
            console.log (`${logPrefix}model missing`);
            return;
        }


        SilkFlo.ViewModels.Business.Idea.Section.About.Model = model;

        SilkFlo.Models.Business.Idea.Save
        (
            model,
            SilkFlo.ViewModels.Business.Idea.Section.About.Save_Callback,
            SilkFlo.DataAccess.Feedback,
            parent.id,
            api,
            'PUT');
    },

    Save_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.About.Save_Callback: ';

        const model = SilkFlo.ViewModels.Business.Idea.Section.About.Model;

        // Guard Clause
        if (!model)
        {
            console.log
                (`${logPrefix}model missing`);
            return;
        }


        const parent = SilkFlo.Business.Idea.Summary.GetParent();
        if (!parent)
            return;


        let name = 'Business.Idea.Name';
        let element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        element.innerHTML = model.Name;


        if (model.ProcessOwnerFullname)
        {
            name = 'Business.Idea.ProcessOwner.Fullname';
            element = parent.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!element)
            {
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }

            element.innerHTML = model.ProcessOwnerFullname;
        }


        name = 'ManageStageAndStatus';
        const elementManageStageAndStatus = parent.querySelector(`[name="${name}"]`);

        if (elementManageStageAndStatus)
        {
            name = 'Business.Idea.Detail.Meta';
            element = parent.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!element)
            {
                console.log ( `${logPrefix}Element with name ${name} missing` );
                return;
            }

            name = 'Business.Idea.IdeaId';
            const elementIdeaId = parent.querySelector ( `[name="${name}"]` );

            // Guard Clause
            if (!elementIdeaId) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            const ideaId = elementIdeaId.value;
            if (!ideaId) {
                console.log(`${logPrefix}ideaId value missing`);
                return;
            }


            SilkFlo.DataAccess.UpdateElement (
                `/api/business/idea/Detail/GetMeta/IdeaId/${ideaId}`,
                null,
                element );
        }


        SilkFlo.ViewModels.Business.Idea.Section.About.Cancel
            (model.Id);
    },


    // Cancel the About Section
    // SilkFlo.ViewModels.Business.Idea.Section.About.Cancel
    Cancel: function (id)
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Section.About.GetParent();

        if (!parent)
            return;

        const url = `/api/Business/Idea/Section/About/Detail/${id}`;
        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            parent,
            '',
            'GET',
            SilkFlo.ViewModels.Business.Idea.Section.About.Cancel_CallBack);
    },

    Cancel_CallBack: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.About.Cancel_CallBack: ';

        SilkFlo.ViewModels.Business.Idea.Section.ScrollTop ();

        const id = 'Business.Idea.Section.About.Content';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        SilkFlo.SVGTools.AnimatePaths ( element );
    }
};