if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};


SilkFlo.ViewModels.Settings.CollaboratorRole = {

    // SilkFlo.ViewModels.Settings.CollaboratorRole.GetParent
    GetParent: function () {
        const id = 'Settings.Roles.Details';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.CollaboratorRole.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },


    // SilkFlo.ViewModels.Settings.CollaboratorRole.Select
    Select: function (selectedElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.CollaboratorRole.Select: ';

        const selectBar = document.getElementById('Settings.Roles.SelectBar');

        // Apply selector
        if (selectedElement)
        {

            const headerElementId = 'Settings.Business.Role.Header';
            const headerElement = document.getElementById(headerElementId);

            if (!headerElement)
            {
                console.log(`${logPrefix}Element with id "${headerElementId}" missing`);
                return;
            }


            // Get Position
            const selectBarTop = headerElement
                .getBoundingClientRect()
                .top;

            const selectedTop = selectedElement
                .getBoundingClientRect()
                .top;

            const deltaY = selectedTop - selectBarTop;


            // Get Matrix
            const matrix = `matrix( 1, 0, 0, 1, 1, ${deltaY})`;


            // Apply Matrix


            if (selectBar)
            {
                selectBar.style.display = 'block';
                selectBar.style.transform = matrix;
            }
        }
        else
        {
                selectBar.style.display = 'none';
        }



        // Deselect rows
        const tableBodyId = 'Business.Role.TableBody';
        const tableBody = document.getElementById(tableBodyId);
        const children = tableBody.children;

        // Guard Clause
        if (!tableBody)
        {
            console.log(`${logPrefix}Element with id "${tableBodyId}" missing`);
            return;
        }


        const length = children.length;
        for (let i = 0; i < length; i++)
        {
            children[i].style.outline = 'none';
        }


        // Select row
        if (selectedElement)
        {
            selectedElement.style.outline = '1px solid var(--bs-gray-lighter)';
        }
    },


    // SilkFlo.ViewModels.Settings.CollaboratorRole.Search
    Search: function (searchText, targetElementId, selectedId)
    {
        if (!searchText)
        {
            const element = document.getElementById('Settings.Roles.Search');

            //Guard Clause
            if (!element)
            {
                const logPrefix = 'SilkFlo.ViewModels.Settings.CollaboratorRole.Search: ';
                console.log(logPrefix + 'Element with id "Settings.Roles.Search" missing');
                return;
            }

            searchText = element.value;
        }


        let isSearchText = true;
        if (!searchText)
        {
            isSearchText = false;
        }


        let url = '/api/Business/Role/Table';
        if (isSearchText)
        {
            url += `/Search/${searchText}`;
        }

        if (selectedId)
        {
            url += `/SelectedId/${selectedId}`;
        }

        SilkFlo.DataAccess.UpdateElementById
        (
            url,
            null,
            targetElementId );
    },

    // SilkFlo.ViewModels.Settings.CollaboratorRole.GetDetail
    GetDetail: function (id)
    {
        let url = '/api/Business/Role/GetDetail';
        if (id)
        {
            url += `/id/${id}`;
        }
        else
        {
            SilkFlo.ViewModels.Settings.CollaboratorRole.Select();
        }

        const parent = SilkFlo.ViewModels.Settings.CollaboratorRole.GetParent();

        if (!parent)
            return;

        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            parent);
    },


    // SilkFlo.ViewModels.Settings.CollaboratorRole.Save
    Save: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.CollaboratorRole.Save: ';
        const parent = SilkFlo.ViewModels.Settings.CollaboratorRole.GetParent();
        if (!parent)
            return;



        const model = SilkFlo.Models.Business.Role.GetModelFromParent(
            parent,
            [
                'Id',
                'Description',
                'Name']);

        SilkFlo.ViewModels.Settings.CollaboratorRole.Model = model;


        const ideaAuthorisations = SilkFlo.Models.Shared.IdeaAuthorisation.GetModelCollectionFromParentById(
            'Model.Shared.IdeaAuthorisations',
            'Shared.IdeaAuthorisation',
            [
                'Id',
                'IsSelected',
                'Name']);


        // Guard Clause
        if (!ideaAuthorisations)
        {
            console.log(logPrefix + 'Idea Authorisations missing');
            return;
        }

        model.IdeaAuthorisations = ideaAuthorisations;

        // Update row
        const row = document.getElementById(model.Id);

        // Guard Clause
        if (!row) {
            console.log(`${logPrefix}tr element with id ${model.Id} missing`);
            return;
        }

        row.classList.remove ( 'pulse-background-green' );

        SilkFlo.Models.Business.Role.Save(
            model,
            SilkFlo.ViewModels.Settings.CollaboratorRole.UpdateSummary,
            SilkFlo.DataAccess.Feedback,
            parent.id,
            '/api/Business/Role/Post');
    },

    UpdateSummary: function (id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.CollaboratorRole.UpdateSummary: ';



        if (!SilkFlo.ViewModels.Settings.CollaboratorRole.Model)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Settings.CollaboratorRole.Model missing');
            return;
        }

        const model = SilkFlo.ViewModels.Settings.CollaboratorRole.Model;


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

            row.classList.add('pulse-background-green');

            // ToDo: Remove commented out code
            //SilkFlo.DataAccess.UpdateTargetElementWithURL(row, `/api/Business/Role/TableRow/${model.Id}`);

            const name = 'Name';
            const element = row.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            SilkFlo.DataAccess.UpdateTargetElementWithURL(element, `/api/Business/Role/TableRow/${model.Id}`);

        }
        else
        {
            // Reload table
            const elementId = 'Settings.Roles.Search';
            const searchElement = document.getElementById(elementId);

            // Guard Clause
            if (!searchElement)
            {
                console.log(logPrefix + 'tr element with id "' + elementId + '" missing');
                return;
            }

            const searchText = searchElement.value;

            const targetElementId = 'Settings.Roles.Table';
            SilkFlo.ViewModels.Settings.CollaboratorRole.Search(
                searchText,
                targetElementId,
                id);


            const parent = SilkFlo.ViewModels.Settings.CollaboratorRole.GetParent ();
            if (!parent)
                return;

            const name = 'Business.Role.Id';
            const elementRoleId = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!elementRoleId) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            elementRoleId.value = id;
        }
    },


    // SilkFlo.ViewModels.Settings.CollaboratorRole.Delete
    Delete: function ()
    {
        const parent = SilkFlo.ViewModels.Settings.CollaboratorRole.GetParent();
        if (!parent)
            return;

        const model = SilkFlo.Models.Business.Role.GetModelFromParent(
            parent,
            [
                'Id',
                'Name',
                'UserCount']);

        let additionalMessage = '';
        if (model.UserCount
         && model.UserCount !== '0')
        {
            if (model.UserCount === 1)
            {
                additionalMessage = `There is ${model.UserCount} user assigned to this role.`;
            }
            else
            {
                additionalMessage = `There are ${model.UserCount} users assigned to this role.`;
            }
        }


        const url = `/api/Model/Business/Role/Delete/Id/${model.Id}`;

        SilkFlo.Models.Abstract.Delete(
            url,
            model.Name,
            additionalMessage,
            SilkFlo.ViewModels.Settings.CollaboratorRole.Delete_Callback,
            SilkFlo.DataAccess.Feedback,
            parent.id);
    },


    // SilkFlo.ViewModels.Settings.CollaboratorRole.Delete_Callback
    Delete_Callback: function (ignored, roleId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.CollaboratorRole.Delete_Callback: ';



        const role = document.getElementById(roleId);

        // Guard Clause
        if (!role) {
            console.log(`${logPrefix}Element with id ${roleId} missing`);
            return;
        }


        const parent = role.parentElement;

        if (parent.children.length === 0)
        {
            const id = 'Settings.Roles.Details';
            const element = document.getElementById(id);

            // Guard Clause
            if (!element) {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            element.innerHTML = '';

            return;
        }


        const firstChild = parent.children[0];
        SilkFlo.ViewModels.Settings.CollaboratorRole.Select(firstChild);
        SilkFlo.ViewModels.Settings.CollaboratorRole.GetDetail(firstChild.id);

        role.remove ();
    }
};