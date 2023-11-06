if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};


// This is a hack.
// For some reason on click can not find SilkFlo.ViewModels.Settings 'namespace'
// After a row is inserted after save.
function SettingsPeopleRowClick (element)
{
    SilkFlo.ViewModels.Settings.People.Row_Click ( element );
};

function isOnClickCalled() {

    const element1 = document.querySelector("[name='Settings.PlatformSetup.CostSetup.Tab']");
    element1.classList.remove("active");

    const element2 = document.querySelector("[name='Settings.PlatformSetup.Applications.Tab']");
    element2.classList.remove("active"); 

    const element3 = document.querySelector("[name='Settings.PlatformSetup.BusinessUnits.Tab']");
    element3.classList.remove("active"); 

    const element4 = document.querySelector("[name='Document.Tab']");
    element4.classList.add("active"); 


    const element5 = document.querySelector("[name='Settings.PlatformSetup.BusinessUnits.Content']");
    element5.style.display = "none"; 

    const element6 = document.querySelector("[name='Settings.PlatformSetup.Applications.Content']");
    element6.style.display = "none";

    const element7 = document.querySelector("[name='Settings.PlatformSetup.CostSetup.Content']");
    element7.style.display = "none";

    const element8 = document.querySelector("[name='Document.Content']");
    element8.style.display = "";

    $.get("/api/business/idea/detail/TemplateDocumentation/ideaId/GetTemplates", function (response) {
        const element9 = document.querySelector("[name='Document.Content.SubContent']");
        element9.innerHTML = response;
    });


}

