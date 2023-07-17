if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Settings)
    SilkFlo.ViewModels.Settings = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup = {};

if (!SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup)
    SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup = {};

// CostSetup.RunningCosts Namespace: Code to manage the content inside the CostSetup > RunningCosts tab
SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts = {

    IsNew: null,
    SelectedRow: null,
    EditRow: null,
    IsLoading: false,


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.GetParent
    GetParent: function ()
    {
        const id = 'Settings.PlatformSetup.PlatformSetup.CostSetup.RunningCosts.Container';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },

    SetMessage: function (text, cls)
    {
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.GetParent ();

        const name = 'Message';
        const element = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.SetMessage: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        element.setAttribute('class', cls);
        element.innerHTML = text;
    },

    UpdateNewButtonToolTip: function (text)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.UpdateNewButtonToolTip: ';

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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.UpdateEditButtonToolTip: ';

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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.UpdateView: ';

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
            elements.EditButton.classList.add('hide');      // Hide edit
            elements.CancelButton.classList.add('hide');    // Hide Cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide delete
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
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.remove('hide'); // Show Cancel
            elements.SaveButton.classList.remove('hide');   // Show Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
        } else
        {
            // nothing selected
            elements.EditButton.classList.add('hide');      // Hide Edit
            elements.CancelButton.classList.add('hide');    // Hide cancel
            elements.SaveButton.classList.add('hide');      // Hide Save
            elements.DeleteButton.classList.add('hide');    // Hide Delete
        }
    },


    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.New_Click
    New_Click: function ()
    {
        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;

        this.IsLoading = true;
        this.Cancel_Click();

        const url = '/api/settings/Tenant/platformSetup/CostSetup/RunningCosts/NewRow';
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
        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const tbody = parent.querySelector('tbody');

        const row = tbody.children[0];
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.IsLoading = false;

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.SelectRow_Click(row);
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Edit_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.IsNew = true;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.UpdateView();
    },

    Cancel_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Cancel_Click: ';


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
        SilkFlo.Models.Abstract.GetModelFromParent (
            row,
            [
                'VenderName',
                'VenderId',
                'AutomationTypeName',
                'AutomationTypeId',
                'LicenceTypeDisplay',
                'LicenceType',
                'FrequencyName',
                'FrequencyId',
                'CostDisplay',
                'Cost',
                'AnnualCostDisplay',
                'AnnualCost',
                'IsLive'
            ],
            'Business.RunningCost.' );


        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        let oldId = elements.VenderName.getAttribute('oldId');
        elements.VenderId.value = oldId;

        oldId = elements.AutomationTypeName.getAttribute('oldId');
        elements.AutomationTypeId.value = oldId;

        let old = elements.LicenceTypeDisplay.getAttribute('old');
        elements.LicenceType.value = old;

        oldId = elements.FrequencyName.getAttribute('oldId');
        elements.FrequencyId.value = oldId;

        old = elements.CostDisplay.getAttribute('old');
        elements.Cost.value = old;

        old = elements.AnnualCostDisplay.getAttribute('old');
        elements.AnnualCost.value = old;

        // Deselect
        if (window.getSelection)
            window.getSelection().removeAllRanges();

        else if (document.selection)
            document.selection.empty();


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
            const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.SelectRow_Click: ';
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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Edit_Click: ';


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
        SilkFlo.Models.Abstract.GetModelFromParent (
            row,
            [
                'IsLive', 'LicenceType', 'Cost', 'AnnualCost'
            ],
            'Business.RunningCost.' );

        const elements = row.ModelElements;


        elements.IsLive.removeAttribute('disabled');

        row.classList.add('edit');
        this.IsNew = false;

        elements.LicenceType.onkeydown = this.KeyPress;

        Delaney.UI.ToolTip.Hide();

        this.UpdateView();
    },



    // SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.KeyPress
    KeyPress: function (event)
    {
        if (event.which === 13)
            event.preventDefault();

        if (event.key === 'Enter')
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Save_Click();

        if (event.key === 'Escape')
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Cancel_Click();
    },

    Dbl_Click: function (element)
    {
        this.SelectRow_Click(element);
        this.Edit_Click();
    },

    Save_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Save_Click: ';

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
                'VenderId',
                'AutomationTypeId',
                'LicenceType',
                'FrequencyId',
                'Cost',
                'IsLive'
            ],
            'Business.RunningCost.');


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'Model is missing');
            return;
        }


        // Guard Clause
        if (!model.VenderId)
        {
            this.SetMessage('Please select a software vender', 'text-warning');
            return;
        }

        // Guard Clause
        if (!model.AutomationTypeId)
        {
            this.SetMessage('Please select an automation type', 'text-warning');
            return;
        }


        // Guard Clause
        if (!model.LicenceType)
        {
            this.SetMessage('Please provide a licence type', 'text-warning');
            return;
        }


        const url = '/api/Business/runningCost/Post';
        SilkFlo.Models.Abstract.Save(
            model,
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Save_CallBack,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.RunningCosts.Container',
            url,
            'POST');
    },


    Save_CallBack: function (
        str)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Save_CallBack: ';

        // Guard Clause
        if (!str)
        {
            SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Cancel_Click();
            return;
        }


        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.EditRow;

        // Guard Clause
        if (!row)
        {
            console.log(`${logPrefix}this.EditRow missing`);
            return;
        }





        // Get the elements
        SilkFlo.Models.Abstract.GetModelFromParent (
            row,
            [
                'VenderId',
                'VenderName',
                'AutomationTypeName',
                'AutomationTypeId',
                'LicenceType',
                'LicenceTypeDisplay',
                'FrequencyName',
                'FrequencyId',
                'Cost',
                'CostDisplay',
                'AnnualCost',
                'AnnualCostDisplay',
                'IsLive'
            ],
            'Business.RunningCost.' );

        const elements = row.ModelElements;
        if (!elements)
        {
            console.log(logPrefix + 'Row elements missing');
            return;
        }

        const parent = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.GetParent ();

        let name = 'Business.RunningCosts.Currency';
        const currencyElement = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!currencyElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        // Do the business

        // Software Vender
        elements.VenderName.setAttribute (
            'oldId',
            elements.VenderId.value );

        let selectedOption = elements.VenderId.options[elements.VenderId.selectedIndex];

        elements.VenderName.innerHTML = selectedOption.text;
        elements.VenderName.setAttribute(
            'old',
            selectedOption.text);



        // Automation Type	
        elements.AutomationTypeName.setAttribute(
            'oldId',
            elements.AutomationTypeId.value);

        selectedOption = elements.AutomationTypeId.options[elements.AutomationTypeId.selectedIndex];

        elements.AutomationTypeName.innerHTML = selectedOption.text;
        elements.AutomationTypeName.setAttribute(
            'old',
            selectedOption.text);


        // Licence Type
        let value = elements.LicenceType.innerHTML;
        elements.LicenceTypeDisplay.innerHTML = value;
        elements.LicenceTypeDisplay.setAttribute (
            'old',
            value);


        // Frequency
        elements.FrequencyName.setAttribute(
            'oldId',
            elements.FrequencyId.value);

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
            old = 'True';

        elements.IsLive.value = old;
        elements.IsLive.setAttribute('old', old);




        if (SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.IsNew)
        {
            name = 'Business.RunningCost.Id';
            const element = row.querySelector(`[name="${name}"]`);

            // Guard Clause
            if (!element)
            {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            element.innerHTML = str;
        }





        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.IsNew = false;
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Cancel_Click();
        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.SetMessage(
            'Saved',
            'text-success');
    },





    Delete_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Delete_Click: ';

        const row = this.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.SelectedRow missing');
            return;
        }


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Id',
                'VenderName',
                'AutomationTypeName'
            ],
            'Business.RunningCost.');


        if (!model || !model.Id)
        {
            console.log(logPrefix + 'Cannot find software vender Id');
            return;
        }


        if (!model.VenderName)
        {
            console.log(logPrefix + 'Cannot find software vender');
            return;
        }



        const url = `/api/Business/RunningCost/Delete/${model.Id}`;


        SilkFlo.Models.Abstract.Delete(
            url,
            `${model.VenderName}&nbsp;-&nbsp;${model.AutomationTypeName}`,
            '',
            this.Delete_Callback,
            SilkFlo.DataAccess.Feedback,
            'Settings.PlatformSetup.PlatformSetup.CostSetup.RunningCosts.Container');
    },

    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Delete_Callback: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.SelectedRow;

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

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.SelectedRow = null;

        SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.UpdateView();
    },






    Search: function (
                searchText,
                page,
                targetElementId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Search: ';


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
            const id = 'Settings.PlatformSetup.CostSetup.RunningCosts.Search';
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

        let url = '/api/settings/Tenant/platformSetup/CostSetup/RunningCosts';
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
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Frequency_OnInput: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.EditRow;

        // Guard Clause
        if (!row)
            return;



        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'Cost',
                'AnnualCost'
            ],
            'Business.RunningCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        let unitCost = elements.Cost.innerHTML;

        // Do the business
        if (event.target.value === 'Monthly')
            unitCost = unitCost * 12;


        unitCost = parseFloat(unitCost)
            .toFixed(2);

        elements.AnnualCost.innerHTML = unitCost;
    },
    
    Cost_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.Cost_OnInput: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.EditRow;

        // Guard Clause
        if (!row)
            return;


        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'FrequencyId',
                'AnnualCost'
            ],
            'Business.RunningCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        let unitCost = event.target.innerHTML;

        if (elements.FrequencyId.value === 'Monthly')
            unitCost = unitCost * 12;



        unitCost = parseFloat(unitCost)
            .toFixed(2);

        elements.AnnualCost.innerHTML = unitCost;
    },

    AnnualCost_OnInput: function (event)
    {
        const logPrefix = 'SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.AnnualCost_OnInput: ';

        const row = SilkFlo.ViewModels.Settings.PlatformSetup.CostSetup.RunningCosts.EditRow;

        // Guard Clause
        if (!row)
            return;


        SilkFlo.Models.Abstract.GetModelFromParent(
            row,
            [
                'FrequencyId',
                'Cost',
            ],
            'Business.RunningCost.');

        // Guard Clause
        if (!row.ModelElements)
        {
            console.log(`${logPrefix}ModelElements missing`);
            return;
        }

        const elements = row.ModelElements;


        // Do the business
        let annualCost = event.target.innerHTML;

        if (elements.FrequencyId.value === 'monthly')
            annualCost = annualCost / 12;

        annualCost = parseFloat(annualCost)
            .toFixed(2);


        elements.Cost.innerHTML = annualCost;
    },
};