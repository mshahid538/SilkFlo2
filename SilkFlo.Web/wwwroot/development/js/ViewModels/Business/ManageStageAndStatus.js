// Add the model
if (SilkFlo.VieModels === undefined)
    SilkFlo.VieModels  = {};


if (SilkFlo.VieModels.Business === undefined)
    SilkFlo.VieModels.Business = {};

SilkFlo.VieModels.Business.ManageStageAndStatus = {

    // SilkFlo.VieModels.Business.ManageStageAndStatus.GetParent
    GetParent: function ()
    {
        const id = 'ManageStageAndStatus';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },

    // SilkFlo.VieModels.Business.ManageStageAndStatus.SetWarning
    SetWarning: function (text)
    {
        if (!text)
            SilkFlo.VieModels.Business.ManageStageAndStatus.SetMessage('');

        SilkFlo.VieModels.Business.ManageStageAndStatus.SetMessage ( `<span class="text-warning">${text}</span>` );
    },

    // SilkFlo.VieModels.Business.ManageStageAndStatus.ShowError
    ShowError: function (text)
    {
        if (!text)
            SilkFlo.VieModels.Business.ManageStageAndStatus.SetMessage('');

        SilkFlo.VieModels.Business.ManageStageAndStatus.SetMessage(`<span class="text-danger">${text}</span>`);
    },

    // SilkFlo.VieModels.Business.ManageStageAndStatus.SetMessage
    SetMessage: function (text) {
        const parent = this.GetParent();
        if (!parent)
            return;

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = text;
    },

    Get: function (ideaId, returnFunction)
    {
        const element = document.getElementById('manageStageAndStatus');

        if (!element)
            return;


        SilkFlo.VieModels.Business.ManageStageAndStatus.ReturnFunction = returnFunction;

        const apiUrl = `/api/Business/Idea/GetManageStageAndStatus/modal/ideaId/${ideaId}`;
        SilkFlo.DataAccess.UpdateElement(
            apiUrl,
            null,
            element,
            '',
            'GET',
            SilkFlo.VieModels.Business.ManageStageAndStatus.SetDatePickers
        );
    },


    SetDatePickers: function ()
    {
        const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.SetDatePickers: ';

        const id = 'AccessDenied.Modal.Message';
        let element = document.getElementById(id);
        if (element)
        {
            // if we have an error message then return.
            return;
        }

        const parent = SilkFlo.VieModels.Business.ManageStageAndStatus.GetParent();

        // Guard Clause
        if (!parent)
            return;


        let name = 'AutomaticStartDate';
        element = parent.querySelector(`[name="${name}"]`);
        if (element)
        {
            // There are no date pickers.
            return;
        }


        name = 'Shared.StageGroup.Id';
        const elementStageGroupId = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementStageGroupId) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        const stageGroupId = elementStageGroupId.value;
        if (!stageGroupId)
        {
            console.log(`${logPrefix}stageGroupId missing`);
            return;
        }


        // Date Start Estimate
        name = 'Business.IdeaStage.DateStartEstimate';
        const dateStartEstimate = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!dateStartEstimate)
        {
            console.log(`${logPrefix}element with name "${name}" missing`);
            return;
        }


        name = 'Business.IdeaStage.BtnEstimateDate';
        const btnEstimateDate = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!btnEstimateDate)
        {
            console.log(`${logPrefix}element with name "${name}" missing`);
            return;
        }



        // Date Start
        name = 'Business.IdeaStage.DateStart';
        const dateStart = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!dateStart)
        {
            console.log(`${logPrefix}element with name "${name}" missing`);
            return;
        }


        name = 'Business.IdeaStage.BtnActualDate';
        const btnActualDate = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!btnActualDate)
        {
            console.log(`${logPrefix}element with name "${name}" missing`);
            return;
        }


        if (stageGroupId === 'n04_Deployed')
        {
            Delaney.UI.DatePicker.Main(
                dateStartEstimate,
                btnEstimateDate);

            Delaney.UI.DatePicker.Main(
                dateStart,
                btnActualDate);
        }
        else
        {
            // Date End Estimate
            name = 'Business.IdeaStage.DateEndEstimate';
            const dateEndEstimate = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!dateEndEstimate) {
                console.log(`${logPrefix}element with name "${name}" missing`);
                return;
            }

            // Date End
            name = 'Business.IdeaStage.DateEnd';
            const dateEnd = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!dateEnd) {
                console.log(`${logPrefix}element with name "${name}" missing`);
                return;
            }

            Delaney.UI.DatePicker.MainRangeSelector(
                dateStartEstimate,
                dateEndEstimate,
                btnEstimateDate);

            Delaney.UI.DatePicker.MainRangeSelector(
                dateStart,
                dateEnd,
                btnActualDate);
        }
    },



    SetDateTimeElements: function ()
    {
        const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.SetDateTimeElements: ';

        const parent = SilkFlo.VieModels.Business.ManageStageAndStatus.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const model = SilkFlo.VieModels.Business.ManageStageAndStatus.GetModel(parent);

        if (!model)
        {
            console.log ( `${logPrefix}model missing` );
            return;
        }

        if (!model.CurrentIdeaStage)
        {
            console.log(`${logPrefix}model.CurrentIdeaStage missing`);
            return;
        }

        if( model.CurrentIdeaStage.IsAutomaticDate)
            return;


        // Guard Clause
        if (!parent.ModelElements)
        {
            console.log(`${logPrefix}parent.ModelElements missing`);
            return;
        }


        if (model.CurrentIdeaStage.IsDeployedGroup)
        {
            SilkFlo.Models.Shared.IdeaStatus.GetModelFromParent(
                parent,
                [
                    'LblDateEstimate',
                    'DateStartEstimate',
                    'BtnEstimateDate',
                    'LblDate',
                    'DateStart',
                    'BtnActualDate'
                ],
                'Business.IdeaStage.');
        }
        else
        {
            SilkFlo.Models.Shared.IdeaStatus.GetModelFromParent(
                parent,
                [
                    'LblDateEstimate',
                    'DateStartEstimate',
                    'DateEndEstimate',
                    'BtnEstimateDate',
                    'LblDate',
                    'DateStart',
                    'DateEnd',
                    'BtnActualDate'
                ],
                'Business.IdeaStage.');

            // Guard Clause
            if (!parent.ModelElements.DateEndEstimate) {
                console.log(`${logPrefix}parent.ModelElements.DateEndEstimate missing`);
                return;
            }

            // Guard Clause
            if (!parent.ModelElements.DateEnd) {
                console.log(`${logPrefix}parent.ModelElements.DateEnd missing`);
                return;
            }
        }





        // Guard Clause
        if (!parent.ModelElements.LblDateEstimate)
        {
            console.log(`${logPrefix}parent.ModelElements.LblDateEstimate missing`);
            return;
        }

        // Guard Clause
        if (!parent.ModelElements.DateStartEstimate)
        {
            console.log(`${logPrefix}parent.ModelElements.DateStartEstimate missing`);
            return;
        }

        // Guard Clause
        if (!parent.ModelElements.BtnEstimateDate)
        {
            console.log(`${logPrefix}parent.ModelElements.BtnEstimateDate missing`);
            return;
        }












        // Guard Clause
        if (!parent.ModelElements.LblDate)
        {
            console.log(`${logPrefix}parent.ModelElements.LblDate missing`);
            return;
        }

        // Guard Clause
        if (!parent.ModelElements.DateStart)
        {
            console.log(`${logPrefix}parent.ModelElements.DateStart missing`);
            return;
        }

        // Guard Clause
        if (!parent.ModelElements.BtnActualDate)
        {
            console.log(`${logPrefix}parent.ModelElements.BtnActualDate missing`);
            return;
        }










        if (model.CurrentIdeaStage.IsAutomaticDate || model.NextIdeaStatus.StatusId === null)
        {
            parent.ModelElements.LblDateEstimate.classList.remove('has-validation');
            parent.ModelElements.LblDateEstimate.classList.remove('mandatory');
            parent.ModelElements.DateStartEstimate.setAttribute('disabled', 'disabled');
            if (parent.ModelElements.DateStartEstimate.LastValue) {
                parent.ModelElements.DateStartEstimate.value = parent.ModelElements.DateStartEstimate.LastValue;
            }
            parent.ModelElements.BtnEstimateDate.setAttribute('disabled', 'disabled');



            parent.ModelElements.DateEndEstimate.setAttribute('disabled', 'disabled');
            if (parent.ModelElements.DateEndEstimate.LastValue !== undefined) {
                parent.ModelElements.DateEndEstimate.value = parent.ModelElements.DateEndEstimate.LastValue;
            }



            parent.ModelElements.BtnActualDate.setAttribute('disabled', 'disabled');
            parent.ModelElements.LblDate.classList.remove('has-validation');
            parent.ModelElements.LblDate.classList.remove('mandatory');
            parent.ModelElements.DateStart.setAttribute('disabled', 'disabled');
            if (parent.ModelElements.DateStart.LastValue !== undefined) {
                parent.ModelElements.DateStart.value = parent.ModelElements.DateStart.LastValue;
            }
            parent.ModelElements.BtnActualDate.setAttribute('disabled', 'disabled');



            parent.ModelElements.DateEnd.setAttribute('disabled', 'disabled');
            if (parent.ModelElements.DateEnd.LastValue !== undefined) {
                parent.ModelElements.DateEnd.value = parent.ModelElements.DateEnd.LastValue;
            }
        }
        else
        {
            parent.ModelElements.LblDateEstimate.classList.add('has-validation');
            parent.ModelElements.LblDateEstimate.classList.add('mandatory');
            parent.ModelElements.DateStartEstimate.removeAttribute('disabled');
            parent.ModelElements.DateStartEstimate.LastValue = parent.ModelElements.DateStartEstimate.value;
            parent.ModelElements.BtnEstimateDate.removeAttribute('disabled');


            if (!model.CurrentIdeaStage.IsDeployedGroup)
            {
                parent.ModelElements.DateEndEstimate.removeAttribute ( 'disabled' );
                parent.ModelElements.DateEndEstimate.LastValue = parent.ModelElements.DateEndEstimate.value;
            }


            parent.ModelElements.LblDate.classList.add('has-validation');
            parent.ModelElements.LblDate.classList.add('mandatory');
            parent.ModelElements.DateStart.removeAttribute('disabled');
            parent.ModelElements.DateStart.LastValue = parent.ModelElements.DateStart.value;
            parent.ModelElements.BtnActualDate.removeAttribute('disabled');

            if (!model.CurrentIdeaStage.IsDeployedGroup)
            {
                parent.ModelElements.DateEnd.removeAttribute ( 'disabled' );
                parent.ModelElements.DateEnd.LastValue = parent.ModelElements.DateEnd.value;
            }
        }
    },

    // SilkFlo.VieModels.Business.ManageStageAndStatus.UncheckOption
    UncheckOption: function (element)
    {

        const parent = SilkFlo.VieModels.Business.ManageStageAndStatus.GetParent();

        // Guard Clause
        if (!parent)
            return;


        const name = 'Shared.IdeaStatuses';
        const ideaStatuses = parent.querySelector(`[name="${name}"]`);


        // Guard Clause
        if (!ideaStatuses)
        {
            const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.UncheckOption: ';
            console.log(`${logPrefix}element with name "${name}" missing`);
            return;
        }

        const nameIsSelected = 'Shared.IdeaStatus.IsSelected';
        const selection = parent.querySelectorAll("[name=\"" + nameIsSelected + '"]');



        const length = selection.length;
        for (let i = 0; i < length; i++)
        {
            const item = selection[i];
            if (item !== element)
            {
                item.checked = false;
            }
        }
    },




    // SilkFlo.VieModels.Business.ManageStageAndStatus.GetModel
    GetModel: function (parent) {
        const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.GetModel: ';

        // The model that will be returned.
        const model = {
            NextIdeaStatus: {
                StatusId: null,
                StageId: null
            },
            CurrentIdeaStage: {
                IsAutomaticDate: false,
                IsDeployedGroup: false,
                StageId: null,
                DateStartEstimate: null,
                DateEndEstimate: null,
                DateStart: null,
                DateEnd: null
            },
            MinDate: null,
            IdeaId: null,
            IsSelected: false
        };

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}parent parameter missing`);
            return null;
        }

        // Get the selected next status
        let name = 'Shared.IdeaStatuses';
        let element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return null;
        }

        const ideaStatuses = SilkFlo.Models.Shared.IdeaStatus.GetModelCollectionFromParent(
            parent,
            'Shared.IdeaStatus',
            ['StatusId', 'StageId', 'IsSelected'] );

        const length = ideaStatuses.length;
        for (let i = 0; i < length; i++)
        {
            const ideaStatus = ideaStatuses[i];
            if (ideaStatus.IsSelected)
            {
                model.NextIdeaStatus.StatusId = ideaStatus.StatusId;
                model.NextIdeaStatus.StageId = ideaStatus.StageId;
                model.IsSelected = true;
                break;
            }
        }



        name = 'Business.Idea.Id';
        element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return null;
        }

        model.IdeaId = element.value;


        // Get the current Business.IdeaStage

        // AutomaticStartDate
        name = 'AutomaticStartDate';
        const elementAutomaticStartDate = parent.querySelector(`[name="${name}"]`);

        if (elementAutomaticStartDate)
        {
            model.CurrentIdeaStage.IsAutomaticDate = true;

            const ideaStage = SilkFlo.Models.Shared.IdeaStatus.GetModelFromParent(
                parent,
                [
                    'StageId'
                ],
                'Business.IdeaStage.');

            model.CurrentIdeaStage.StageId = ideaStage.StageId;

            parent.Model = model;
            return model;
        }


        model.CurrentIdeaStage.IsDeployedGroup = SilkFlo.VieModels.Business.ManageStageAndStatus.IsDeployedGroup(parent);

        if (model.CurrentIdeaStage.IsDeployedGroup)
        {
            const ideaStage = SilkFlo.Models.Shared.IdeaStatus.GetModelFromParent(
                parent,
                [
                    'StageId', 'DateStartEstimate', 'DateStart'
                ],
                'Business.IdeaStage.');

            model.CurrentIdeaStage.StageId = ideaStage.StageId;
            model.CurrentIdeaStage.DateStartEstimate = ideaStage.DateStartEstimate;
            model.CurrentIdeaStage.DateStart = ideaStage.DateStart;


            const minDate = parent.ModelElements.DateStartEstimate.getAttribute('min');

            if (!minDate)
            {
                console.log(`${logPrefix}min attribute is missing from element with name "Business.IdeaStage.DateStartEstimate"`);
                return null;
            }


            const isoDate = SilkFlo.VieModels.Business.ManageStageAndStatus.GetISOString(minDate);
            model.MinDate = new Date(isoDate);
        }
        else
        {
            const ideaStage = SilkFlo.Models.Shared.IdeaStatus.GetModelFromParent (
                parent,

                [
                    'StageId', 'DateStartEstimate', 'DateEndEstimate', 'DateStart', 'DateEnd'
                ],
                'Business.IdeaStage.');

            model.CurrentIdeaStage.StageId = ideaStage.StageId;
            model.CurrentIdeaStage.DateStartEstimate = ideaStage.DateStartEstimate;
            model.CurrentIdeaStage.DateEndEstimate = ideaStage.DateEndEstimate;
            model.CurrentIdeaStage.DateStart = ideaStage.DateStart;
            model.CurrentIdeaStage.DateEnd = ideaStage.DateEnd;
        }

        return model;
    },


    // SilkFlo.VieModels.Business.ManageStageAndStatus.IsDeployedGroup
    IsDeployedGroup: function (parent)
    {
        const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.IsDeployedGroup: ';

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}element parameter missing`);
            return false;
        }


        const name = 'Shared.StageGroup.Id';
        const elementStageGroupId = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementStageGroupId) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return false;
        }


        const stageGroupId = elementStageGroupId.value;

        // Guard Clause
        if (!stageGroupId) {
            console.log(`${logPrefix}stageGroupId missing`);
            return false;
        }

        
        return stageGroupId === 'n04_Deployed';
    },


    // SilkFlo.VieModels.Business.ManageStageAndStatus.Validate
    Validate: function (parent)
    {
        const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.Validate: ';

        // Guard Clause
        if (!parent)
        {
            console.log(logPrefix + 'parent parameter missing');
            return false;
        }


        const model = SilkFlo.VieModels.Business.ManageStageAndStatus.GetModel(parent);

        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'model is missing');
            return false;
        }

        if (!model.IsSelected)
        {
            SilkFlo.VieModels.Business.ManageStageAndStatus.SetWarning ('Please select a status.');
            return false;
        }


        // Do we have content?
        if (!model.CurrentIdeaStage.IsAutomaticDate)
        {
            if (model.CurrentIdeaStage.IsDeployedGroup)
            {
                if (!model.CurrentIdeaStage.DateStartEstimate
                 && !model.CurrentIdeaStage.DateStart)
                {
                    SilkFlo.VieModels.Business.ManageStageAndStatus.SetWarning ( 'Please provide dates.' );
                    return false;
                }
            }
            else
            {
                if (!model.CurrentIdeaStage.DateStartEstimate
                    && !model.CurrentIdeaStage.DateEndEstimate
                    && !model.CurrentIdeaStage.DateStart
                    && !model.CurrentIdeaStage.DateEnd)
                {
                    SilkFlo.VieModels.Business.ManageStageAndStatus.SetWarning ( 'Please provide dates.' );
                    return false;
                }
            }
        }


        if (!model.NextIdeaStatus.StatusId)
        {
            SilkFlo.VieModels.Business.ManageStageAndStatus.ShowError('Please select a status.');
            return false;
        }


        let isValid = true;
        if (!model.CurrentIdeaStage.IsAutomaticDate)
        {
            if (model.CurrentIdeaStage.IsDeployedGroup)
            {
                if (!model.CurrentIdeaStage.DateStartEstimate
                    || !model.CurrentIdeaStage.DateStart)
                {
                    SilkFlo.VieModels.Business.ManageStageAndStatus.SetWarning('Please complete current stage information.');
                    return false;
                }

                SilkFlo.Models.Shared.IdeaStatus.GetModelFromParent (
                    parent,
                    [
                        'DateStartEstimate_InvalidFeedback',
                        'DateStart_InvalidFeedback'
                    ],
                    'Business.IdeaStage.' );
            }
            else
            {
                if (!model.CurrentIdeaStage.DateStartEstimate
                    || !model.CurrentIdeaStage.DateEndEstimate
                    || !model.CurrentIdeaStage.DateStart
                    || !model.CurrentIdeaStage.DateEnd)
                {
                    SilkFlo.VieModels.Business.ManageStageAndStatus.SetWarning('Please complete current stage information.');
                    return false;
                }


                SilkFlo.Models.Shared.IdeaStatus.GetModelFromParent (
                    parent,
                    [
                        'DateStartEstimate_InvalidFeedback',
                        'DateEndEstimate_InvalidFeedback',
                        'DateStart_InvalidFeedback',
                        'DateEnd_InvalidFeedback'
                    ],
                    'Business.IdeaStage.' );
            }



            // DateStartEstimate < MinDate
            if (model.CurrentIdeaStage.DateStartEstimate < model.MinDate)
            {
                parent.ModelElements.DateStartEstimate_InvalidFeedback.style.display = 'block';
                parent.ModelElements.DateStartEstimate_InvalidFeedback.innerHTML = `The estimated date must be greater than ${this.GetISOString(model.MinDate)}.`;
                isValid = false;
            }
            else
            {
                parent.ModelElements.DateStartEstimate_InvalidFeedback.style.display = '';
            }


            if (!model.CurrentIdeaStage.IsDeployedGroup)
            {
                // DateEndEstimate
                if (model.CurrentIdeaStage.DateEndEstimate <= 0)
                {
                    parent.ModelElements.DateEndEstimate_InvalidFeedback.style.display = 'block';
                    isValid = false;
                }
                else
                {
                    parent.ModelElements.DateEndEstimate_InvalidFeedback.style.display = '';
                }
            }


            // DateStart < MinDate
            if (model.CurrentIdeaStage.DateStart < model.MinDate)
            {
                parent.ModelElements.DateStart_InvalidFeedback.style.display = 'block';
                parent.ModelElements.DateStart_InvalidFeedback.innerHTML = `The actual date must be greater than ${this.GetISOString(this.Model.MinDate)}.`;
                isValid = false;
            }
            else
            {
                parent.ModelElements.DateStart_InvalidFeedback.style.display = '';
            }



            if (model.CurrentIdeaStage.DateStartEstimate !== ''
                && model.CurrentIdeaStage.DateStartEstimate < model.MinDate)
            {
                SilkFlo.VieModels.Business.ManageStageAndStatus.ShowError('Please complete the stage fields.');
                isValid = false;
            }


            if (!model.CurrentIdeaStage.IsDeployedGroup)
            {
                // DateEnd < MinDate
                if (!model.CurrentIdeaStage.IsDeployedGroup)
                {
                    if (model.CurrentIdeaStage.DateEnd < model.MinDate)
                    {
                        parent.ModelElements.DateEnd_InvalidFeedback.style.display = 'block';
                        parent.ModelElements.DateEnd_InvalidFeedback.innerHTML = `The actual date must be greater than ${this.GetISOString ( this.Model.MinDate )}.`;
                        isValid = false;
                    }
                    else
                    {
                        parent.ModelElements.DateEnd_InvalidFeedback.style.display = '';
                    }
                }
            }
        }

        parent.Model = model;

        if (!isValid)
            return false;

        return true;
    },

    // SilkFlo.VieModels.Business.ManageStageAndStatus.Save
    Save: function ()
    {
        const parent = SilkFlo.VieModels.Business.ManageStageAndStatus.GetParent();

        // Guard Clause
        if (!parent)
            return;



        if (!SilkFlo.VieModels.Business.ManageStageAndStatus.Validate(parent))
            return;

        SilkFlo.VieModels.Business.ManageStageAndStatus.Parent = parent;

        SilkFlo.VieModels.Business.ManageStageAndStatus.IdeaId = parent.Model.IdeaId;


        // Save
        const url = '/api/Business/Idea/SaveStatus';

        SilkFlo.Models.Shared.IdeaStatus.Save(
            parent.Model,
            SilkFlo.VieModels.Business.ManageStageAndStatus.Save_Callback,
            SilkFlo.DataAccess.Feedback,
            parent.id,
            url);
    },

    // SilkFlo.VieModels.Business.ManageStageAndStatus.Save_Callback
    Save_Callback: function ()
    {
        const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.Save_Callback: ';

        const parent = SilkFlo.VieModels.Business.ManageStageAndStatus.Parent;

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}SilkFlo.VieModels.Business.ManageStageAndStatus.Parent missing`);
            return;
        }


        const ideaId = SilkFlo.VieModels.Business.ManageStageAndStatus.IdeaId;

        // Guard Clause
        if (!ideaId)
        {
            console.log(`${logPrefix}SilkFlo.VieModels.Business.ManageStageAndStatus.IdeaId missing`);
            return;
        }




        const model = parent.Model;

        // Guard Clause
        if (!model)
        {
            console.log(`${logPrefix}parent.Model missing`);
            return;
        }



        let id = `StageCell_${ideaId}`;
        let element = document.getElementById(id);

        if (element)
            SilkFlo.DataAccess.UpdateTargetElement ( element );

        id = `StatusCell_${ideaId}`;
        element = document.getElementById(id);


        if (element)
            SilkFlo.DataAccess.UpdateTargetElement(element);


        id = 'totalIdeas';
        element = document.getElementById(id);
        if (element)
        {
            SilkFlo.DataAccess.UpdateTargetElement (element);
        }

        id = 'totalInBuild';
        element = document.getElementById(id);
        if (element)
        {
            SilkFlo.DataAccess.UpdateTargetElement (element);
        }

        id = 'Dashboard/GetTotalIdeas';
        element = document.getElementById(id);
        if (element)
        {
            SilkFlo.DataAccess.UpdateTargetElement (element);
        }

        id = 'totalDeployed';
        element = document.getElementById(id);
        if (element)
        {
            SilkFlo.DataAccess.UpdateTargetElement (element);
        }

        id = 'Chart.AutomationProgramPerformance';
        element = document.getElementById(id);
        if (element)
        {
            SilkFlo.DataAccess.UpdateTargetElement (element);
        }

        id = 'Chart.AutomationBuildPipeline';
        element = document.getElementById(id);
        if (element)
        {
            SilkFlo.DataAccess.UpdateTargetElement (element);
        }

        id = 'Chart.GlobalOverview';
        element = document.getElementById(id);
        if (element) {
            SilkFlo.DataAccess.UpdateTargetElement(element);
        }

        // Close the modal
        window.$('#manageStageAndStatusContainer').modal('hide');
    },




    // SilkFlo.VieModels.Business.ManageStageAndStatus.GetISOString
    GetISOString: function (dateString)
    {
        // Guard Clause
        if (!dateString)
        {
            const logPrefix = 'SilkFlo.VieModels.Business.ManageStageAndStatus.GetISOString: ';
            console.log(logPrefix + 'dateString parameter missing');
            return null;
        }


        const date = new Date ( dateString );

        let iso = date.toISOString();
        iso = iso.substring(0, 10);

        return iso;
    }
};