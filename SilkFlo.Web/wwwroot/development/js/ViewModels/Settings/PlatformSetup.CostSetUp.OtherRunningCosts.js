if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup = {};

// CostSetup.OtherRunningCosts Namespace: Code to manage the content inside the CostSetup > OtherRunningCosts tab
SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts = {

    IsNew: null,
    SelectedRow: null,
    EditRow: null,
    IsLoading: false,


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.GetParent
    GetParent: function ()
    {
        const id = 'Settings.PlatformSetup.PlatformSetup.CostSetup.OtherRunningCosts.Container';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SetMessage
    SetMessage: function (
        text,
        cls = '')
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.GetParent();

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.setAttribute('class', cls);
        element.innerHTML = text;
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.UpdateNewButtonToolTip
    UpdateNewButtonToolTip: function (text)
    {
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
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.UpdateNewButtonToolTip: ';
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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.UpdateEditButtonToolTip: ';

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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.UpdateView: ';

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
            elements.NewButton.classList.add('hide');       // Hide New
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

    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.New_Click
    New_Click: function ()
    {
        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;

        this.Cancel_Click();

        this.IsLoading = true;
        this.UpdateView();
        const url = '/api/settings/Tenant/platformSetup/CostSetup/OtherRunningCosts/NewRow';
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
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const tbody = parent.querySelector('tbody');

        const row = tbody.children[0];
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.IsLoading = false;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SelectRow_Click(row);
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Edit_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.IsNew = true;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.UpdateView();
    },

    Cancel_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Cancel_Click: ';


        // Guard Clause
        if (!this.EditRow)
            return;

        if (this.EditRow.isEqualNode ( this.SelectedRow ))
            return;


        const row = this.EditRow;
        this.EditRow = null;
        this.SelectedRow = row;


        if (this.IsNew)
        {
            row.remove ();
            this.SelectedRow = null;
            this.IsNew = false;
            this.UpdateView ();
            return;
        }


        row.classList.remove('edit');


        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'NameDisplay',
                'Name',
                'CostTypeName',
                'CostTypeId',
                'DescriptionDisplay',
                'Description',
                'FrequencyName',
                'FrequencyId',
                'CostDisplay',
                'Cost',
                'AnnualCostDisplay',
                'AnnualCost',
                'IsLive'
            ],
            'Business.OtherRunningCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;

        let old = elements.NameDisplay.getAttribute('old');
        elements.Name.value = old;

        let oldId = elements.CostTypeName.getAttribute('oldId');
        elements.CostTypeId.value = oldId;

        old = elements.DescriptionDisplay.getAttribute('old');
        elements.Description.value = old;

        oldId = elements.FrequencyName.getAttribute('oldId');
        elements.FrequencyId.value = oldId;

        old = elements.CostDisplay.getAttribute('old');
        elements.Cost.value = old;

        old = elements.AnnualCostDisplay.getAttribute('old');
        elements.AnnualCost.value = old;


        elements.IsLive.setAttribute('disabled', '');

        old = elements.IsLive.getAttribute('old');
        if (old === 'True')
            elements.IsLive.checked = true;
        else
            elements.IsLive.checked = false;

        elements.IsLive.value = old;

        this.UpdateView();
    },


    SelectRow_Click: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SelectRow_Click: ';
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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Edit_Click: ';


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
            [
                'Name', 'Description', 'Cost', 'AnnualCost', 'IsLive'
            ],
            'Business.OtherRunningCost.');

        const elements = row.ModelElements;


        elements.IsLive.removeAttribute('disabled');

        row.classList.add('edit');
        this.IsNew = false;

        elements.Name.onkeydown = this.KeyPress;
        elements.Description.onkeydown = this.KeyPress;

        Delaney.UI.ToolTip.Hide();

        this.UpdateView();
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.KeyPress
    KeyPress: function (event)
    {
        if (event.which === 13)
            event.preventDefault();

        if (event.key === 'Enter')
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Save_Click();

        if (event.key === 'Escape')
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Cancel_Click();
    },

    Dbl_Click: function (element)
    {
        this.SelectRow_Click(element);
        this.Edit_Click();
    },

    Save_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Save_Click: ';

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
                'CostTypeId',
                'Description',
                'FrequencyId',
                'Cost',
                'AnnualCost',
                'IsLive'
            ],
            'Business.OtherRunningCost.');


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'Model is missing');
            return;
        }


        // Guard Clause
        if (!model.Name)
        {
            this.SetMessage('The cost name is missing', 'text-warning');
            return;
        }

        // Guard Clause
        if (!model.Description)
        {
            this.SetMessage('The description is missing', 'text-warning');
            return;
        }



        const url = '/api/Business/otherRunningCost/Post';
        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Save_CallBack,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.OtherRunningCosts.Container',
            url,
            'POST');
    },


    Save_CallBack: function (
        str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Save_CallBack: ';

        // Guard Clause
        if (!str)
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Cancel_Click();
            return;
        }


        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.EditRow;

        // Guard Clause
        if (!row)
        {
            console.log(`${logPrefix}this.EditRow missing`);
            return;
        }





        // Get the elements
        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'NameDisplay',
                'Name',
                'CostTypeName',
                'CostTypeId',
                'DescriptionDisplay',
                'Description',
                'FrequencyName',
                'FrequencyId',
                'Cost',
                'CostDisplay',
                'AnnualCost',
                'AnnualCostDisplay',
                'IsLive'
            ],
            'Business.OtherRunningCost.');

        const elements = row.ModelElements;
        if (!elements)
        {
            console.log(logPrefix + 'Row elements missing');
            return;
        }

        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.GetParent();

        let name = 'Business.OtherRunningCosts.Currency';
        const currencyElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!currencyElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        // Do the business
        // Cost Name
        let value = model.Name;
        elements.NameDisplay.innerHTML = value;
        elements.NameDisplay.setAttribute ('old', value);


        // Cost Type
        value = model.CostTypeId;
        elements.CostTypeName.setAttribute(
            'oldId',
            value);

        let selectedOption = elements.CostTypeId.options[elements.CostTypeId.selectedIndex];

        elements.CostTypeName.innerHTML = selectedOption.text;
        elements.CostTypeName.setAttribute(
            'old',
            selectedOption.text);



        // Description
        value = model.Description;
        elements.DescriptionDisplay.innerHTML = value;
        elements.DescriptionDisplay.setAttribute('old', value);



        // Frequency
        elements.FrequencyName.setAttribute(
            'oldId',
            model.FrequencyId);

        selectedOption = elements.FrequencyId.options[elements.FrequencyId.selectedIndex];

        elements.FrequencyName.innerHTML = selectedOption.text;
        elements.FrequencyName.setAttribute(
            'old',
            selectedOption.text);



        let symbol = currencyElement.value;




        value = elements.Cost.innerHTML;
        elements.CostDisplay.setAttribute('old', value);
        elements.CostDisplay.innerHTML = `${symbol}&nbsp;${value}`;



        value = elements.AnnualCost.innerHTML;
        elements.AnnualCostDisplay.setAttribute('old', value);
        elements.AnnualCostDisplay.innerHTML = `${symbol}&nbsp;${value}`;




        elements.IsLive.setAttribute('disabled', '');

        let old = 'False';
        if (elements.IsLive.checked)
        {
            old = 'True';
        }

        elements.IsLive.value = old;
        elements.IsLive.setAttribute('old', old);




        if (SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.IsNew)
        {
            name = 'Business.OtherRunningCost.Id';
            const element = row.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element)
            {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            element.innerHTML = str;
        }


        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.IsNew = false;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Cancel_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SetMessage(
            'Saved',
            'text-success');
    },



    Delete_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Delete_Click: ';

        const row = this.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SelectedRow missing');
            return;
        }


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'Name'
            ],
            'Business.OtherRunningCost.');


        if (!model || !model.Id)
        {
            console.log(logPrefix + 'Cannot find other running cost Id');
            return;
        }


        if (!model.Name)
        {
            console.log(logPrefix + 'Cannot find other running cost');
            return;
        }



        const url = `/api/Business/OtherRunningCost/Delete/${model.Id}`;


        SilkFlo.Models.Abstract.Delete(
            url,
            model.Name,
            '',
            this.Delete_Callback,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.OtherRunningCosts.Container');
    },

    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Delete_Callback: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SelectedRow;

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

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.SelectedRow = null;

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.UpdateView();
    },



    Search: function (
                searchText,
                page,
                targetElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Search: ';


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
            const id = 'Settings.PlatformSetup.CostSetup.OtherRunningCosts.Search';
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

        let url = '/api/settings/Tenant/platformSetup/CostSetup/OtherRunningCosts';
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
    },




    Frequency_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Frequency_OnInput: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.EditRow;

        // Guard Clause
        if (!row)
            return;



        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Cost',
                'AnnualCost'
            ],
            'Business.OtherRunningCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;



        // Do the business
        if (event.target.value === 'Monthly')
            model.Cost *= 12;
        else
            model.Cost *= 1;


        let unitCost = SilkFlo.FormatNumber(model.Cost);

        elements.AnnualCost.value = model.Cost;
        elements.AnnualCost.innerHTML = unitCost;

    },

    Cost_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.Cost_OnInput: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.EditRow;

        // Guard Clause
        if (!row)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'FrequencyId',
                'AnnualCost',
                'Cost'
            ],
            'Business.OtherRunningCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        if (model.FrequencyId === 'Monthly')
            model.Cost *= 12;
        else
            model.Cost *= 1;



        const unitCost = SilkFlo.FormatNumber(model.Cost);

        elements.AnnualCost.value = model.Cost;
        elements.AnnualCost.innerHTML = unitCost;

    },

    AnnualCost_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.AnnualCost_OnInput: ';


        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.OtherRunningCosts.EditRow;

        // Guard Clause
        if (!row)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'FrequencyId',
                'AnnualCost',
                'Cost'
            ],
            'Business.OtherRunningCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        if (model.FrequencyId === 'Monthly')
            model.AnnualCost /= 12;
        else
            model.AnnualCost *= 1;


        const annualCost = SilkFlo.FormatNumber(model.AnnualCost);


        elements.Cost.value = model.AnnualCost;
        elements.Cost.innerHTML = annualCost;
    }
};