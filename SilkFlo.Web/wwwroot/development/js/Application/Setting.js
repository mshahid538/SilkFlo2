// Add the model
if (SilkFlo.Application === undefined)
    SilkFlo.Application = {};

SilkFlo.Application.Setting = {

    // SilkFlo.Application.Setting.GetParent
    GetParent: function ()
    {
        const id = 'Application.Setting';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.Application.Setting.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },


    // SilkFlo.Application.Setting.CheckPasswordName
    CheckPasswordName: function ()
    {
        const parent = SilkFlo.Application.Setting.GetParent();
        if (!parent)
            return;


        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'PracticeAccountCanSignIn',
                'PracticeAccountPassword',
                'PracticeAccountPassword_InvalidFeedback',
                'PracticeAccountPassword_Label'],
            parent.id + '.');

        const checkElement = parent.ModelElements.PracticeAccountCanSignIn;
        const inputElement = parent.ModelElements.PracticeAccountPassword;
        const invalidElement = parent.ModelElements.PracticeAccountPassword_InvalidFeedback;
        const labelElement = parent.ModelElements.PracticeAccountPassword_Label;

        if (checkElement.checked)
        {
            labelElement.classList.add ('mandatory');
        } else
        {
            labelElement.classList.remove ('mandatory');
        }

        if (inputElement.value.length === 0 && checkElement.checked)
        {
            inputElement.classList.add ('is-invalid');
            invalidElement.innerHTML = 'Password required if can sign in is true';
        } else
        {
            inputElement.classList.remove ('is-invalid');
            invalidElement.innerHTML = '';
        }
    },


    // SilkFlo.Application.Setting.Save
    Save: function ()
    {
        const parentId = 'Application.Setting';
        const parent = document.getElementById (parentId);

        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.Application.Setting.Save: ';
            console.log
                (`${logPrefix}Element with id "${parentId}" missing`);
            return;
        }

        const model = SilkFlo.Models.Abstract.GetModelFromParent (
            parent,
            [
                'PracticeAccountCanSignIn',
                'PracticeAccountPassword',
                'TestEmailAccount',
                'TrialPeriod'],
            parent.id + '.');

        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.Application.Setting.ReturnToPreviousPage,
            SilkFlo.DataAccess.Feedback,
            parent.id,
            '/api/Application/Settings/Post');
    },

    Cancel: function ()
    {
        SilkFlo.Application.Setting.ReturnToPreviousPage();
    },

    ReturnToPreviousPage: function ()
    {
        let url = '';
        if (SilkFlo)
        {
            if (SilkFlo.SideBar)
            {
                url = SilkFlo.SideBar.ReturnURL;
            }
        }


        if (url)
        {
            window.location.href = url;
        } else
        {
            SilkFlo.SideBar.OnClick('/dashboard');
        }
    }
};