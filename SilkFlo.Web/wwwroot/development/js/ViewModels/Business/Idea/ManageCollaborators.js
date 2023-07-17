//ManageCollaborators

// Add the model
if (SilkFlo.ViewModels.Business === undefined)
    SilkFlo.ViewModels.Business = {};

if (SilkFlo.ViewModels.Business.Idea === undefined)
    SilkFlo.ViewModels.Business.Idea = {};

SilkFlo.ViewModels.Business.Idea.ManageCollaborators = {

    // SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ParentId
    ParentId: null,

    // SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetParent
    GetParent: function ()
    {
        const id = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ParentId;
        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },


    // SilkFlo.ViewModels.Business.Idea.ManageCollaborators.OpenModal
    OpenModal: function (
                   parentId,
                   canScroll = false)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.OpenModal: ';

        // Guard Clause
        if (!parentId) {
            console.log(`${logPrefix}parentId parameter missing`);
            return;
        }


        // Guard Clause Check Modal Exists
        const id = 'ManageCollaborators_Modal';
        const elementModal = document.getElementById(id);

        // Guard Clause
        if (!elementModal) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ParentId = parentId;
        SilkFlo.ViewModels.Business.Idea.ManageCollaborators.Scroll = canScroll;

        window.$(`#${id}`)
            .modal('show');
    },


    // SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetCollaboratorCount
    GetCollaboratorCount: function (parent)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetCollaboratorCount: ';

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return null;
        }



        const name = 'Business.Idea.Collaborators';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetCollaboratorCount: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return null;
        }


        return element.children.length;
    },

    Search_KeyUp: function (modalId, element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.Search: ';


        //Guard Clause
        if (!modalId)
        {
            console.log(logPrefix + 'modalId parameter missing');
            return;
        }

        //Guard Clause
        if (!element)
        {
            console.log(logPrefix + 'element parameter missing');
            return;
        }


        const modal = document.getElementById(modalId);


        SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ModalId = modalId;

        //Guard Clause
        if (!modal)
        {
            console.log(logPrefix + 'element with id "modalId" missing');
            return;
        }


        // Get SearchResults element
        const searchResultsElement = modal.querySelector ( '[name="SearchResults"]' );

        //Guard Clause
        if (!searchResultsElement)
        {
            console.log(logPrefix + 'element with name "SearchResults" missing');
            return;
        }


        // Get SearchResults element
        const collaboratorSelected = modal.querySelector ( '[name="Collaborator_Selected"]' );

        //Guard Clause
        if (!collaboratorSelected)
        {
            console.log(logPrefix + 'element with name "Collaborator_Selected" missing');
            return;
        }




        if (element.value)
        {
            const url = `/api/User/GetCollaborators/Search/${element.value}`;
            SilkFlo.DataAccess.UpdateElement(url, null, searchResultsElement);
        }
        else
        {
            searchResultsElement.innerHTML = '<div name="Collaborators_Potential"> </div>';
        }
    },

    Add: function (event, id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.Add: ';

        event.stopPropagation();


        //Guard Clause
        if (!id)
        {
            console.log(logPrefix + 'id parameter missing');
            return;
        }

        const modalId = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ModalId;
        const modal = document.getElementById(modalId);

        //Guard Clause
        if (!modal)
        {
            console.log(logPrefix + 'modal element with id "' + modalId + '" missing');
            return;
        }


        const collaboratorsPotential = modal.querySelector('[name="Collaborators_Potential"]');

        //Guard Clause
        if (!collaboratorsPotential)
        {
            console.log(logPrefix + 'element with name "Collaborators_Potential" missing');
            return;
        }


        const collaboratorSelected = modal.querySelector('[name="Collaborator_Selected"]');

        //Guard Clause
        if (!collaboratorSelected)
        {
            console.log(logPrefix + 'element with name "Collaborator_Selected" missing');
            return;
        }

        if (collaboratorSelected.childElementCount > 0)
        {
            const old = collaboratorSelected.children[0];
            collaboratorsPotential.prepend(old);
            old.classList.remove('pulse-background-green');
            old.classList.add('pulse-background-red');
        }


        const element = modal.querySelector('[name="' + id + '"]');


        //Guard Clause
        if (!element)
        {
            console.log(logPrefix + 'Element with name "' + id + '" missing');
            return;
        }

        collaboratorSelected.prepend(element);
        element.classList.remove('pulse-background-red');
        element.classList.add('pulse-background-green');

        if (collaboratorsPotential.children.length === 0)
        {
            collaboratorsPotential.classList.remove('Collaborators_Potential');
        }


        const fullnameDisplay = element.querySelector('[name="User.FullnameDisplay"]');
        fullnameDisplay.classList.add('pulse-font-weight');

    },

    Deselect: function (id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.Deselect: ';


        if (!id)
            console.log(logPrefix + "id parameter is missing");


        const modalId = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ModalId;
        const modal = document.getElementById(modalId);

        //Guard Clause
        if (!modal)
        {
            console.log(logPrefix + 'modal element with id "' + modalId + '" missing');
            return;
        }


        const collaboratorsPotential = modal.querySelector('[name="Collaborators_Potential"]');

        //Guard Clause
        if (!collaboratorsPotential)
        {
            console.log(logPrefix + 'element with name "Collaborators_Potential" missing');
            return;
        }

        collaboratorsPotential.classList.add('Collaborators_Potential');

        const collaboratorSelected = modal.querySelector('[name="Collaborator_Selected"]');

        //Guard Clause
        if (!collaboratorSelected)
        {
            console.log(logPrefix + 'element with name "Collaborator_Selected" missing');
            return;
        }


        const element = modal.querySelector('[name="' + id + '"]');


        //Guard Clause
        if (!element)
        {
            console.log(logPrefix + 'Element with name "' + id + '" missing');
            return;
        }

        collaboratorsPotential.prepend(element);
        element.classList.remove('pulse-background-green');
        element.classList.add('pulse-background-red');
    },

    Select: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.Select: ';

        const id = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ModalId;
        let parent = document.getElementById(id);

        //Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}modal element with id "${id}" missing`);
            SilkFlo.ShowOKMessage('Missing collaborator', 'Please search for then select a collaborator.');
            return;
        }


        const collaboratorSelected = parent.querySelector('[name="Collaborator_Selected"]');

        //Guard Clause
        if (!collaboratorSelected)
        {
            console.log(logPrefix + 'element with name "Collaborator_Selected" missing');
            return;
        }


        const collaboratorsPotential = parent.querySelector('[name="Collaborators_Potential"]');

        //Guard Clause
        if (!collaboratorsPotential)
        {
            console.log(logPrefix + 'element with name "Collaborators_Potential" missing');
            return;
        }




        const children = collaboratorSelected.children;

        //Guard Clause
        if (children.length === 0)
        {
            console.log(logPrefix + 'No users selected');
            SilkFlo.ShowOKMessage('Missing Collaborator', 'Please search for then select a collaborator.');
            return;
        }


        const userElement = children[0];

        //Guard Clause
        if (!userElement)
        {
            console.log(logPrefix + 'element with name "User" missing');
            return;
        }
        
        const model = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetModel(userElement);


        if (model.Roles.length === 0)
        {
            SilkFlo.ShowOKMessage('Missing Roles', `Please select roles to be assigned to ${model.Fullname}.`);
            return;
        }


        if (SilkFlo.ViewModels.Business.Idea.ManageCollaborators.CreateCard ( model ))
        {
            window.$ ( `#${id}` )
                .modal ( 'hide' );

            if (SilkFlo.ViewModels.Business.Idea.ManageCollaborators.Scroll)
                window.scrollTo (
                    0,
                    document.body.scrollHeight );
        }
        else
        {
            console.log(logPrefix + 'Card creation failed');
            return;
        }


        const searchResults = collaboratorsPotential.children;

        //Guard Clause
        const length = searchResults.length;
        if (searchResults.length > 0)
        {
            for (let i = 0; i < length; i++)
            {
                const searchElement = searchResults[i];
                searchElement.classList.remove('pulse-background-red');

                const spanElement = searchElement.querySelector('[name="User.FullnameDisplay"]');
                if (!spanElement)
                {
                    console.log(logPrefix + 'element with name "User.FullnameDisplay" missing');
                    return;
                }

                spanElement.classList.remove('pulse-font-weight');
            }
        }

        parent = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetParent();
        if (!parent)
            return;

        SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ToggleAddCollaboratorsButton(parent);
    },

    ToggleAddCollaboratorsButton: function (parent)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ToggleAddCollaboratorsButton: ';


        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }



        let name = 'btnAddCollaborators';
        const btnElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!btnElement) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'CollaboratorMessage';
        const messageElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!messageElement) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'CollaboratorLimit';
        const elementCollaboratorLimit = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementCollaboratorLimit) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        const collaboratorLimit = Number(elementCollaboratorLimit.value);


        const count = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetCollaboratorCount(parent);

        if (!count)
            return;

        if (collaboratorLimit <= count) {
            btnElement.style.display = 'none';
            messageElement.style.display = '';
        }
        else {
            btnElement.style.display = '';
            messageElement.style.display = 'none';
        }
    },


    // SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetModel
    GetModel: function (userElement)
    {

        if (!userElement)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.GetModel: ';
            console.log(`${logPrefix}userElement parameter is missing`);
            return null;
        }

        const user = SilkFlo.Models.User.GetModelFromParent(
            userElement,
            ['Id', 'Fullname', 'Email', 'FirstName', 'LastName', 'Status']);


        const roles = SilkFlo.Models.Business.Role.GetModelCollectionFromParent (
            userElement,
            'Business.Role',
            [
                'Id', 'IsSelected', 'Name'
            ] );

        user.Roles = [];

        const length = roles.length;
        for (let i = 0; i < length; i++)
        {
            if (roles[i].IsSelected)
            {
                user.Roles.push(roles[i]);
                user.RolesSelected = true;
            }
        }

        if (user.RolesSelected)
        {
            userElement.remove();
        }

        return user;
    },


    CreateCard: function (model)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.CreateCard: ';

        if (!model)
        {
            console.log(logPrefix + 'model parameter is missing');
            return false;
        }

        // Guard Clause
        if (model.Roles.length === 0)
        {
            console.log(logPrefix + 'No roles');
            return false;
        }

        if (!SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ParentId)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ParentId missing');
            return false;
        }

        let id = SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ParentId;
        const form = document.getElementById(id);

        // Guard Clause
        if (!form)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return false;
        }


        let name = 'Business.Idea.Collaborators';
        const targetElement = form.querySelector(`[name="${name}"]`);

        //Guard Clause
        if (!targetElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return false;
        }



        id = 'Template.SelectedCollaborator';
        const template = document.getElementById(id);

        //Guard Clause
        if (!template)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return false;
        }


        id = 'Template.BusinessRole';
        const templateRole = document.getElementById(id);

        //Guard Clause
        if (!templateRole)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return false;
        }


        const currentCollaboratorElement = document.getElementById(model.Id);
        if (currentCollaboratorElement)
        {
            name = 'Business.Roles';
            const rolesElement = currentCollaboratorElement.querySelector(`[name="${name}"]`);

            if (!rolesElement)
            {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return false;
            }

            let content = '';
            let roleContent = templateRole.innerHTML;
            const roleLength = model.Roles.length;
            for (let i = 0; i < roleLength; i++)
            {
                content += roleContent;
                content = content.replaceAll('{id}', model.Roles[i].Id);
                content = content.replaceAll('{name}', model.Roles[i].Name);
            }

            rolesElement.innerHTML = content;
            return true;
        }


        let s = template.innerHTML;
        s = s.replaceAll('{id}', model.Id);
        s = s.replaceAll('{firstname} {lastname}', model.Fullname);
        s = s.replaceAll('{firstname}', model.FirstName);
        s = s.replaceAll('{lastname}', model.LastName);
        s = s.replaceAll('{email}', model.Email);
        s = s.replaceAll('{status}', model.Status);


        let r = '';
        let sRole = templateRole.innerHTML;
        const length = model.Roles.length;
        for (let i = 0; i < length; i++)
        {
            r += sRole;
            r = r.replaceAll('{id}', model.Roles[i].Id);
            r = r.replaceAll('{name}', model.Roles[i].Name);
        }

        s = s.replaceAll('<span>roles</span>',r);

        targetElement.innerHTML += s;

        return true;
    },

    // SilkFlo.ViewModels.Business.Idea.ManageCollaborators.Remove
    Remove: function (id, fullname)
    {
        SilkFlo.ViewModels.Business.Idea.ManageCollaborators.CollaboratorId = id;

        SilkFlo.ShowYesNoMessage(
            'Remove Collaborator',
            `Are you sure you would like to remove the collaborator ${fullname}?`,
            SilkFlo.ViewModels.Business.Idea.ManageCollaborators.RemoveMessage_Yes);
    },

    // SilkFlo.ViewModels.Business.Idea.ManageCollaborators.RemoveMessage_Yes
    RemoveMessage_Yes: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.ManageCollaborators.RemoveMessage_Yes: ';

        // Guard Clause
        if (!SilkFlo.ViewModels.Business.Idea.ManageCollaborators.CollaboratorId)
        {
            console.log(logPrefix + 'collaborator id missing');
            return;
        }

        const element = document.getElementById(SilkFlo.ViewModels.Business.Idea.ManageCollaborators.CollaboratorId);

        let parent = element.parentElement;

        let name = 'Business.Idea.Collaborators';

        // Guard Clause
        if (!parent && parent.name !== name)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        parent = parent.parentElement;
        name = 'Business.Idea.CollaboratorSummary';

        // Guard Clause
        if (!parent && parent.name !== name)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        element.remove();

        SilkFlo.ViewModels.Business.Idea.ManageCollaborators.ToggleAddCollaboratorsButton(parent);
    }
};