SilkFlo.ViewModels.Settings.People = {

    // SilkFlo.ViewModels.Settings.People.GetParent
    GetParent: function ()
    {
        const id = 'Settings.People.Modal';
        const parent = document.getElementById(id);

        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.People.GetParent: ';
            console.log(`${logPrefix}Element with id "${id}" missing`);
            return null;
        }

        return parent;
    },


    // SilkFlo.ViewModels.Settings.People.Row_Click
    Row_Click: function (element)
    {
        if (HotSpot.Card)
             HotSpot.Card.Close();

        SilkFlo.ViewModels.Settings.People.PopulateModal(element.id);
    },

    // SilkFlo.ViewModels.Settings.People.SearchPeople
    SearchPeople: function (
        searchText,
        page,
        targetElementId,
        insertFirstId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.People.SearchPeople: ';

        if (!insertFirstId)
            insertFirstId = '';




        // Model
        const peopleSearch = {
            Text: searchText,
            PageNumber: 1,
            FirstUserId: insertFirstId
        };



        let id = 'Settings.People.GuestsOnly';
        const elementGuestsOnly = document.getElementById(id);

        if (elementGuestsOnly)
            peopleSearch.GuestOnly = elementGuestsOnly.checked;


        if (!peopleSearch.Text)
        {
            id = 'Settings.People.Search';
            const element = document.getElementById(id);

            //Guard Clause
            if (!element)
            {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            peopleSearch.Text = element.value;
        }



        const url = '/api/Settings/People/GetRows';

        SilkFlo.DataAccess.UpdateElementById (
            url,
            peopleSearch,
            targetElementId,
            null,
            'POST');
    },


    PopulateModal: function (userId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.People.PopulateModal: ';

        let url = '/api/User/GetModal';
        if (userId)
        {
            url += `/id/${userId}`;
        }



        let id = 'modelManageUserLabel';
        const modelManageUserLabel = document.getElementById(id);

        // Guard Clause
        if (!modelManageUserLabel) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        id = 'modelManageUser.btnDelete';
        const btnDelete = document.getElementById(id);

        // Guard Clause
        if (!btnDelete) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        if (userId)
        {
            modelManageUserLabel.innerHTML = 'Edit the User';
            btnDelete.style.display = 'block';
        }
        else
        {
            modelManageUserLabel.innerHTML = 'Add a New User';
            btnDelete.style.display = 'none';
        }



        SilkFlo.DataAccess.UpdateElementById(
            url,
            null,
            'Settings.People.Modal',
            '',
            'GET',
            SilkFlo.ViewModels.Settings.People.AfterModalLoaded);
    },


    // SilkFlo.ViewModels.Settings.People.AfterModalLoaded
    AfterModalLoaded: function ()
    {
        const parent = SilkFlo.ViewModels.Settings.People.GetParent();
        if (!parent)
            return;

        const name = 'User.IsGuestEmail';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.People.AfterModalLoaded: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        if (element.checked)
            SilkFlo.Radio_Click(element, SilkFlo.ViewModels.Settings.People.ToggleEmail);

        SilkFlo.ViewModels.Settings.People.SetFocusOnFistName ();
    },


    // SilkFlo.ViewModels.Settings.People.SetFocusOnFistName
    SetFocusOnFistName: function ()
    {
        const id = 'Modal.User.FirstName';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.People.SetFocusOnFistName: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        SilkFlo.SetFocus(element );
    },

    SavePerson: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.People.SavePerson: ';

        const parent = SilkFlo.ViewModels.Settings.People.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const model = SilkFlo.Models.User.GetModelFromParent(
            parent,
            [
                'Id',
                'FirstName',
                'LastName',
                'JobTitle',
                'DepartmentId',
                'EmailPrefix']);


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'User model missing');
            return;
        }

        let name = 'User.IsBusinessEmail';
        let element = parent.querySelector(`[name="${name}"]`);

        if (element)
        {
            let value = false;
            if (element.checked) {
                value = true;
            }

            model.IsBusinessEmail = value;
        }


        name = 'User.GuestEmail';
        element = parent.querySelector(`[name="${name}"]`);

        if (element)
            model.GuestEmail = element.value;


        const rolesElement = parent.querySelector('[name="user.Roles"]');
        const roleModels = SilkFlo.Models.Role.GetModelCollectionFromParent(
            rolesElement,
            'Role',
            [
                'Id',
                'Name',
                'IsSelected']);




        name = 'User.StandardUserRole';
        const elementStandardUserRole = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (elementStandardUserRole) {
            const model2 = SilkFlo.Models.User.GetModelFromParent(
                elementStandardUserRole,
                [
                    'Id',
                    'Name',
                    'IsSelected'],
                'User.StandardUserRole.');

            roleModels.push(model2);
        }


        // Guard Clause
        if (!roleModels)
        {
            console.log(logPrefix + 'Role model missing');
            return;
        }


        model.Roles = roleModels;


        SilkFlo.Models2.User.Model = model;

        const url = '/api/Models/User/Save';


        SilkFlo.Models.Business.Client.Save(
            model,
            SilkFlo.ViewModels.Settings.People.CloseModelAndUpdateTable,
            SilkFlo.DataAccess.Feedback,
            parent.id,
            url);
    },


    // SilkFlo.ViewModels.Settings.People.DeletePerson
    DeletePerson: function ()
    {
        const parent = SilkFlo.ViewModels.Settings.People.GetParent();
        // Guard Clause
        if (!parent)
            return;

        const model = SilkFlo.Models.User.GetModelFromParent(
            parent,
            [
                'Id',
                'Fullname',
                'CollaboratorCount']);

        if (!model)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.People.DeletePerson: ';
            console.log(logPrefix + 'model missing');
            return;
        }

        SilkFlo.ViewModels.Settings.People.DeleteId = model.Id;

        let message = '';

        if (model.CollaboratorCount
            && model.CollaboratorCount !== '0')
        {
            if (model.CollaboratorCount === '1')
            {
                message += '<br><b><span class="text-danger">They collaborate with 1 idea.</span></b>';
            }
            else
            {
                message += `<br><b><span class="text-danger">They collaborate with ${model.CollaboratorCount} ideas.</span></b>`;
            }
        }


        SilkFlo.Models.User.Delete(
            SilkFlo.ViewModels.Settings.People.RemoveRow,
            SilkFlo.ViewModels.Settings.People.DeleteFailed,
            SilkFlo.ViewModels.Settings.People.DeleteId,
            model.Fullname,
            message);

    },


    // SilkFlo.ViewModels.Settings.People.RemoveRow
    RemoveRow: function (id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.People.RemoveRow: ';
        console.log(logPrefix);
        if (!id)
        {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }

        const element = document.getElementById(id);

        if (!element)
        {
            console.log(`${logPrefix}Element with id "${id}" missing`);
            return;
        }


        element.classList.add('glow-background-red');

        setTimeout(() =>
        {
            element.remove();
        }, 1000);

    },

    DeleteFailed: function (message)
    {
        if (message)
        {
            const model = JSON.parse(message);
            message = model.error;
        }
        else
        {
            message = 'The user was not deleted';
        }

        bootbox.dialog({
            title: 'User Not Deleted',
            message: message,
            onEscape: true,
            backdrop: true,
            buttons: {
                Ok: {
                    label: 'Ok',
                    className: 'btn-danger'
                }
            }
        });
    },


    CloseModelAndUpdateTable: function (id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.People.CloseModelAndUpdateTable: ';

        const model = SilkFlo.Models2.User.Model;

        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'SilkFlo.Models2.User.Model missing');
        }

        if (model.Id)
        {
            // Update row
            const row = document.getElementById(model.Id);

            // Guard Clause
            if (!row)
            {
                console.log(`${logPrefix}tr element with id ${model.Id} missing`);
                return;
            }

            SilkFlo.DataAccess.UpdateTargetElementWithURL(row, `/api/Settings/Tenant/People/TableRow/${model.Id}`);
        }
        else
        {
            // Reload table
            let elementId = 'Settings.People.Search';
            const searchElement = document.getElementById(elementId);

            // Guard Clause
            if (!searchElement)
            {
                console.log(`${logPrefix}tr element with id "${elementId}" missing`);
                return;
            }

            const searchText = searchElement.value;


            elementId = 'Settings.People.Table.SelectedPage';
            const pageElement = document.getElementById(elementId);

            let page = '';
            if (pageElement)
            {
                page = pageElement.value;
            }



            const targetElementId = 'People.Table.Content';
            SilkFlo.ViewModels.Settings.People.SearchPeople(searchText, page, targetElementId, id);
        }

        window.$('#modelManageUser').modal('hide');
    },

    // SilkFlo.ViewModels.Settings.People.Row
    //Row: null,

    //AssignRowClick: function ()
    //{
    //    SilkFlo.ViewModels.Settings.People.Row.addEventListener('click', )
    //},

    // SilkFlo.ViewModels.Settings.People.SendInvitationClick
    SendInvitationClick: function (id)
    {
        const parent = SilkFlo.ViewModels.Settings.People.GetParent();

        // Guard Clause
        if (!parent)
            return;


        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.People.SendInvitationClick: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = '';

        SilkFlo.Models2.User.SendInvite(
            id,
            SilkFlo.ViewModels.Settings.People.SendInvitationCallback);
    },


    // SilkFlo.ViewModels.Settings.People.SendInvitationCallback
    SendInvitationCallback: function (str)
    {
        const parent = SilkFlo.ViewModels.Settings.People.GetParent();

        // Guard Clause
        if (!parent)
            return;


        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.People.SendInvitationCallback: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.innerHTML = str;
    },


    // SilkFlo.ViewModels.Settings.People.ToggleEmail
    ToggleEmail: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.People.ToggleEmail: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        const parent = SilkFlo.ViewModels.Settings.People.GetParent();

        if (!parent)
            return;


        const data = element.getAttribute('data');

        // Guard Clause
        if (!data) {
            console.log(`${logPrefix}data attribute missing`);
            return;
        }


        const name = 'user.Roles';
        const rolesElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!rolesElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }



        const children = rolesElement.children;
        const length = children.length;
        for (let i = 0; i < length; i++)
        {
            const child = children[i];

            const dataId = child.getAttribute('data');

            // Guard Clause
            if (!dataId) {
                console.log(`${logPrefix}data attribute missing`);
                return;
            }


            if (dataId === '-165')
            {
                if (data === 'User.Email')
                    child.removeAttribute('disabled');
                else
                    child.setAttribute('disabled', '');
            }
        }
    }
};