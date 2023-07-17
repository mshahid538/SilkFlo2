if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};


SilkFlo.ViewModels.Business.Idea.Edit = {

    // SilkFlo.ViewModels.Business.Idea.Edit.GetParent
    GetParent: function () {
        const id = 'FormEdit_BusinessIdea';

        const parent = document.getElementById(id);

        if (!parent) {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.GetParent: ';
            console.log(`${logPrefix}Element with id "${id}" missing`);
            return null;
        }

        return parent;
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.Get_Callback
    Get_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Get_Callback: ';
        if (!SilkFlo.SideBar.ClearMenu)
        {
            console.log(`${logPrefix}SilkFlo.SideBar.ClearMenu function missing.`);
            return;
        }

        SilkFlo.SideBar.ClearMenu ();

        const parent = SilkFlo.ViewModels.Business.Idea.Edit.GetParent();
        if (!parent)
            return;

        const name = 'Business.Idea.Name';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        setTimeout(
            () => {
                element.focus();
            },
            500);
    },

    // SilkFlo.ViewModels.Business.Idea.Edit.ValidateElement
    ValidateElement: function (parentElement, name, min, max)
    {
        const element = parentElement.querySelector('[name="' + name + '"]');

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.ValidateElement: ';
            console.log(logPrefix + 'Element with name ' + name + ' is missing.');
            return false;
        }

        return SilkFlo.IsRequired(element, min, max, "Required.", false);
    },

    // SilkFlo.ViewModels.Business.Idea.Edit.ValidateRangeElement
    ValidateRangeElement: function (parentElement, name, min, max) {

        const element = parentElement.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.ValidateRangeElement: ';
            console.log(`${logPrefix}Element with name ${name} is missing.`);
            return false;
        }

        return SilkFlo.IsInRange(element, min, max);
    },


    /*
     * GetBusinessTeam Overview
     * ------------------------
     * This will update the Business.Idea.TeamId select element and Business.Idea.ProcessId select element
     * based on the selection in the departmentElement.
     *
     * Parameters
     * ----------
     * parentElementId - The element id for the element containing the elements representing the model.
     * departmentElement     - The department element.
     */
    GetBusinessTeam: function (parentElementId, departmentElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.GetBusinessTeam: ';


        const teamElement = SilkFlo.GetElementInParentId(parentElementId, 'Business.Idea.TeamId');

        // Guard Clause
        if (!teamElement) {
            console.log(`${logPrefix}Could not find an element with id Business.Idea.TeamId.`);
            return;
        }

        // Guard Clause
        if (!departmentElement) {
            console.log(`${logPrefix}departmentElement Element is null.`);
            return;
        }


        const id = departmentElement.value;
        if (id)
        {
            SilkFlo.DataAccess.UpdateElement(
                `/api/business/team/departmentId/${id}`,
                null,
                teamElement,
                '',
                SilkFlo.ViewModels.Business.Idea.Edit.ResetProcessComboList(parentElementId)
            );
        }
        else
        {
            const content = '<option value="">Select...</option>';

            teamElement.innerHTML = content;

            const processElement = SilkFlo.GetElementInParentId(parentElementId, 'Business.Idea.ProcessId');

            // Guard Clause
            if (!processElement)
            {
                console.log(`${logPrefix}Element with id Business.Idea.ProcessId is null.`);
                return;
            }

            processElement.innerHTML = content;
        }
    },



    /*
     * GetBusinessProcess Overview
     * ---------------------------
     * This will update the Business.Idea.ProcessId select element based on the selection in the teamElement
     * 
     * Parameters
     * ----------
     * parentElementId - The element id for the element containing the elements representing the model.
     * teamElement     - The team element.
     */
    GetBusinessProcess: function (parentElementId, teamElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.GetBusinessProcess: ';


        // Guard Clause
        if (!teamElement) {
            console.log(`${logPrefix}teamElement Element is null.`);
            return;
        }


        const processElement = SilkFlo.GetElementInParentId(parentElementId, 'Business.Idea.ProcessId');

        // Guard Clause
        if (!processElement)
        {
            console.log(`${logPrefix}Could not find an element with id Business.Idea.ProcessId.`);
            return;
        }


        // Update the process element.
        const id = teamElement.value;
        if (id)
        {
            SilkFlo.DataAccess.UpdateElement(
                `/api/business/process/team/${id}`,
                null,
                processElement);
        }
        else
        {
            processElement.innerHTML = '<option value="">Select...</option>';
        }
    },


    /*
     * GetBusinessProcess Overview
     * ---------------------------
     * 
     * Reset the select list with id Business.Idea.ProcessId.
     */
    // SilkFlo.ViewModels.Business.Idea.Edit.ResetProcessComboList
    ResetProcessComboList: function (parentElementId)
    {
        const content = '<option value="">Select...</option>';
        const processElement = SilkFlo.GetElementInParentId(parentElementId, 'Business.Idea.ProcessId');


        // Guard Clause
        if (!processElement)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.ResetProcessComboList: ';
            console.log(`${logPrefix}Element with id Business.Idea.ProcessId is null.`);
            return;
        }


        processElement.innerHTML = content;
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.Submit_OnClick
    Submit_OnClick: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.GetParent();
        if (!parent)
            return;
        
        const isDraft = parent.querySelector('[name="Business.Idea.IsDraft"]');
        isDraft.checked = false;

        SilkFlo.ViewModels.Business.Idea.Edit.Save();
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.SaveDraft_OnClick
    SaveDraft_OnClick: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.GetParent();
        if (!parent)
            return;

        const isDraft = parent.querySelector('[name="Business.Idea.IsDraft"]');

        if (!isDraft)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.SaveDraft_OnClick: ';
            console.log(`${logPrefix}Element with name "Business.Idea.IsDraft" missing`);
            return;
        }

        isDraft.checked = true;

        SilkFlo.ViewModels.Business.Idea.Edit.Save();
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.Save
    Save: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.GetParent();
        if (!parent)
            return;


        const model = SilkFlo.Models.Business.Idea.GetModelFromParent(parent);

        // Guard Clause
        if (!model)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Save: ';
            console.log(`${logPrefix}model missing`);
            return;
        }


        model.IdeaApplicationVersions = SilkFlo.ViewModels.Business.Idea.Edit.Calculations.GetApplications(parent);
        model.Collaborators = SilkFlo.ViewModels.Business.Idea.Section.Collaborators.GetModel(parent);

        this.ReturnURL = model.ReturnURL;


        SilkFlo.Models.Business.Idea.Save(
            model,
            SilkFlo.ViewModels.Business.Idea.Edit.Redirect,
            SilkFlo.ViewModels.Business.Idea.Edit.SaveFailed_CallBack,
            'content');
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.SaveFailed_CallBack
    SaveFailed_CallBack: function (feedback)
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.GetParent ();
        SilkFlo.DataAccess.Feedback(
            feedback,
            parent.id);
    },

    // SilkFlo.ViewModels.Business.Idea.Edit.Cancel_OnClick
    Cancel_OnClick: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.GetParent();

        if (!parent)
            return;

        const model = SilkFlo.Models.Business.Idea.GetModelFromParent (
                parent,
                ['ReturnURL'] );


        if (!model.ReturnURL)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Cancel_OnClick: ';
            console.log(`${logPrefix}model.ReturnURL is missing.`);
            SilkFlo.SideBar.OnClick('/Dashboard');
        }

        window.location.href = model.ReturnURL;
    },

    Redirect: function ()
    {
        if (!SilkFlo.ViewModels.Business.Idea.Edit.ReturnURL
            || SilkFlo.ViewModels.Business.Idea.Edit.ReturnURL.indexOf('/account/signin')  !== -1 )
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.Redirect: ';
            console.log(`${logPrefix}SilkFlo.ViewModels.Business.Idea.Edit.ReturnURL is missing.`);
            SilkFlo.SideBar.OnClick('/Dashboard');
        }

        SilkFlo.SideBar.OnClick(SilkFlo.ViewModels.Business.Idea.Edit.ReturnURL);
    }
};