if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Edit)
    SilkFlo.ViewModels.Business.Idea.Edit = {};

SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts = {

    SelectedRow: null,
    IsLoading: false,

    GetCurrency: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetCurrency: ';


        const id = 'Business.Idea.Currency';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        if (!element.value)
        {
            console.log(`${logPrefix}currency symbol missing`);
            return '';
        }

        return element.value;
    },


    GetParent: function ()
    {
        const id = 'Business.Idea.CostBenefit.CostEstimates.RPASoftwareCosts';
        const parent = document.getElementById(id);


        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return parent;
    },


    UpdateView: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.UpdateView: ';

        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;


        SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['NewButton', 'DeleteButton'],
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
            elements.NewButton.classList.add ( 'hide' ); // Hide New
            elements.DeleteButton.classList.add ( 'hide' ); // Hide Delete
            return;
        }
        else
        {
            elements.NewButton.classList.remove('hide'); // Hide New

            // nothing selected
            if (this.SelectedRow)
                elements.DeleteButton.classList.remove('hide');    // Hide Delete
            else
                elements.DeleteButton.classList.add('hide');    // Hide Delete
        }
    },


    // SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.New_Click
    New_Click: function ()
    {
        const parent = this.GetParent();

        // Guard Clause
        if (!parent)
            return;


        this.IsLoading = true;
        this.UpdateView();
        const url = '/api/Business/ImplementationCost/GetRPSSoftwareCosts/NewRow';

        const name = 'Business.IdeaRunningCost.Tbody';
        const tbody = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!tbody)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        SilkFlo.DataAccess.AppendElement(
            url,
            null,
            tbody,
            this.New_CallBack,
            true,
            tbody.lastElementChild);
    },

    New_CallBack: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetParent();

        // Guard Clause
        if (!parent)
            return;

        const name = 'Business.IdeaRunningCost.Tbody';
        const tbody = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!tbody)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        let length = tbody.children.length;
        if (length)
            length -= 2;

        const row = tbody.children[length];
        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.IsLoading = false;
        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.SelectRow_Click(row);
        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.UpdateView();
    },


    SelectRow_Click: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.SelectRow_Click: ';
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        if (this.EditRow
            && element.isEqualNode(this.EditRow))
            return;

        this.SelectedRow = element;


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


    Delete_Click: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.Delete_Click: ';

        const row = this.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.SelectedRow missing');
            return;
        }



        const name = 'Business.IdeaRunningCost.RunningCostId';
        const element = row.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }


        let text = 'unknown';

        if (element.selectedIndex !== -1)
            text = element.children[element.selectedIndex].textContent;

        bootbox.dialog({
            title: `Delete ${name}`,
            message: `Are you sure that you want to delete <b>${text}</b>?`,
            onEscape: true,
            backdrop: true,
            buttons: {
                cancel: {
                    label: 'Cancel',
                    className: 'btn-secondary bootbox-accept'
                },
                delete: {
                    label: 'Delete',
                    className: 'btn-danger',
                    callback: SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.Delete_Callback

                }
            }
        });
    },

    Delete_Callback: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.Delete_Callback: ';

        const row = SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.SelectedRow;

        if (!row)
        {
            console.log(logPrefix + 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.SelectedRow missing');
            return;
        }
        row.classList.remove('select');
        row.classList.add('glow-border-red');
        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.SelectedRow = null;

        setTimeout(function ()
        {
            row.classList.remove('glow-border-red');
            row.remove();

            SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.UpdateView();

            const parent = SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetParent();

            const name = 'Business.IdeaRunningCost.Tbody';
            const tbody = parent.querySelector(`[name="${name}"]`);;

            // Guard Clause
            if (!tbody)
            {
                console.log(`${logPrefix}Element with name ${name} missing`);
                return;
            }

            SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.UpdateTotals(tbody);

        }, 1000);
    },

    RunningCost_Change: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.RunningCost_Change: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        const row = element.parentElement.parentElement.parentElement;

        if (row.localName !== 'tr')
        {
            console.log(logPrefix + 'parent not tr');
            return;
        }

        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.CalculateRowTotal(row);
    },

    LicenceCount_Input: function (element)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.LicenceCount_Input: ';

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        const row = element.parentElement.parentElement;

        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.CalculateRowTotal ( row );
    },

    CalculateRowTotal: function (rowElement)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.CalculateRowTotal: ';

        // Guard Clause
        if (!rowElement)
        {
            console.log(`${logPrefix}rowElement parameter missing`);
            return;
        }


        // Guard Clause
        if (rowElement.localName !== 'tr')
        {
            console.log(`${logPrefix}rowElement is not a tr. It is a ${rowElement.localName}`);
            return;
        }



        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            rowElement,
            ['RunningCostId', 'LicenceCount', 'CostPerYear', 'CostPerMonth', 'TotalCostPerYear', 'TotalCostPerMonth'],
            'Business.IdeaRunningCost.');




        const runningCostId = rowElement.ModelElements.RunningCostId;
        let period = '';
        let cost = 0;
        if (runningCostId.selectedIndex !== -1)
        {
            const option = runningCostId.children[runningCostId.selectedIndex];
            period = option.getAttribute('period');
            cost = option.getAttribute ('cost');
        }


        let licenceCount = model.LicenceCount;

        if (!licenceCount)
            licenceCount = 0;



        let costYear = 0;
        let costMonth = 0;
        let totalCostYearValue = 0;
        let totalCostMonthValue = 0;

        if (period === 'Annual')
        {
            costYear = cost * 1;
            costMonth = cost / 12;
        }
        else
        {
            costYear = cost * 12;
            costMonth = cost * 1;
        }

        totalCostYearValue = costYear * licenceCount;
        totalCostMonthValue = costMonth * licenceCount;


        costYear = SilkFlo.FormatNumber(costYear);
        costMonth = SilkFlo.FormatNumber(costMonth);
        const totalCostYear = SilkFlo.FormatNumber(totalCostYearValue);
        const totalCostMonth = SilkFlo.FormatNumber(totalCostMonthValue);




        // Apply Values
        let currency = SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetCurrency();


        rowElement.ModelElements.CostPerYear.innerHTML = currency + '&nbsp;' + costYear;
        rowElement.ModelElements.CostPerMonth.innerHTML = currency + '&nbsp;' + costMonth;

        rowElement.ModelElements.TotalCostPerYear.innerHTML = currency + '&nbsp;' + totalCostYear;
        rowElement.ModelElements.TotalCostPerMonth.innerHTML = currency + '&nbsp;' + totalCostMonth;

        rowElement.ModelElements.TotalCostPerYear.setAttribute (
            'value',
            totalCostYearValue);

        rowElement.ModelElements.TotalCostPerMonth.setAttribute(
            'value',
            totalCostMonthValue);

        const tbody = rowElement.parentElement;


        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.UpdateTotals(tbody);
    },


    UpdateTotals: function (tbody)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.UpdateTotals: ';

        // Guard Clause
        if (!tbody)
        {
            console.log(`${logPrefix}tbody parameter missing`);
            return;
        }

        const models = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            tbody,
            'Business.IdeaRunningCost',
            ['TotalCostPerYear', 'TotalCostPerMonth'],
            'Business.IdeaRunningCost.');

        let totalCostYear = 0;
        let totalCostMonth = 0;


        const length = models.length;
        for (let i = 0; i < length; i++)
        {
            const row = tbody.ModelElements[i];
            const modelElements = row.ModelElements;

            const totalCostYearValue = modelElements.TotalCostPerYear.getAttribute('value');
            totalCostYear += totalCostYearValue * 1;

            const totalCostMonthValue = modelElements.TotalCostPerMonth.getAttribute('value');
            totalCostMonth += totalCostMonthValue * 1;
        }

        let name = 'RpaTotalCostPerYear';
        const grandTotalCostPerYearElement = tbody.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!grandTotalCostPerYearElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        name = 'RpaTotalCostPerMonth';
        const grandTotalCostPerMonthElement = tbody.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!grandTotalCostPerMonthElement)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        let currency = SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.GetCurrency();


        grandTotalCostPerYearElement.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(totalCostYear);
        grandTotalCostPerMonthElement.innerHTML = currency + '&nbsp;' + SilkFlo.FormatNumber(totalCostMonth);

        SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.UpdateTotals ();
    }
}