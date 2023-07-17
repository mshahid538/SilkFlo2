if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup = {};


// CostSetup.SoftwareVender Namespace: Code to manage the content inside the CostSetup > SoftwareVender tab
SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender = {

    IsNew: null,
    SelectedRow: null,
    EditRow: null,
    IsLoading: false,

    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.GetParent
    GetParent: function ()
    {
        const id = 'Settings.PlatformSetup.PlatformSetup.CostSetup.SoftwareVendor.Container';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },

    SetMessage: function (text, cls)
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.GetParent();
        if (!parent)
            return;

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element) {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.setAttribute('class', cls);
        element.innerHTML = text;
    },

    UpdateNewButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.UpdateNewButtonToolTip: ';

        if (!text)
            return;


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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.UpdateEditButtonToolTip: ';

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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.UpdateView: ';

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

        if (this.IsLoading)
        {
            elements.NewButton.classList.add('hide');
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.add('hide');    // Hide Cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
            return;
        }

        // Do the business
        if (this.IsNew
            || this.EditRow)
            elements.NewButton.classList.add('hide');
        else
            elements.NewButton.classList.remove('hide');


        if (this.SelectedRow)
        {
            // Is selected
            elements.EditButton.classList.remove('hide');    // Show Edit
            elements.CancelButton.classList.add('hide');     // Hide Cancel
            elements.SaveButton.classList.add('hide');       // Hide Save
            elements.DeleteButton.classList.remove('hide');  // Show Delete
        }
        else if (this.EditRow)
        {
            // Is edit
            elements.EditButton.classList.add('hide');      // Hide edit
            elements.CancelButton.classList.remove('hide'); // Show cancel
            elements.SaveButton.classList.remove('hide');   // Show save
            elements.DeleteButton.classList.add('hide');    // Hide delete
        } else
        {
            // nothing selected
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.add('hide');    // Hide cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
        }
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.New_Click
    New_Click: function ()
    {
        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;

        this.Cancel_Click();

        this.IsLoading = true;
        this.UpdateView();

        const url = '/api/settings/Tenant/platformSetup/CostSetup/SoftwareVender/NewRow';
        const tbody = parent.querySelector('tbody');
        SilkFlo.DataAccess.AppendElement(
            url,
            null,
            tbody,
            this.New_CallBack,
            true);
    },

    New_CallBack: function ()
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const tbody = parent.querySelector('tbody');

        const row = tbody.children[0];
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.IsLoading = false;


        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SelectRow_Click(row);
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Edit_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.IsNew = true;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.UpdateView();
    },


    Cancel_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Cancel_Click: ';


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



        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            ['Name', 'IsLive'],
            'Business.SoftwareVender.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;



        // Do the business
        let old = elements.Name.getAttribute('old');
        elements.Name.innerHTML = old;
        elements.Name.removeAttribute('contenteditable');
        elements.IsLive.setAttribute('disabled', '');

        old = elements.IsLive.getAttribute('old');
        if (old === 'True')
        {
            elements.IsLive.checked = true;
        }
        else
        {
            elements.IsLive.checked = false;
        }

        elements.IsLive.value = old;


        // Deselect
        if (window.getSelection)
            window.getSelection().removeAllRanges();

        else if (document.selection)
            document.selection.empty();


        this.UpdateView();
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SelectRow_Click
    SelectRow_Click: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SelectRow_Click: ';
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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Edit_Click: ';


        this.Cancel_Click();

        const row = this.SelectedRow;
        this.EditRow = row;
        this.SelectedRow = null;

        // Guard Clause
        if (!row)
        {
            console.log(`${logPrefix}this.SelectedRow missing`);
            return;
        }



        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            ['Name', 'IsLive'],
            'Business.SoftwareVender.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        row.classList.add('edit');
        this.IsNew = false;


        elements.Name.setAttribute(
            'contenteditable',
            '');


        elements.IsLive.removeAttribute('disabled');


        // Select the text
        if (window.getSelection && document.createRange)
        {
            const range = document.createRange();
            range.selectNodeContents(elements.Name);
            const sel = window.getSelection();
            sel.removeAllRanges();
            sel.addRange(range);
        }
        else if (document.body.createTextRange)
        {
            const range = document.body.createTextRange();
            range.moveToElementText(elements.Name);
            range.select();
        }



        elements.Name.onkeydown = this.KeyPress;
        elements.IsLive.onkeydown = this.KeyPress;


        Delaney.UI.ToolTip.Hide();

        this.UpdateView();
    },

    Dbl_Click: function (element)
    {
        this.SelectRow_Click(element);
        this.Edit_Click();
    },





    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.KeyPress
    KeyPress: function (event)
    {
        if (event.which === 13)
        {
            event.preventDefault();
        }

        if (event.key === 'Enter')
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Save_Click();
        }

        if (event.key === 'Escape')
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Cancel_Click();
        }
    },






    Save_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Save_Click: ';


        if (!this.EditRow)
        {
            console.log(`${logPrefix}this.EditRow parameter missing`);
            return;
        }

        const row = this.EditRow;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name',
                'IsLive'
            ],
            'Business.SoftwareVender.');


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'Model is missing');
            return;
        }



        // Guard Clause
        if (!model.Name)
        {
            this.SetMessage('Please provide a software vender name', 'text-warning');
            return;
        }



        const url = '/api/Business/SoftwareVender/Post';
        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Save_CallBack,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.SoftwareVendor.Container',
            url,
            'POST');
    },


    Save_CallBack: function (
                       str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Save_CallBack: ';

        // Guard Clause
        if (!str)
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Cancel_Click();
            return;
        }


        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.EditRow;

        // Guard Clause
        if (!row)
        {
            console.log(`${logPrefix}this.EditRow missing`);
            return;
        }





        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            ['Id', 'Name', 'IsLive'],
            'Business.SoftwareVender.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;




        const n = elements.Name.innerHTML.trim();
        elements.Name.setAttribute('old', n);


        if (elements.IsLive.checked)
            elements.IsLive.setAttribute (
                'old',
                'True' );
        else
            elements.IsLive.setAttribute(
                'old',
                'False');


        if (SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.IsNew)
        {
            // Guard Clause
            if (!elements.Id)
            {
                console.log(`${logPrefix}Element.Id missing`);
                return;
            }

            elements.Id.innerHTML = str;
        }


        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.IsNew = false;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Cancel_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SetMessage (
            'Saved',
            'text-success');
    },



    Delete_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Delete_Click: ';

        const row = this.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SelectedRow missing');
            return;
        }



        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name'
            ],
            'Business.SoftwareVender.');


        if (!model || !model.Id)
        {
            console.log(logPrefix + 'Cannot find software vender Id');
            return;
        }


        if (!model.Name)
        {
            console.log(logPrefix + 'Cannot find software vender');
            return;
        }



        const url = `/api/Business/SoftwareVender/Delete/${model.Id}`;


        SilkFlo.Models.Abstract.Delete(
            url,
            `${model.Name}`,
            '',
            this.Delete_Callback,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.SoftwareVendor.Container');
    },


    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Delete_Callback: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'this.SelectedRow missing');
            return;
        }


        row.classList.remove('select');
        row.classList.add('glow-border-red');

        setTimeout(function ()
        {
            row.classList.remove('glow-border-red');
            row.remove();
        }, 1000);

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.SelectedRow = null;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.UpdateView();
    },




    Search: function (
                searchText,
                page,
                targetElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.SoftwareVender.Search: ';


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
            const id = 'Settings.PlatformSetup.CostSetup.SoftwareVender.Search';
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
        {
            isPage = false;
        }

        let isSearchText = true;
        if (!searchText)
        {
            isSearchText = false;
        }

        let url = '/api/settings/Tenant/platformSetup/CostSetup/SoftwareVenders';
        if (isSearchText)
        {
            url += `/Search/${searchText}`;
        }

        if (isPage)
        {
            url += `/page/${page}`;
        }

        this.SelectedRow = null;

        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            targetElement,
            '',
            'GET',
            this.SearchResultCallBack);
    }
};