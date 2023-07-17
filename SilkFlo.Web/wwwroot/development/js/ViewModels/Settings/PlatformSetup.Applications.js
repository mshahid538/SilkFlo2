if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};


if (!SilkFlo.ViewModels.Settings.PlatformSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup = {};



// Applications Namespace: Code to manage the content inside the Applications tab
SilkFlo.ViewModels.Settings.PlatformSetup.Applications = {

    IsNew: null,
    SelectedRow: null,
    EditRow: null,
    IsLoading: false,

    NewToolTip: null,
    NewButtonElement: null,

    EditToolTip: null,
    EditButtonElement: null,


    // SilkFlo.ViewModels.Settings.PlatformSetup.Applications.GetParent
    GetParent: function ()
    {
        const id = 'Settings.PlatformSetup.Applications.Container';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },

    // SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SetMessage
    SetMessage: function (text, cls)
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.Applications.GetParent();
        if (!parent)
            return;


        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.setAttribute('class', cls);
        element.innerHTML = text;
    },

    UpdateNewButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.UpdateNewButtonToolTip: ';

        if (!text)
        {
            return;
        }


        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;



        const elementName = 'NewButton';
        const newButton = parent.querySelector(`[name="${elementName}"]`);

        // Guard Clause
        if (!newButton)
        {
            console.log(`${logPrefix}Element with name ${elementName} missing`);
            return;
        }


        this.NewToolTip = text;
        this.NewButtonElement = newButton;

        newButton.onmousemove = this.ShowNewToolTip;
        newButton.onmouseout = Delaney.UI.ToolTip.Hide;
    },

    UpdateEditButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.UpdateEditButtonToolTip: ';

        if (!text)
            return;


        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;
        

        const elementName = 'EditButton';
        const editButton = parent.querySelector(`[name="${elementName}"]`);

        // Guard Clause
        if (!editButton)
        {
            console.log(`${logPrefix}Element with name ${elementName} missing`);
            return;
        }


        this.EditToolTip = text;
        this.EditButtonElement = editButton;

        editButton.onmousemove = this.ShowEditToolTip;
        editButton.onmouseout = Delaney.UI.ToolTip.Hide;
    },

    ShowEditToolTip: function ()
    {
        const toolTip = this.EditToolTip;
        const element = this.EditButtonElement;
        Delaney.UI.ToolTip.Show(event, toolTip, element);
    },

    ShowNewToolTip: function ()
    {
        const toolTip = this.NewToolTip;
        const element = this.NewButtonElement;
        Delaney.UI.ToolTip.Show(event, toolTip, element);
    },


    UpdateView: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.UpdateView: ';

        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;



        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['NewButton', 'EditButton', 'DeleteButton', 'SaveButton', 'CancelButton'],
            '');


        // Guard Clause
        if (!parent.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }


        // Get the elements
        const elements = parent.ModelElements;


        // Do the business

        // Is Loading
        if (this.IsLoading)
        {
            elements.NewButton.classList.add('hide');
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.add('hide');    // Hide Cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
            return;
        }


        // Show/Hide New
        if (this.IsNew
            || this.EditRow)
            elements.NewButton.classList.add('hide');
        else
            elements.NewButton.classList.remove('hide');



        if (this.SelectedRow)
        {

            const row = this.SelectedRow;
            let name = 'Business.Version.IdeaApplicationVersions.Count';
            const processCountElement = row.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!processCountElement)
            {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }


            let value = processCountElement.innerHTML;
            value = value.trim();

            if (value === '0'
                && !this.IsNew)
                elements.DeleteButton.classList.remove('hide'); // Show Delete
            else
                elements.DeleteButton.classList.add('hide');    // Hide Delete


            elements.EditButton.classList.remove('hide');   // Show Edit
            elements.CancelButton.classList.add('hide');    // Hide Cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
        }
        else if (this.EditRow)
        {
            // Is edit
            elements.EditButton.classList.add('hide');      // Hide edit
            elements.CancelButton.classList.remove('hide'); // Show cancel
            elements.SaveButton.classList.remove('hide');   // Show save
            elements.DeleteButton.classList.add('hide');    // Hide delete
        }
        else
        {
            // nothing selected
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.add('hide');    // Hide cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
        }
    },


    // New item.
    // Cancel existing edit.
    //SilkFlo.ViewModels.Settings.PlatformSetup.Applications.New_Click
    New_Click: function ()
    {
        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;


        this.Cancel_Click();

        this.IsLoading = true;
        this.UpdateView();

        const url = '/api/settings/Tenant/platformSetup/Applications/NewRow';
        const tbody = parent.querySelector('tbody');
        SilkFlo.DataAccess.AppendElement(
            url,
            null,
            tbody,
            this.New_CallBack,
            true);
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.Applications.New_CallBack
    New_CallBack: function ()
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.Applications.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const tbody = parent.querySelector('tbody');

        const row = tbody.children[0];
        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.IsLoading = false;

        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SelectRow_Click(row);
        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Edit_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.IsNew = true;
        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.UpdateView();
    },



    Cancel_Click: function ()
    {
        // Guard Clause
        if (!this.EditRow)
            return;

        if (this.EditRow.isEqualNode(this.SelectedRow))
            return;


        const row = this.EditRow;
        this.EditRow = null;
        this.SelectedRow = row;


        if (this.IsNew)
        {
            row.remove();
            this.SelectedRow = null;
            this.IsNew = false;
            this.UpdateView();
            return;
        }


        row.classList.remove('edit');


        let name = 'Business.Application.Name';
        const applicationNameElement = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!applicationNameElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        if (!applicationNameElement.hasAttribute('old'))
        {
            console.log(`${logPrefix}Old attribute missing`);
            return;
        }




        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Name',
                'PlannedUpdateDateDisplay',
                'PlannedUpdateDate',
                'PlannedUpdateDateDisplayDatePicker',
                'PlannedUpdateDateDisplayDatePickerContainer',
                'IsLive'],
            'Business.Version.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        let old = applicationNameElement.getAttribute('old');
        applicationNameElement.innerHTML = old;
        applicationNameElement.removeAttribute('contenteditable');

        old = elements.Name.getAttribute('old');
        elements.Name.innerHTML = old;
        elements.Name.removeAttribute('contenteditable');

        elements.IsLive.setAttribute('disabled', '');

        old = elements.IsLive.getAttribute('old');
        if (old === 'True')
            elements.IsLive.checked = true;
        else
            elements.IsLive.checked = false;

        elements.IsLive.value = old;


        elements.PlannedUpdateDate.value = elements.PlannedUpdateDateDisplay.innerHTML;

        elements.PlannedUpdateDateDisplay.style.display = 'block';
        elements.PlannedUpdateDateDisplayDatePickerContainer.style.display = 'none';



        // Deselect
        if (window.getSelection)
        {
            window.getSelection().removeAllRanges();
        }
        else if (document.selection)
        {
            document.selection.empty();
        }



        this.UpdateView();
    },


    SelectRow_Click: function(element)
    {
        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SelectRow_Click: ';
            console.log(`${logPrefix}element parameter missing`);
            return;
        }


        if (this.EditRow
            && element.isEqualNode(this.EditRow))
            return;

        this.SelectedRow = element;

        this.Cancel_Click();


        const parent = element.parentElement;


        const children = parent.children;

        const length = children.length;
        for (let i = 0; i < length; i++)
        {
            const child = children[i];
            child.classList.remove('select');
        }

        element.classList.add('select');

        this.UpdateView();
    },

    Edit_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Edit_Click: ';


        this.Cancel_Click();

        const row = this.SelectedRow;
        this.EditRow = row;
        this.SelectedRow = null;

        // Guard Clause
        if (!row)
        {
            console.log(`${logPrefix}SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SelectedRow missing`);
            return;
        }




        // Business.Application.Name
        const name = 'Business.Application.Name';
        const applicationNameElement = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!applicationNameElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Name',
                'PlannedUpdateDateDisplay',
                'PlannedUpdateDateDisplayDatePicker',
                'PlannedUpdateDateDisplayDatePickerContainer',
                'PlannedUpdateDate',
                'IsLive',
                'PlannedUpdateDateButton'
            ],
            'Business.Version.');

        const elements = row.ModelElements;


        elements.IsLive.removeAttribute('disabled');

        row.classList.add('edit');
        this.IsNew = false;


        Delaney.UI.DatePicker.Main(elements.PlannedUpdateDate, elements.PlannedUpdateDateButton );


        applicationNameElement.setAttribute(
            'contenteditable',
            '');


        elements.Name.setAttribute(
            'contenteditable',
            '');



        elements.PlannedUpdateDateDisplay.style.display = 'none';


        elements.PlannedUpdateDateDisplayDatePickerContainer.style.display = '';


        Delaney.UI.ToolTip.Hide();

        this.UpdateView();


        // Select the text
        if (window.getSelection && document.createRange)
        {
            const range = document.createRange();
            range.selectNodeContents(applicationNameElement);
            const sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        }
        else if (document.body.createTextRange)
        {
            const range = document.body.createTextRange();
            range.moveToElementText(applicationNameElement);
            range.select();
        }


        applicationNameElement.onkeydown = this.KeyPress;
        elements.Name.onkeydown = this.KeyPress;
        elements.IsLive.onkeydown = this.KeyPress;
        elements.PlannedUpdateDate.onkeydown = this.KeyPress;

        Delaney.UI.ToolTip.Hide();

        this.UpdateView();
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.Applications.KeyPress
    KeyPress: function (event)
    {
        if (event.which === 13)
            event.preventDefault();

        if (event.key === 'Enter')
            SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Save_Click();

        if (event.key === 'Escape')
            SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Cancel_Click();
    },

    Dbl_Click: function (element)
    {
        this.SelectRow_Click(element);
        this.Edit_Click();
    },


    Save_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Save_Click: ';


        if (!this.EditRow)
        {
            console.log(`${logPrefix}this.SelectedRow parameter missing`);
            return;
        }

        const row = this.EditRow;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name',
                'IsLive',
                'PlannedUpdateDate'
            ],
            'Business.Version.');

        const modelApplication = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name'
            ],
            'Business.Application.');

        model.Application = modelApplication;



        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'Model is missing');
            return;
        }



        // Guard Clause
        if (!model.Application)
        {
            console.log(logPrefix + 'Model.Application is missing');
            return;
        }



        // Guard Clause
        if (!model.Application.Name)
        {
            this.SetMessage('Please provide an application name', 'text-warning');
            return;
        }


        // Guard Clause
        if (!model.Name)
        {
            this.SetMessage('Please provide a version', 'text-warning');
            return;
        }


        const url = '/api/Business/Version/Post';
        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Save_CallBack,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.Applications.Container',
            url,
            'POST');
    },


    Save_CallBack: function (
        str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Save_CallBack: ';

        // Guard Clause
        if (!str)
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Cancel_Click();
            return;
        }

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.Applications.EditRow;

        // Guard Clause
        if (!row)
        {
            console.log(`${logPrefix}this.SelectedRow missing`);
            return;
        }


        const o = JSON.parse(str);

        // Guard Clause
        if (!o)
        {
            console.log(`${logPrefix}Falied to parse str`);
            return;
        }




        let name = 'Business.Version.Name';
        const elementVersionName = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementVersionName)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        let n = elementVersionName.innerHTML.trim();
        elementVersionName.setAttribute('old', n);


        name = 'Business.Application.Name';
        const elementApplicationName = row.querySelector(`[name="${name}"]`);


        // Guard Clause
        if (!elementApplicationName)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }



        name = 'Business.Version.PlannedUpdateDateDisplay';
        let dateDisplayElement = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!dateDisplayElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'Business.Version.PlannedUpdateDate';
        let dateElement = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!dateElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        name = 'Business.Version.IsLive';
        const elementIsLive = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!elementIsLive)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        if (elementIsLive.checked)
        {
            elementIsLive.setAttribute(
                'old',
                'True');
        }
        else
        {
            elementIsLive.setAttribute(
                'old',
                'False');
        }


        dateDisplayElement.innerHTML = dateElement.value;


        if (SilkFlo.ViewModels.Settings.PlatformSetup.Applications.IsNew)
        {
            if (o.id)
            {
                name = 'Business.Version.Id';
                const element = row.querySelector(`[name="${name}"]`);

                // Guard Clause
                if (!element)
                {
                    console.log(`${logPrefix}Element with name ${name} missing`);
                    return;
                }

                element.innerHTML = o.id;
            }

            if (o.applicationId)
            {
                name = 'Business.Application.Id';
                const element = row.querySelector(`[name="${name}"]`);

                // Guard Clause
                if (!element)
                {
                    console.log(`${logPrefix}Element with name ${name} missing`);
                    return;
                }

                element.innerHTML = o.applicationId;
            }


            n = elementApplicationName.innerHTML.trim();
            elementApplicationName.setAttribute('old', n);


        } else
        {
            if (o.applicationName)
            {

                const id = 'Settings.PlatformSetup.Applications';
                const parent = document.getElementById(id);

                // Guard Clause
                if (!parent)
                {
                    console.log(`${logPrefix}Element with id ${id} missing`);
                    return;
                }


                const oldSource = elementApplicationName.getAttribute('old');

                // Guard Clause
                if (!oldSource)
                {
                    console.log(`${logPrefix}old attribute missing`);
                    return;
                }


                const nameElements = parent.querySelectorAll('[name="Business.Application.Name"]');
                const length = nameElements.length;

                for (var i = 0; i < length; i++)
                {
                    const nameElement = nameElements[i];
                    const oldTarget = nameElement.getAttribute('old');

                    if (oldTarget === oldSource)
                    {
                        nameElement.innerHTML = o.applicationName;
                        nameElement.setAttribute('old', o.applicationName);
                        nameElement.parentElement.classList.add('pulse-background-green');

                        setTimeout(function ()
                        {
                            nameElement.parentElement.classList.remove('pulse-background-green');
                        }, 2000);
                    }
                }


                n = elementApplicationName.innerHTML.trim();
                elementApplicationName.setAttribute('old', n);

            }

        }


        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.IsNew = false;
        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Cancel_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SetMessage(
            'Saved',
            'text-success');
    },




    Delete_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Delete_Click: ';

        const row = this.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SelectedRow missing');
            return;
        }

        

        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name'
            ],
            'Business.Version.');

        const modelApplication = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Name'
            ],
            'Business.Application.');


        if (!model || !model.Id)
        {
            console.log(logPrefix + 'Cannot find version Id');
            return;
        }

        if (!model.Name)
        {
            console.log(logPrefix + 'Cannot find version');
            return;
        }

        if (!modelApplication || !modelApplication.Name)
        {
            console.log(logPrefix + 'Cannot find application');
            return;
        }

        const url = `/api/Business/Version/Delete/${model.Id}`;


        SilkFlo.Models.Abstract.Delete(
            url,
            `${modelApplication.Name} ${model.Name}`,
            '',
            this.Delete_Callback,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.Applications.Container');
    },


    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Delete_Callback: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'this.SelectedRow missing');
            return;
        }


        row.classList.remove('select');
        row.classList.add('pulse-background-red');

        setTimeout(function ()
        {
            row.classList.remove('pulse-background-red');
            row.remove();
        }, 2000);

        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SelectedRow = null;

        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.UpdateView();
    },



    Search: function (
        searchText,
        page,
        targetElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.Applications.Search: ';


        // Guard Clause
        if (!targetElementId)
        {
            console.log(`${logPrefix}targetElementId parameter missing`);
            return;
        }


        const targetElement = document.getElementById(targetElementId);

        // Guard Clause
        if (!targetElement)
        {
            console.log(`${logPrefix}Element with id ${targetElementId} missing`);
            return;
        }

        if (!searchText)
        {
            const id = 'Settings.PlatformSetup.Applications.Search.Id';
            const element = document.getElementById(id);


            // Guard Clause
            if (!element)
            {
                console.log(`${logPrefix}Element with id ${id} missing`);
                return;
            }

            searchText = element.value;
        }



        let isPage = true;
        if ((page === undefined
            || page === null
            || page === '')
            || page === '1')
            isPage = false;


        let isSearchText = true;
        if (!searchText)
            isSearchText = false;

        let url = '/api/settings/Tenant/platformSetup/Applications';
        if (isSearchText)
            url += '/Search/' + searchText;

        if (isPage)
            url += '/page/' + page;

        this.SelectedRow = null;

        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            targetElement,
            '',
            'GET',
            this.SearchResultCallBack);
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.Applications.SearchResultCallBack
    SearchResultCallBack: function ()
    {
        SilkFlo.ViewModels.Settings.PlatformSetup.Applications.UpdateView();
    },

    LostFocus_Click: function(event)
    {
        const target = event.target;


        const name = target.getAttribute('name');

        if (name === 'EditButton'
            || name === 'SaveButton'
            || name === 'CancelButton'
            || name === 'NewButton')
            return;


        const row = this.SelectedRow;

        if (!row)
            return;


        if (row.contains(target))
            return;


        this.Cancel_Click();
    }
};