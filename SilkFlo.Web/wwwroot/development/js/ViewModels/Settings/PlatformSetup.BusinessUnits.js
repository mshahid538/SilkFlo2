if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};


if (!SilkFlo.ViewModels.Settings.PlatformSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup = {};


// BusinessUnits Namespace: Code to manage the content inside the Business Units tab
SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits = {

    SelectedColumn: null,
    SelectedHeader: null,
    SelectedCell: null,
    DivCell: null,
    OldNameCell: null,

    DepartmentId: null,
    TeamId: null,

    DeleteToolTip: null,
    DeleteButtonElement: null,

    EditToolTip: null,
    EditButtonElement: null,


    // SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.GetParent
    GetParent: function ()
    {
        const id = 'Settings.PlatformSetup.BusinessUnits.Container';

        const element = document.getElementById(id);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SetMessage
    SetMessage: function (text, cls)
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.GetParent();

        if (!parent)
            return;

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        element.setAttribute('class', cls);
        element.innerHTML = text;
    },


    SelectHeader_Click: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectHeader_Click: ';

        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click();

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }



        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectHeader(element);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView(element.parentElement.parentElement);
    },


    Select_Click: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Select_Click: ';


        // Check if in click zone
        if (SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell)
        {
            if (event.target === SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell)
            {
                // Is edit zone
                return;
            }

            if (event.target === SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell.parentElement)
            {
                // Is edit zone
                return;
            }
        }


        // Cancel previous edit if applicable
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click();


        // Get the element
        const element = event.currentTarget;


        // Get the components
        const listElement = element.parentElement;
        const listElement2 = listElement.parentElement;
        const tableElement = listElement2.parentElement;
        const name = 'Header';
        const headerElement = tableElement.querySelector(`[name="${name}"]`);
    // Guard Clause
        if (!headerElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }
        // Do the business
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectHeader(headerElement);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Select(element);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView();
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.GetList (element);
    },
        
    DeSelectHeader: function (tableElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectHeader';

        const columns = tableElement.children;
        const length = columns.length;
        for (let i = 0; i < length; i++)
        {
            const column = columns[i];
            const cells = column.children;
            const length2 = cells.length;

            if (length2 !== 2)
            {
                console.log(logPrefix + 'cell count is not correct');
            }

            for (let j = 0; j < length2; j++)
            {
                const cell = cells[j];
                cell.classList.remove('select');
            }
        }
    },

    // Select the table header
    SelectHeader: function (headerElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectHeader: ';

        // Guard Clause
        if (!headerElement)
        {
            console.log(`${logPrefix}headerElement parameter missing`);
            return;
        }





        const parent = headerElement.parentElement;

        // De-select other columns
        const tableElement = parent.parentElement;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectHeader(tableElement);



        // Select Column
        const name = 'List';
        const listElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!listElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        headerElement.classList.add('select');
        listElement.classList.add('select');




        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedColumn = parent;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedHeader = headerElement;


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateNewButtonToolTip(`Create new ${headerElement.innerHTML}`);
    },

    ShowNewToolTip: function ()
    {
        const toolTip = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.NewToolTip;
        const element = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.NewButtonElement;
        Delaney.UI.ToolTip.Show(event, toolTip, element);
    },

    UpdateNewButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateNewButtonToolTip: ';

        if (!text)
        {
            return;
        }


        const elementId = 'Settings.PlatformSetup.BusinessUnits.Container';
        const contentElement = document.getElementById(elementId);

        // Guard Clause
        if (!contentElement)
        {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return;
        }



        const elementName = 'NewButton';
        const newButtonElement = contentElement.querySelector(`[name="${elementName}"]`);

        // Guard Clause
        if (!newButtonElement)
        {
            console.log(`${logPrefix}Element with name ${elementName} missing`);
            return;
        }


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.NewToolTip = text;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.NewButtonElement = newButtonElement;

        newButtonElement.onmousemove = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.ShowNewToolTip;
        newButtonElement.onmouseout = Delaney.UI.ToolTip.Hide;

    },

    UpdateEditButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateEditButtonToolTip: ';

        if (!text)
        {
            return;
        }


        const elementId = 'Settings.PlatformSetup.BusinessUnits.Container';
        const contentElement = document.getElementById(elementId);

        // Guard Clause
        if (!contentElement)
        {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return;
        }


        //const elementName = 'EditButton';
        //const editButtonElement = contentElement.querySelector(`[name="${elementName}"]`);

        //// Guard Clause
        //if (!editButtonElement)
        //{
        //    console.log(`${logPrefix}Element with name ${elementName} missing`);
        //    return;
        //}

        //SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.EditToolTip = text;
        //SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.EditButtonElement = editButtonElement;

        //editButtonElement.onmousemove = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.ShowEditToolTip;
        //editButtonElement.onmouseout = Delaney.UI.ToolTip.Hide;
    },

    UpdateDeleteButtonToolTip: function (text) {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateDeleteButtonToolTip: ';

        if (!text) {
            return;
        }


        const elementId = 'Settings.PlatformSetup.BusinessUnits.Container';
        const contentElement = document.getElementById(elementId);

        // Guard Clause
        if (!contentElement) {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return;
        }


        //const elementName = 'DeleteButton';
        //const deleteButtonElement = contentElement.querySelector(`[name="${elementName}"]`);

        //// Guard Clause
        //if (!deleteButtonElement) {
        //    console.log(`${logPrefix}Element with name ${elementName} missing`);
        //    return;
        //}

        //SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeleteToolTip = text;
        //SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeleteButtonElement = deleteButtonElement;

        //deleteButtonElement.onmousemove = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.ShowDeleteToolTip;
        //deleteButtonElement.onmouseout = Delaney.UI.ToolTip.Hide;
    },

    DeSelectCells: function (listElement)
    {
        
        // Guard Clause
        if (!listElement)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectCells: ';
            console.log(`${logPrefix}listElement parameter missing`);
            return;
        }
        const children = listElement.children;
        if (!children)
        {
            return;
        }
        const length = children.length;
        for (let i = 0; i < length; i++)
        {
            const cell = children[i];
            cell.classList.remove ('select');
        }
    },


    DeSelectrRows: function (list) {

        
        // Guard Clause
        if (!list) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectrRows: ';
            console.log(`${logPrefix}list parameter missing`);
            return;
        }
        const ListChild = list.children;
        if (!ListChild) {
            return;
        }
        const length = ListChild.length;
        for (let i = 0; i < length; i++) {
            const RowsChild = ListChild[i].children;
            if (!RowsChild) {
                continue;
            }
            for (let j = 0; j < RowsChild.length; j++) {
                const cell = RowsChild[j];
                if (cell && cell.classList) {
                    cell.classList.remove('select');
                }
            }
        }
    },

    Select: function (element)
    {
        
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Select: ';
        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }
        const listElement = element.parentElement;
        const list = listElement.parentElement;
        const listChild = listElement.children[0];
        const columnElement2 = listElement.parentElement;
        const columnElement = columnElement2.parentElement;
        const headerElement = columnElement.children[0];
       // SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectHeader(headerElement); 
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectCells(headerElement);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectrRows(list);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectHeader(headerElement); 
        //SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectCells(columnElement);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell = element;
        element.classList.add('select');

        const name = element.getAttribute('name');
        if (name === 'Business.Department')
        {
            const idPropertyName = 'Business.Department.Id';
            const idProperty = element.querySelector('[name="' + idPropertyName + '"]');

            // Guard Clause
            if (!idProperty) {
                console.log(`${logPrefix}Element with name ${idProperty} missing`);
                return;
            }


            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DepartmentId = idProperty.value;
        }
        else if (name === 'Business.Team')
        {
            const idPropertyName = 'Business.Team.Id';
            const idProperty = element.querySelector('[name="' + idPropertyName + '"]');

            // Guard Clause
            if (!idProperty) {
                console.log(`${logPrefix}Element with name ${idProperty} missing`);
                return;
            }

            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.TeamId = idProperty.value;
        }


        const content = element.children[0];
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateEditButtonToolTip(`Edit ${content.innerHTML}`);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateDeleteButtonToolTip(`Delete ${content.innerHTML}`);

    },

    ShowEditToolTip: function ()
    {
        const toolTip = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.EditToolTip;
        const element = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.EditButtonElement;
        Delaney.UI.ToolTip.Show(event, toolTip, element);
    },

    ShowDeleteToolTip: function () {
        const toolTip = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeleteToolTip;
        const element = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeleteButtonElement;
        Delaney.UI.ToolTip.Show(event, toolTip, element);
    },

    UpdateView: function () {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView: ';

        const elementId = 'Settings.PlatformSetup.BusinessUnits.Container';
        const contentElement = document.getElementById(elementId);

        // Guard Clause
        if (!contentElement) {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return;
        }


        let name = 'NewButton';
        const newButton = contentElement.querySelector(`[name="${name}"]`);
        
        // Guard Clause
        if (!newButton) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        //name = 'EditButton';
        //const editButton = contentElement.querySelector(`[name="${name}"]`);

        //// Guard Clause
        //if (!editButton) {
        //    console.log(`${logPrefix}Element with name ${name} missing`);
        //    return;
        //}


        //name = 'DeleteButton';
        //const deleteButton = contentElement.querySelector(`[name="${name}"]`);

        //// Guard Clause
        //if (!deleteButton) {
        //    console.log(`${logPrefix}Element with name ${name} missing`);
        //    return;
        //}



        // Show/Hide New Button
        if (SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedHeader) {
            newButton.classList.remove('hide');
        }
        else {
            newButton.classList.add('hide');
        }


        // Show/Hide Edit Button
        //if (SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell) {
        //    editButton.classList.remove('hide');
        //    deleteButton.classList.remove('hide');
        //}
        //else {
        //    editButton.classList.add('hide');
        //    deleteButton.classList.add('hide');
        //}







         name = 'SaveButton';
        const saveButton = contentElement.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!saveButton) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'CancelButton';
        const cancelButton = contentElement.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!cancelButton) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }








        if (SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell) {
            cancelButton.classList.remove('hide');
            saveButton.classList.remove('hide');

            newButton.classList.add('hide');
            //editButton.classList.add('hide');
            //deleteButton.classList.add('hide');
        }
        else {
            cancelButton.classList.add('hide');
            saveButton.classList.add('hide');


            //// New Button
            if (SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedColumn) {
                newButton.classList.remove('hide');
            }
            else {
                newButton.classList.add('hide');
            }



            //if (SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell) {
            //    editButton.classList.remove('hide');
            //    deleteButton.classList.remove('hide');
            //}
            //else {
            //    editButton.classList.add('hide');
            //    deleteButton.classList.add('hide');
            //}
        }
    },

    GetList: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.GetList: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }



        const targetTableName = element.getAttribute('targetName');


        // Guard Clause
        if (!targetTableName)
        {
            return;
        }

        const contentId = 'Settings.PlatformSetup.BusinessUnits.Container';
        const contentElement = document.getElementById(contentId);

        // Guard Clause
        if (!contentElement)
        {
            console.log(`${logPrefix}Element with id ${contentId} missing`);
            return;
        }


        const targetTableElement = contentElement.querySelector(`[name="${targetTableName}"]`);

        // Guard Clause
        if (!targetTableElement)
        {
            console.log(`${logPrefix}Element with name ${targetTableName} missing`);
            return;
        }


        targetTableElement.style.display = 'flex';

        const name = 'List';
        const targetElement = targetTableElement.querySelector(`[name="${name}"]`);
        // Guard Clause
        if (!targetElement) {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        let url = element.getAttribute('url');

        // Guard Clause
        if (!url)
        {
            console.log(logPrefix + 'url attribute missing');
            return;
        }


        if (targetTableName === 'Area')
        {
            const subAreaName = 'SubArea';
            const subAreaElement = contentElement.querySelector(`[name="${subAreaName}"]`);

            if (!subAreaElement)
            {
                console.log(`${logPrefix}Element with name ${subAreaName} missing`);
                return;
            }


            const subAreaListName = 'List';
            const subAreaListElement = subAreaElement.querySelector(`[name="${subAreaListName}"]`);

            if (!subAreaListElement) {
                console.log(`${logPrefix}Element with name ${subAreaName} missing`);
                return;
            }

            subAreaListElement.innerHTML = '';
        }


        url = `/api/Settings/PlatformSetup/BusinessUnits/${url}`;


       

        SilkFlo.DataAccess.UpdateElement (
            url,
            null,
            targetElement );
    },

    Dbl_Click: function (event)
    {
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Select_Click(event);
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Edit_Click();
    },

    Edit_Click: function ()
    {
        
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Edit_Click: ';


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click ();
        


        const selectedCellElement = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell;

        // Guard Clause
        if (!selectedCellElement)
        {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell missing`);
            return;
        }



        const textBoxElement = selectedCellElement.querySelector('div');

        // Guard Clause
        if (!textBoxElement)
        {
            console.log(`${logPrefix}div element missing`);
            return;
        }


        const name = 'NameOld';
        const inputNameOld = selectedCellElement.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!inputNameOld)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell = textBoxElement;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell = inputNameOld;

        textBoxElement.setAttribute (
            'contenteditable',
            '' );

        selectedCellElement.classList.add('edit');
        selectedCellElement.classList.remove('select');



        Delaney.UI.ToolTip.Hide();

        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView ();


        // Select the text
        if (window.getSelection && document.createRange)
        {
            const range = document.createRange();
            range.selectNodeContents(textBoxElement);
            const sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        }
        else if (document.body.createTextRange)
        {
            const range = document.body.createTextRange();
            range.moveToElementText(textBoxElement);
            range.select();
        }

        // remove in click. This will be reinstated on cancel 
        // Note. Cancel_Click is also ran after successful save
        selectedCellElement.removeAttribute('onclick');

        textBoxElement.onkeydown = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.KeyPress;

        //window.addEventListener('click', SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.LostFocus_Click);

    },

    LostFocus_Click: function(event)
    {
        const name = event.target.getAttribute('name');

        if (name === 'EditButton' || name === 'SaveButton' || name === 'CancelButton' || name === 'NewButton' )
        {
            return;
        }


        if (event.target === SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell)
        {
            // Is edit zone
            return;
        }

        if (event.target === SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell.parentElement)
        {
            return;
        }

        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click();
    },

    // SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.KeyPress
    KeyPress: function (event)
    {
        if (event.which === 13)
        {
            event.preventDefault();
        }

        if (event.key === 'Enter')
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Save_Click();
        }

        if (event.key === 'Escape')
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click();
        }
    },

    // New item.
    // Look at the SelectedColumn to find out which column and hence which type of item should be created.
    // Cancel existing edit.
    New_Click: function ()
    {

      //  let name = 'NewButton';
        
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.New_Click: ';


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click();


        let columnElement = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedColumn;


        // Guard Clause
        if (!columnElement)
        {
            const elementId = 'Settings.PlatformSetup.BusinessUnits.Container';
            const element = document.getElementById(elementId);

            const columnName = 'BusinessUnit';
            columnElement = element.querySelector(`[name="${columnName}"]`);
        }

        let name = 'List';
        const listElement = columnElement.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!listElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = columnElement.getAttribute('name');
        if (name === 'Area' && !SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DepartmentId)
        {
            bootbox.dialog({
                title: `Select Business Unit`,
                message: 'Please select a business unit for your new area.',
                onEscape: true,
                backdrop: true,
                buttons: {
                    ok: {
                        label: 'OK',
                        className: 'btn-success bootbox-accept'
                    }
                }
            });
            return;
        }

        if (name === 'SubArea' && !SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.TeamId) {
            bootbox.dialog({
                title: `Select Business Area`,
                message: 'Please select a business area for your new sub-area.',
                onEscape: true,
                backdrop: true,
                buttons: {
                    ok: {
                        label: 'OK',
                        className: 'btn-success bootbox-accept'
                    }
                }
            });
            return;
        }


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DeSelectCells(listElement);


        let column = 'BusinessUnit';
        let newName = 'Business.Department';
        let targetName = 'Area';
        let nameValue = 'Business.Department.Name';
        let idValue = 'Business.Department.Id';
        let departmentIdValue = '';
        let teamIdValue = '';

        if (name === 'Area')
        {
            column = 'Area';
            newName = 'Business.Team';
            targetName = 'SubArea';
            nameValue = 'Business.Team.Name';
            idValue = 'Business.Team.Id';
            departmentIdValue = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DepartmentId;
        }
        else if (name === 'SubArea')
        {
            column = 'SubArea';
            newName = 'Business.Process';
            targetName = '';
            nameValue = 'Business.Process.Name';
            idValue = 'Business.Process.Id';
            teamIdValue = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.TeamId;
        }



        const div = document.createElement('div');
        div.setAttribute('url', '');
        div.setAttribute('column', column);
        div.setAttribute('name', newName);

        if (targetName)
        {
            div.setAttribute('targetName', targetName);
        }

        if (departmentIdValue)
        {
            const inputDepartmentId = document.createElement('input');
            inputDepartmentId.name = 'Business.Team.DepartmentId';
            inputDepartmentId.value = departmentIdValue;
            inputDepartmentId.type = 'hidden';
            div.appendChild(inputDepartmentId);
        }

        if (teamIdValue)
        {
            const inputTeamId = document.createElement('input');
            inputTeamId.name = 'Business.Process.TeamId';
            inputTeamId.value = teamIdValue;
            inputTeamId.type = 'hidden';
            div.appendChild(inputTeamId);
        }

        div.classList.add('select');
        div.addEventListener('click', SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Select_Click);
        div.addEventListener('dblclick', SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Dbl_Click);
        listElement.appendChild(div);


        const divTextBox = document.createElement('div');
        divTextBox.setAttribute('name', nameValue);
        divTextBox.setAttribute('role', 'textbox');
        div.appendChild(divTextBox);


        const inputOldName = document.createElement('input');
        inputOldName.name =  'NameOld';
        inputOldName.type = 'hidden';
        div.appendChild(inputOldName);

        const inputId = document.createElement('input');
        inputId.name = idValue;
        inputId.type = 'hidden';
        div.appendChild(inputId);
        divTextBox.focus();
        divTextBox.scrollIntoView({ behavior: "smooth" });
        divTextBox.scrollTop = 0;
        //window.removeEventListener('click', SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.LostFocus_Click);


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell = div;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Edit_Click();
    },

    Cancel_Click: function ()
    {
       
        //window.removeEventListener('click', SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.LostFocus_Click);

        // Guard Clause
        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell)
        {
            return;
        }


        const selectedCell = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell;

        if (selectedCell.classList.contains('select'))
        {
            return;
        }

        const id = selectedCell.id;

        if (!id)
        {
            selectedCell.remove();

            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell = null;
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell = null;
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell = null;
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView();
            const element = document.querySelector("[name='NewButton']");
           
            if (element) {
                // Replace 'classToRemove' with the actual class name you want to remove
                element.classList.remove('hide');
            } else {
                console.error("Element not found.");
            }
            return;
        }


        selectedCell.classList.add('select');
        selectedCell.classList.remove('edit');
        selectedCell.addEventListener('click', SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Select_Click);



        // Guard Clause
        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell) {
            return;
        }

        // Guard Clause
        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click: ';
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell missing`);
            return;
        }



        const div = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell;
        const old = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell;

        const text = old.value;
        div.innerHTML = text;
        div.removeAttribute('contenteditable');

        // Deselect
        if (window.getSelection)
        {
            window.getSelection().removeAllRanges();
        }
        else if (document.selection)
        {
            document.selection.empty();
        }


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell = null;

        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView();

        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SetMessage('Cancelled', 'text-warning');
       
    },

    Save_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Save_Click: ';


        // Guard Clause
        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell)
        {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell missing`);
            return;
        }


        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell;



        const name = parent.getAttribute('name');

        if (!name)
        {
            console.log(logPrefix + 'name is missing');
            return;
        }


        let url = '';
        let model = null;
        if (name === 'Business.Department')
        {
            url = '/api/Business/Department/Post';
            model = SilkFlo.Models.Abstract.GetModelFromParent (
                parent,
                [
                    'Id',
                    'Name'
                ],
                'Business.Department.');
        }
        else if (name === 'Business.Team')
        {
            url = '/api/Business/Team/Post';
            model = SilkFlo.Models.Abstract.GetModelFromParent(
                parent,
                [
                    'Id',
                    'Name',
                    'DepartmentId'
                ],
                'Business.Team.');
        }
        else if (name === 'Business.Process')
        {
            url = '/api/Business/Process/Post';
            model = SilkFlo.Models.Abstract.GetModelFromParent(
                parent,
                [
                    'Id',
                    'Name',
                    'TeamId'
                ],
                'Business.Process.');
        }


        if (!model)
        {
            console.log(logPrefix + 'Model is missing');
            return;
        }


        if (!model.Name)
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SetMessage('Please provide a name.', 'text-warning');

            return;
        }


        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Save_CallBack,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.BusinessUnits.Container',
            url,
            'POST');
    },

    Save_CallBack: function (str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Save_CallBack: ';


        let isNew = false;
        if (str)
        {
            isNew = true;
        }




        // Guard Clause
        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell) {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell missing`);
            return;
        }


        // Guard Clause
        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell)
        {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell missing`);
            return;
        }


        // Guard Clause
        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell)
        {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell missing`);
            return;
        }


        const selectedElement = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell;
        const textBoxElement = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell;
        const oldNameElement = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell;
        oldNameElement.value = textBoxElement.innerHTML;


        const name = selectedElement.getAttribute('name');


        if (isNew)
        {
            let url = '';
            if (name === 'Business.Department')
            {
                url = `GetAreas/departmentId/${str}`;
            }
            else if (name === 'Business.Team')
            {
                url = `GetSubAreas/teamId/${str}`;
            }


            if (url)
            {
                selectedElement.setAttribute('url', url);
            }

            selectedElement.id = str;
        }


        let text = 'Saved Department';
        let idName = 'Business.Department.Id';
        if (name === 'Business.Team')
        {
            text = 'Saved Area';
            idName = 'Business.Team.Id';
        }
        else if (name === 'Business.Process')
        {
            text = 'Saved Sub-Area';
            idName = 'Business.Process.Id';
        }


        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Cancel_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SetMessage(text, 'text-success');



        if (isNew)
        {
            const idProperty = selectedElement.querySelector(`[name="${idName}"]`);
            if (!idProperty) {
                console.log(`${logPrefix}Element with name ${idName} missing`);
                return;
            }

            idProperty.value = str;

            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Select(selectedElement);
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView();
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.GetList(selectedElement);
        }
    },

    Delete_Click: function ()
    {
        
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Delete_Click: ';

        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell)
        {
            console.log(logPrefix + 'SelectedCell missing');
            return;
        }

        if (!SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell.id) {
            console.log(logPrefix + 'SelectedCell.id missing');
            return;
        }

        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell;

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell missing`);
            return;
        }


        let name = parent.getAttribute('name');

        if (!name) {
            console.log(logPrefix + 'name is missing');
            return;
        }


        name = name.replace('.', '/');
        const url = `/api/Models/${name}/Delete/Id/${SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell.id}`;

        name = name.replace('/', ' ');
        name = name.replace('Department', 'Unit');
        name = name.replace('Team', 'Area');
        name = name.replace('Process', 'Sub-Area');


        SilkFlo.Models.Abstract.Delete
        (
            url,
            name,
            '',
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Delete_CallBack,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.BusinessUnits.Container'
        );
    },

    Delete_CallBack: function()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Delete_CallBack: ';

        const element = SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell;

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell missing`);
            return;
        }

        let name = element.getAttribute('name');


        // Guard Clause
        if (!name) {
            console.log(logPrefix + 'name is missing');
            return;
        }


        console.log(logPrefix + name);

        const id = 'Settings.PlatformSetup.BusinessUnits.Container';
        const parent = document.getElementById(id);

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        if (name === 'Business.Department')
        {
            let name = 'Area';
            const columnElement = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!columnElement) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            name = 'List';
            const listElement = columnElement.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!listElement) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            listElement.innerHTML = '';
        }
        else if (name === 'Business.Team')
        {
            let name = 'SubArea';
            const columnElement = parent.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!columnElement) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            name = 'List';
            const listElement = columnElement.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!listElement) {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            listElement.innerHTML = '';
        }



        element.remove();
    },

    Search: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.Search: ';

        let elementId = 'Settings.PlatformSetup.BusinessUnits.Search.Id';
        const element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return;
        }


        elementId = 'Settings.PlatformSetup.BusinessUnits.Container';
        const containerElement = document.getElementById(elementId);


        let name = 'BusinessUnit';
        const businessUnitElement = containerElement.querySelector (`[name="${name}"]`);


        name = 'List';
        const listElement = businessUnitElement.querySelector(`[name="${name}"]`);
        // Guard Clause
        if (!listElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        const text = element.value;
        console.log(logPrefix + text);

        const url = `/api/Business/Department/GetForBusinessUnits/SearchText/${text}`;

        SilkFlo.DataAccess.UpdateElement (
            url,
            null,
            listElement,
            '',
            'GET',
            SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SearchResultCallBack);
    },

    SearchResultCallBack: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SearchResultCallBack: ';

        const elementId = 'Settings.PlatformSetup.BusinessUnits.Container';
        const parent = document.getElementById(elementId);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return;
        }


        let name = 'Area';
        const elementArea = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementArea)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = 'List';
        const elementAreaList = elementArea.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementAreaList)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = 'Header';
        const elementAreaHeader = elementArea.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementAreaHeader)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'SubArea';
        const elementSubArea = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementSubArea)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'List';
        const elementSubAreaList = elementSubArea.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementSubAreaList)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = 'Header';
        const elementSubAreaHeader = elementSubArea.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementSubAreaHeader)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        elementAreaList.innerHTML = '';
        elementAreaList.classList.remove ('select');
        elementAreaHeader.classList.remove('select');
        elementSubAreaList.innerHTML = '';
        elementSubAreaList.classList.remove ('select');
        elementSubAreaHeader.classList.remove('select');
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedColumn = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedHeader = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.SelectedCell = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DivCell = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.OldNameCell = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.DepartmentId = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.TeamId = null;

        SilkFlo.ViewModels.Settings.PlatformSetup.BusinessUnits.UpdateView();


        name = 'NewButton';
        const elementNewButton = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementNewButton)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        elementNewButton.classList.remove('hide');
    }
};