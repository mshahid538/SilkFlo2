if (!SilkFlo.ViewModels)
    SilkFlo.ViewModels = {};

if (!SilkFlo.ViewModels.Business)
    SilkFlo.ViewModels.Business = {};

if (!SilkFlo.ViewModels.Business.Idea)
    SilkFlo.ViewModels.Business.Idea = {};

if (!SilkFlo.ViewModels.Business.Idea.Section)
    SilkFlo.ViewModels.Business.Idea.Section = {};


SilkFlo.ViewModels.Business.Idea.Section.CostBenefit = {

    GetParent: function ()
    {
        const id = 'Business.Idea.Section.CostBenefit.Content';
        const element = document.getElementById(id);

        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent: ';
            console.log(`${logPrefix}Element with id ${id} missing`);
            return null;
        }

        return element;
    },


    SetMessage: function (text, errorCount)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.SetMessage';

        let id = 'errorButton';
        const errorButton = document.getElementById(id);

        // Guard Clause
        if (!errorButton)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }


        if (text)
        {
            errorButton.style.display = 'block';
            if (errorCount === 0)
                errorButton.innerHTML = '1 error';
            else
                errorButton.innerHTML = errorCount + ' errors';
        }
        else
            errorButton.style.display = 'none';


        id = 'errorModalBody';
        const errorModalBody = document.getElementById(id);

        // Guard Clause
        if (!errorModalBody)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        text = `<div style="color: var(--bs-danger);">${text}</div>`;

        errorModalBody.innerHTML = text;
    },

    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Edit
    Edit: function (element, id)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Edit: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Guard Clause
        if (!id) {
            console.log(`${logPrefix}id parameter missing`);
            return;
        }

        SilkFlo.DisableButton ( element );

        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;

        const url = `/api/Business/Idea/Section/CostBenefit/Edit/${id}`;
        SilkFlo.DataAccess.UpdateElement(
            url,
            null,
            parent,
            null,
            'GET',
            SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Edit_CallBack);
    },

    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Edit_CallBack
    Edit_CallBack: function ()
    {
        SilkFlo.DataAccess.GetComponents ();
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.CreateGanttCharts();
        SilkFlo.ViewModels.Business.Idea.Edit.OneTimeCosts.CalculateTotals();

        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;

        const name = 'Business.IdeaRunningCost.Tbody';
        const tbodyIdeaRunningCost = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!tbodyIdeaRunningCost)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Edit_CallBack: ';
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.ViewModels.Business.Idea.Edit.RPSSoftwareCosts.UpdateTotals(tbodyIdeaRunningCost);

        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateOtherRunningCosts('Business.Idea.CostBenefit.CostEstimates.SoftwareLicence');
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateOtherRunningCosts('Business.Idea.CostBenefit.CostEstimates.Support');
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateOtherRunningCosts('Business.Idea.CostBenefit.CostEstimates.Infrastructure');
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateOtherRunningCosts('Business.Idea.CostBenefit.CostEstimates.Other');
        SilkFlo.ViewModels.Business.Idea.Edit.RunningCosts.UpdateTotals();
    },

    UpdateOtherRunningCosts: function(parentId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateOtherRunningCosts: ';


        // Guard Clause
        if (!parentId)
        {
            console.log(`${logPrefix}parentId parameter missing`);
            return;
        }

        const parent = document.getElementById(parentId);

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        const name = 'tbody';
        const tbody = parent.querySelector(`[name="${name}"]`);

        // Guard Clause
        if (!tbody)
        {
            console.log(`${logPrefix}Element with name ${name} missing`);
            return;
        }

        SilkFlo.ViewModels.Business.Idea.Edit.IdeaOtherRunningCosts.UpdateTotals(tbody);
    },


    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.RunningCostId_Change
    RunningCostId_Change: function ()
    {
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateDisplay ();
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateWorkloadSplitEstimations();
    },

    WorkloadSplit_OnInput: function (event)
    {
        const element = event.target;

        const value = element.innerHTML.trim();


        if (IsNumeric ( value ))
        {
            if (value < 0)
                element.innerHTML = 0;

            if (value > 100)
                element.innerHTML = 100;
        }


        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateWorkloadSplitEstimations ();
    },

    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateWorkloadSplitEstimations
    UpdateWorkloadSplitEstimations: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateWorkloadSplitEstimations: ';


        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['RunningCostId', 'B1', 'C1', 'WorkloadSplit'],
            'Business.Idea.');


        // Get automationTypeId
        let automationTypeId = '';
        if (parent.ModelElements.RunningCostId.selectedIndex !== -1)
        {
            const option = parent.ModelElements.RunningCostId.children[parent.ModelElements.RunningCostId.selectedIndex];
            automationTypeId = option.getAttribute('AutomationTypeId');
        }



        // Get estimatedAutomationPotential
        let estimatedAutomationPotential = 100;
        if (automationTypeId === 'Unattended')
        {
            const attribute = parent.ModelElements.C1.getAttribute('estimatedAutomationPotential');
            // Guard Clause
            if (!attribute)
            {
                console.log(`${logPrefix}B2 value attribute missing`);
                return;
            }

            estimatedAutomationPotential = attribute * 1;
        }

        let workloadSplit = model.WorkloadSplit;
        if (!workloadSplit && workloadSplit !== 0)
            workloadSplit = 0;


        const automationPotential = 100 - workloadSplit;


        parent.ModelElements.B1.innerHTML = SilkFlo.FormatNumber(automationPotential) + '&nbsp;%';

        if (automationTypeId)
            parent.ModelElements.C1.innerHTML = SilkFlo.FormatNumber(estimatedAutomationPotential) + '&nbsp;%';
        else
            parent.ModelElements.C1.innerHTML = '';

        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow2VolumePerYear (automationPotential, estimatedAutomationPotential, workloadSplit);
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow3TotalFTEFTR (automationPotential, estimatedAutomationPotential, workloadSplit, automationTypeId);
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow4TotalHoursYear (automationPotential, estimatedAutomationPotential, workloadSplit);
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow567RunningCost (automationPotential, estimatedAutomationPotential, workloadSplit);
    },

    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateAHTRobotPerTransaction
    UpdateAHTRobotPerTransaction: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;

        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'RobotSpeedMultiplier',
                'AHT',
                'AHTRobot'
        ],
            'Business.Idea.');

        const elements = parent.ModelElements;

        let formatted;

        if (model.RobotSpeedMultiplier)
        {
            const aht = model.AHT * 1;
            const robotSpeedMultiplier = model.RobotSpeedMultiplier * 1;
            const value = aht / robotSpeedMultiplier;
            formatted = SilkFlo.FormatNumber ( value );
        }
        else
            formatted = SilkFlo.FormatNumber( model.AHT );

        formatted = formatted.replace (
            ',',
            '' );

        elements.AHTRobot.innerHTML = formatted;

        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateWorkloadSplitEstimations();
    },



    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateDisplay
    UpdateDisplay: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            [
                'RunningCostId',
                'ProcessVolumetryPerYear',
                'ProcessVolumetryPerMonth',
                'EmployeeCount',
                'RobotWorkHourDay',
                'RobotWorkDayYear',
                'RobotSpeedMultiplier',
                'RobotWorkHourDay_Container',
                'RobotWorkDayYear_Container'],
                'Business.Idea.');

        const modelElements = parent.ModelElements;




        let automationTypeId = '';
        if (modelElements.RunningCostId.selectedIndex !== -1)
        {
            const option = modelElements.RunningCostId.children[modelElements.RunningCostId.selectedIndex];
            automationTypeId = option.getAttribute ( 'AutomationTypeId' );
        }



        if (automationTypeId === 'Attended')
        {
            // Attended: robots that need human interaction.
            // In this case, the robot assumptions will **not be displayed**
            // as the platform is set up to balance
            // the number of attended robots with the number of employees performing the process.

            modelElements.ProcessVolumetryPerYear.style.background = '';
            modelElements.ProcessVolumetryPerYear.setAttribute (
                'contenteditable',
                '');

            modelElements.ProcessVolumetryPerMonth.style.background = '';
            modelElements.ProcessVolumetryPerMonth.setAttribute(
                'contenteditable',
                '');


            modelElements.EmployeeCount.style.background = '';
            modelElements.EmployeeCount.setAttribute(
                'contenteditable',
                '');

            modelElements.RobotWorkHourDay_Container.style.display = 'none';
            modelElements.RobotWorkDayYear_Container.style.display = 'none';
        }
        else if (automationTypeId === 'Unattended')
        {
            // unattended
            // Unattended: robots that don't require any human interaction.
            // In this case, the Project Manager needs to manually input the following Automation Assumptions:
            // * Robot Working hours / day,
            // * Robot Working days / year,
            // * and Robot Speed multiplier

            modelElements.ProcessVolumetryPerYear.style.background = 'var(--readOnly-background)';
            modelElements.ProcessVolumetryPerYear.removeAttribute (
                'contenteditable' );


            modelElements.ProcessVolumetryPerMonth.style.background = 'var(--readOnly-background)';
            modelElements.ProcessVolumetryPerMonth.removeAttribute (
                'contenteditable' );


            modelElements.EmployeeCount.style.background = 'var(--readOnly-background)';
            modelElements.EmployeeCount.removeAttribute (
                'contenteditable' );

            modelElements.RobotWorkHourDay_Container.style.display = 'block';
            modelElements.RobotWorkDayYear_Container.style.display = 'block';
        }
        else
        {
            modelElements.ProcessVolumetryPerYear.style.background = 'var(--readOnly-background)';
            modelElements.ProcessVolumetryPerYear.removeAttribute(
                'contenteditable');


            modelElements.ProcessVolumetryPerMonth.style.background = 'var(--readOnly-background)';
            modelElements.ProcessVolumetryPerMonth.removeAttribute(
                'contenteditable');

            modelElements.EmployeeCount.style.background = 'var(--readOnly-background)';
            modelElements.EmployeeCount.removeAttribute (
                'contenteditable');

            modelElements.RobotWorkHourDay_Container.style.display = 'none';
            modelElements.RobotWorkDayYear_Container.style.display = 'none';
        }

        let message = '';
        let errorCount = 0;
        if (model.RobotWorkHourDay < 0 || model.RobotWorkHourDay > 24)
        {
            message += '<p>The robot working hours in a day must be between 0 and 24.</p>';
            errorCount++;
        }

        if (model.RobotWorkDayYear < 0 || model.RobotWorkDayYear > 365)
        {
            message += '<p>The robot working days in a year must be between 0 and 365.</p>';
            errorCount++;
        }

        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.SetMessage(message, errorCount);
        if (message)
            return;
    },

    // Row 2 - Volumes/Year
    UpdateRow2VolumePerYear: function (automationPotential, estimatedAutomationPotential, workloadSplit)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow2VolumePerYear: ';


        // Guard Clause
        if (!IsNumeric(automationPotential))
        {
            console.log(`${logPrefix}automationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(estimatedAutomationPotential))
        {
            console.log(`${logPrefix}estimatedAutomationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(workloadSplit))
        {
            console.log(`${logPrefix}workloadSplit is not a number`);
            return;
        }


        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['RunningCostId', 'A2', 'B2', 'C2', 'D2'],
            'Business.Idea.');

        const modelElements = parent.ModelElements;


        const A2Attribute = modelElements.A2.getAttribute('value');

        // Guard Clause
        if (!A2Attribute)
        {
            console.log(`${logPrefix}A2 value attribute missing`);
            return;
        }

        const A2 = A2Attribute * 1;


        // Column B
        if (!model.RunningCostId)
            modelElements.B2.innerHTML = '';
        else
            modelElements.B2.innerHTML = SilkFlo.FormatNumber(A2 / 100 * automationPotential);



        // Column C
        if (!model.RunningCostId)
            modelElements.C2.innerHTML = '';
        else
            modelElements.C2.innerHTML = SilkFlo.FormatNumber(A2 / 100 * estimatedAutomationPotential);


        // Column D
        modelElements.D2.innerHTML = SilkFlo.FormatNumber(A2 / 100 * workloadSplit);
    },

    // Row 3 - Total FTE/FTR
    UpdateRow3TotalFTEFTR: function (automationPotential, estimatedAutomationPotential, workloadSplit, automationTypeId)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow3TotalFTEFTR: ';


        // Guard Clause
        if (!IsNumeric(automationPotential))
        {
            console.log(`${logPrefix}automationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(estimatedAutomationPotential))
        {
            console.log(`${logPrefix}estimatedAutomationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(workloadSplit))
        {
            console.log(`${logPrefix}workloadSplit is not a number`);
            return;
        }


        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['RunningCostId', 'A3', 'B3', 'C3', 'D3', 'ProcessVolumetryPerYear', 'AHTRobot', 'RobotWorkDayYear', 'RobotWorkHourDay'],
            'Business.Idea.');

        const modelElements = parent.ModelElements;


        const A3Attribute = modelElements.A3.getAttribute('value');

        // Guard Clause
        if (!A3Attribute)
        {
            console.log(`${logPrefix}A3 value attribute missing`);
            return;
        }

        const A3 = A3Attribute * 1;


        // Column B
        if (!model.RunningCostId)
            modelElements.B3.innerHTML = '';
        else
            modelElements.B3.innerHTML = SilkFlo.FormatNumber(A3 / 100 * automationPotential);



        const processVolumetryPerYear = model.ProcessVolumetryPerYear * 1;
        const ahtRobot = model.AHTRobot * 1;
        const robotWorkDayYear = model.RobotWorkDayYear * 1;
        const robotWorkHourDay = model.RobotWorkHourDay * 1;
        
        // Column C & D
        if (!model.RunningCostId)
        {
            modelElements.C3.innerHTML = '';
            modelElements.D3.innerHTML = '';
        }
        else
        {
            if (!model.RobotWorkDayYear
                || !model.RobotWorkHourDay
                || automationTypeId === 'Attended')
            {
                modelElements.C3.innerHTML = '';
                modelElements.D3.innerHTML = '';
            }
            else
            {
                modelElements.C3.innerHTML = SilkFlo.FormatNumber(processVolumetryPerYear * estimatedAutomationPotential * ahtRobot / 100 / robotWorkDayYear / robotWorkHourDay / 60 );
                modelElements.D3.innerHTML = SilkFlo.FormatNumber(processVolumetryPerYear * workloadSplit * ahtRobot / 100 / robotWorkDayYear / robotWorkHourDay / 60 );
            }
        }
    },

    // Row 4 - Total Hours/Year
    UpdateRow4TotalHoursYear: function (automationPotential, estimatedAutomationPotential, workloadSplit)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow4TotalHoursYear: ';


        // Guard Clause
        if (!IsNumeric(automationPotential))
        {
            console.log(`${logPrefix}automationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(estimatedAutomationPotential))
        {
            console.log(`${logPrefix}estimatedAutomationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(workloadSplit))
        {
            console.log(`${logPrefix}workloadSplit is not a number`);
            return;
        }


        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['RunningCostId', 'A4', 'B4', 'C4', 'D4'],
            'Business.Idea.');

        const modelElements = parent.ModelElements;


        const A4Attribute = modelElements.A4.getAttribute('value');

        // Guard Clause
        if (!A4Attribute)
        {
            console.log(`${logPrefix}A4 value attribute missing`);
            return;
        }

        const A4 = A4Attribute * 1;


        // Column B
        if (!model.RunningCostId)
            modelElements.B4.innerHTML = '';
        else
            modelElements.B4.innerHTML = SilkFlo.FormatNumber(A4 / 100 * automationPotential);
    },


    // Row 5 - Running Cost/Year
    // Row 6 - Running Cost/Month
    UpdateRow567RunningCost: function (automationPotential, estimatedAutomationPotential, workloadSplit)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow567RunningCost: ';


        // Guard Clause
        if (!IsNumeric(automationPotential))
        {
            console.log(`${logPrefix}automationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(estimatedAutomationPotential))
        {
            console.log(`${logPrefix}estimatedAutomationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(workloadSplit))
        {
            console.log(`${logPrefix}workloadSplit is not a number`);
            return;
        }


        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['RunningCostId', 'A5', 'B5', 'C5', 'D5', 'B6', 'C6', 'D6', 'A7', 'B7', 'C7', 'D7', 'Currency', 'ProcessVolumetryPerYear'],
            'Business.Idea.');

        const modelElements = parent.ModelElements;


        const A5Attribute = modelElements.A5.getAttribute('value');

        // Guard Clause
        if (!A5Attribute)
        {
            console.log(`${logPrefix}A5 value attribute missing`);
            return;
        }

        const A5 = A5Attribute * 1;


        // Column B
        if (!model.RunningCostId)
        {
            modelElements.B5.innerHTML = '';
            modelElements.B6.innerHTML = '';
        }
        else
        {
            const value = A5 / 100 * automationPotential;

            modelElements.B5.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber (value);
            modelElements.B6.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(value/12);
        }


        const A7Attribute = modelElements.A7.getAttribute('value');

        // Guard Clause
        if (!A7Attribute)
        {
            console.log(`${logPrefix}A7 value attribute missing`);
            return;
        }

        const A7 = A7Attribute * 1;

        // Column B
        if (!model.RunningCostId)
        {
            modelElements.B7.innerHTML = '';
        }
        else
        {
            const value = A7 / 100 * automationPotential;
            modelElements.B7.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(value);
        }




        let annualRunningCosts = 0;
        let name = 'Business.IdeaRunningCost.TotalCostPerYear';
        const totalCostPerYearElements = parent.querySelectorAll ('[name="' + name + '"]');
        let length = totalCostPerYearElements.length;
        for (let i = 0; i < length; i++)
        {
            const value = totalCostPerYearElements[i].getAttribute('value');
            if (value)
            {
                annualRunningCosts += value * 1;
            }
        }


        name = 'Business.IdeaOtherRunningCost.TotalCostPerYear';
        const otherTotalCostPerYearElements = parent.querySelectorAll ('[name="' + name + '"]');
        length = otherTotalCostPerYearElements.length;
        for (let i = 0; i < length; i++)
        {
            const value = otherTotalCostPerYearElements[i].getAttribute('value');
            if (value)
            {
                annualRunningCosts += value * 1;
            }
        }

        let runningCostTransaction = 0;
        if (model.ProcessVolumetryPerYear)
            runningCostTransaction = annualRunningCosts / model.ProcessVolumetryPerYear * 1;

        const monthlyRunningCosts = annualRunningCosts / 12;
        modelElements.C5.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(annualRunningCosts);
        modelElements.D5.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(annualRunningCosts);

        modelElements.C6.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(monthlyRunningCosts);
        modelElements.D6.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(monthlyRunningCosts);

        modelElements.C7.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(runningCostTransaction);
        modelElements.D7.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(runningCostTransaction);


    },


    // Row 7 - Running Cost / Transaction
    UpdateRow7HumanRunningCostTransaction: function (automationPotential, estimatedAutomationPotential, workloadSplit)
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.UpdateRow7HumanRunningCostTransaction: ';


        // Guard Clause
        if (!IsNumeric(automationPotential))
        {
            console.log(`${logPrefix}automationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(estimatedAutomationPotential))
        {
            console.log(`${logPrefix}estimatedAutomationPotential is not a number`);
            return;
        }


        // Guard Clause
        if (!IsNumeric(workloadSplit))
        {
            console.log(`${logPrefix}workloadSplit is not a number`);
            return;
        }


        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;


        const model = SilkFlo.Models.Abstract.GetModelFromParent(
            parent,
            ['RunningCostId', 'A7', 'B7', 'C7', 'D7', 'Currency'],
            'Business.Idea.');

        const modelElements = parent.ModelElements;


        const A7Attribute = modelElements.A7.getAttribute('value');

        // Guard Clause
        if (!A7Attribute)
        {
            console.log(`${logPrefix}A7 value attribute missing`);
            return;
        }

        const A7 = A7Attribute * 1;

        // Column B
        if (!model.RunningCostId)
        {
            modelElements.B7.innerHTML = '';
        }
        else
        {
            const value = A7 / 100 * automationPotential;
            modelElements.B7.innerHTML = model.Currency + '&nbsp;' + SilkFlo.FormatNumber(value);
        }
    },




    // Get the About Section
    GetTimeLineModel: function ()
    {
        const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetTimeLineModel: ';


        const model = {};


        let elementId = 'gantt1';
        let element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return null;
        }


        let ideaStageEstimates = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            element,
            'Business.IdeaStage',
            [
                'Id', 'DateStart', 'DateEnd'
            ],
            'Business.IdeaStage.');

        model.IdeaStageEstimates = ideaStageEstimates;



        elementId = 'gantt2';
        element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Element with id ${elementId} missing`);
            return null;
        }


        const ideaStages = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            element,
            'Business.IdeaStage',
            [
                'Id', 'DateStart', 'DateEnd'
            ],
            'Business.IdeaStage.');

        model.IdeaStages = ideaStages;





        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Model = model;
        return model;
    },


    // Save the CostBenefit Section
    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Save
    Save: function ()
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;

        const model = SilkFlo.Models.Abstract.GetModelFromParent (
            parent,
            [
                'Id',
                'EaseOfImplementationFinal',
                'RunningCostId',
                'ProcessVolumetryPerYear',
                'ProcessVolumetryPerMonth',
                'EmployeeCount',
                'RobotWorkHourDay',
                'RobotWorkDayYear',
                'RobotSpeedMultiplier',
                'AHTRobot',
                'WorkloadSplit'
            ],
            'Business.Idea.');

        const timelineModel = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetTimeLineModel();

        // Guard Clause
        if (!timelineModel)
        {
            const logPrefix = 'SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Save: ';
            console.log
                (`${logPrefix}timelineModel missing`);
            return;
        }

        //This is holds the contents of the table 'One Time Costs' in the UI

        const implementationCosts = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            parent,
            'Business.ImplementationCost',
            ['IdeaStageId', 'RoleId', 'Allocation', 'Day'],
            'Business.ImplementationCost.');

        //This is holds the contents of the table 'RPA Software Costs' in the UI
        const ideaRunningCosts = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            parent,
            'Business.IdeaRunningCost',
            ['RunningCostId', 'LicenceCount'],
            'Business.IdeaRunningCost.');

        const ideaOtherRunningCosts = SilkFlo.Models.Abstract.GetModelCollectionFromParent(
            parent,
            'Business.IdeaOtherRunningCost',
            ['OtherRunningCostId', 'Number'],
            'Business.IdeaOtherRunningCost.');

        model.IdeaStageEstimates = timelineModel.IdeaStageEstimates;
        model.IdeaStages = timelineModel.IdeaStages;
        model.ImplementationCosts = implementationCosts;
        model.IdeaRunningCosts = ideaRunningCosts;
        model.IdeaOtherRunningCosts = ideaOtherRunningCosts;


        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Model = model;

        SilkFlo.Models.Abstract.Save
                (
                    model,
                    SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Save_Callback,
                    SilkFlo.DataAccess.Feedback,
                    parent.id,
                    '/api/Business/Idea/Section/CostBenefit/Post',
                    'PUT');
    },


    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Save_Callback
    Save_Callback: function ()
    {
        const model = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Model;

        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Cancel
            (model.Id);
    },

    // Cancel the CostBenefit Section
    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Cancel
    Cancel: function (id)
    {
        const parent = SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.GetParent();

        if (!parent)
            return;

        const url = `/api/Business/Idea/Section/CostBenefit/Detail/${id}`;
        SilkFlo.DataAccess.UpdateElement (
            url,
            null,
            parent,
            '',
            'GET',
            SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Cancel_Callback);
    },

    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.Cancel_Callback
    Cancel_Callback: function ()
    {
        SilkFlo.ViewModels.Business.Idea.Section.CostBenefit.CreateGanttCharts();
        SilkFlo.ViewModels.Business.Idea.Section.ScrollTop();
        SilkFlo.DataAccess.GetComponents ();
    },


    // SilkFlo.ViewModels.Business.Idea.Section.CostBenefit
    CreateGanttCharts: function ()
    {
        Gantt.MainById('gantt1');
        Gantt.MainById('gantt2');
    }
};