if (!SilkFlo.Models2)
    SilkFlo.Models2 = {};

if (!SilkFlo.Models2.Business)
    SilkFlo.Models2.Business = {};

SilkFlo.Models2.Business.Idea = {

    /* GetCalculation Overview
     * -----------------------
     * Send the Idea model to the server api
     * for calculation. The return result is then
     * consumed by the afterFunction.
     * The afterFunction should display and store the value.
     * 
     *   url = './api/...'  */
    GetCalculation: function (
        model,
        url,
        targetElement,
        afterFunction)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.GetCalculation: ';


        // Guard Clause
        if (!model)
        {
            console.log(logPrefix + 'model parameter is missing.');
            return;
        }


        // Guard Clause
        if (!url)
        {
            console.log(logPrefix + 'url parameter is missing.');
            return;
        }


        // Guard Clause
        if (!targetElement)
        {
            console.log(logPrefix + 'targetElement parameter is missing.');
            return;
        }


        // Guard Clause
        if (!afterFunction)
        {
            console.log(logPrefix + 'afterFunction parameter is missing.');
            return;
        }


        // 1. Set up async request;
        const json = JSON.stringify(model);

        const http = new XMLHttpRequest();
        http.open('POST', url, true);
        http.setRequestHeader('Content-type', 'application/json');
        http.onreadystatechange = function () {
            return function () {

                // 3. Receive response from sender
                if (http.readyState === XMLHttpRequest.DONE
                    && http.status === 200) {

                    const s = http.responseText;
                    const n = parseFloat(s);
                    afterFunction(n, targetElement);

                }
            };
        }(this);

        // 2. Send
        http.send(json);
    },


    /* Take a number (n) and displays in the submit form
     * and store in the model */
    Populate_And_FormatToHoursPerYear: function (aFloat, elementId)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.Populate_And_FormatToHoursPerYear: ';

        if (!elementId)
        {
            console.log(logPrefix + 'elementId parameter is missing.');
            return;
        }


        // Get the element
        const element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Could not find an element with id of "${elementId}"`);
            return;
        }


        // Display
        element.value = SilkFlo.Models2.Business.Idea.FormatToHoursPerYear(aFloat);
    },


    Populate_And_FormatNumberTo2DecimalPlaces: function (aFloat, elementId = null)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberTo2DecimalPlaces: ';

        if (!elementId) {
            console.log(logPrefix + 'elementId is missing.');
            return;
        }

        // Get the element
        const element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Could not find an element with id of "${elementId}"`);
            return;
        }




        // Display
        element.value = SilkFlo.Models2.Business.Idea.FormatNumberTo2DecimalPlaces(aFloat);
    },



    Populate_And_FormatNumberToCurrency: function (aFloat, elementId = null)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToCurrency: ';

        if (!elementId)
        {
            console.log(logPrefix + 'elementId is missing.');
            return;
        }


        // Get the element
        const element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Could not find an element with id of "${elementId}"`);
            return;
        }


        const currencyElement = document.getElementById ('Currency');
        const currency = currencyElement.value;


        const ctrlLocale = document.getElementById ('Locale');
        const locale = ctrlLocale.value;

        // Guard Clause
        if (isNaN(aFloat))
        {
            element.value = currency + ' 0.00';
            return;
        }


        // Display
        element.value = currency + ' ' + aFloat.toLocaleString(locale);
    },


    Populate_And_FormatNumberToPercent: function (aFloat, elementId = null)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.Populate_And_FormatNumberToPercent: ';

        if (!elementId)
        {
            console.log(logPrefix + 'elementId is missing.');
            return;
        }


        // Get the element
        const element = document.getElementById(elementId);

        // Guard Clause
        if (!element)
        {
            console.log(`${logPrefix}Could not find an element with id of "${elementId}"`);
            return;
        }


        // Guard Clause
        if (isNaN(aFloat)) {
            element.value = '0 %';
            return;
        }

        const localeElement = document.getElementById('Locale');



        const locale = localeElement.value;

        // Guard Clause
        if (!locale)
        {
            console.log(logPrefix + 'localeElement does not contain a value.');
            return;
        }



        // Display
        element.value = aFloat.toFixed(2) + ' %';
    },


    /* Convert a number (n) to a string suffixed with ' hours/year'.
     * For Example:   33.3333 -> 33.33 hours/year */
    FormatToHoursPerYear: function (hours = null) {

        if (isNaN(hours))
            return '0.00 hours/year';

        if (hours === null)
            return '0.00 hours/year';

        hours = SilkFlo.Models2.Business.Idea.FormatNumberTo2DecimalPlaces(hours);


        let formatted = hours.toLocaleString();
        formatted = formatted + ' hours/year';

        return formatted;
    },

    FormatNumberTo2DecimalPlaces: function (n) {

        if (isNaN(n))
            return 0.00;

        n = Math.round((n + Number.EPSILON) * 100) / 100;

        return n;
    },

    GetFeasibilityGaugeComponent: function ()
    {
        const parentElementId = 'FormModal_BusinessIdea';
        const url = '/api/Business/Ideas/GetFeasibilityGaugeComponent';
        const id = 'FeasibilityGaugeComponent';
        SilkFlo.Models2.Business.Idea.GetGaugeComponent(parentElementId, url, id);
    },

    GetReadinessGaugeComponent: function ()
    {
        const parentElementId = 'FormModal_BusinessIdea';
        const url = '/api/Business/Ideas/GetReadinessGaugeComponent';
        const id = 'ReadinessGaugeComponent';
        SilkFlo.Models2.Business.Idea.GetGaugeComponent(parentElementId, url, id);
    },

    GetIdeaGaugeComponent: function ()
    {
        const parentElementId = 'FormModal_BusinessIdea';
        const url = '/api/Business/Ideas/GetIdeaGaugeComponent';
        const id = 'IdeaGaugeComponent';
        SilkFlo.Models2.Business.Idea.GetGaugeComponent(parentElementId, url, id);
    },

    // SilkFlo.Models2.Business.Idea.GetGaugeComponent
    GetGaugeComponent: function (parentElementId, url, id)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.GetGaugeComponent: ';

        const element = document.getElementById(parentElementId);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}Element with id ${id} missing`);
            return;
        }

        const model = SilkFlo.Models.Abstract.GetModelFromParent (
            element,
            [
                'RuleId',
                'InputId',
                'InputDataStructureId',
                'ProcessStabilityId',
                'DocumentationPresentId'
            ],
            'Business.Idea.'
        );

        if (!model)
            console.log(`${logPrefix}model missing`);

        if (!model.RuleId)
            model.RuleId = 'VeryCreative';

        if (!model.InputId)
            model.InputId = 'VeryMuchPaperBased';

        if (!model.InputDataStructureId)
            model.InputDataStructureId = 'VeryMuchUnstructured';

        if (!model.ProcessStabilityId)
            model.ProcessStabilityId = 'SignificantChange';

        if (!model.DocumentationPresentId)
            model.DocumentationPresentId = 'No';


        // 1. Set up async request;
        const json = JSON.stringify(model);

        const http = new XMLHttpRequest();
        http.open('POST', url, true);
        http.setRequestHeader('Content-type', 'application/json');
        http.onreadystatechange = function ()
        {
            return function ()
            {
                // 3. Receive response from sender
                if (http.readyState === XMLHttpRequest.DONE
                    && http.status === 200)
                {

                    const str = http.responseText;
                    const target = document.getElementById(id);

                    if (target)
                    {
                        target.innerHTML = str;
                        SilkFlo.SVGTools.AnimatePaths(target);
                        HotSpot.Get(target);
                    }
                }
            };
        }(this);

        // 2. Send
        http.send(json);
    },


    // SilkFlo.Models2.Business.Idea.FilterSummary
    FilterSummary: function (
        ids,
        targetId,
        url)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.FilterSummary';

        // Guard Clause
        if (!targetId) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        const target = document.getElementById(targetId);

        // Guard Clause
        if (!target) {
            console.log(`${logPrefix}Element with id ${targetId} missing`);
            return;
        }

        const json = JSON.stringify(ids);

        SilkFlo.DataAccess.UpdateElement (
            url,
            json,
            target,
            '',
            'POST',
            null,
            null,
            'application/json; charset=utf-8');
    },


    // SilkFlo.Models2.Business.Idea.ResetSummary
    ResetSummary: function (targetId)
    {
        const logPrefix = 'SilkFlo.Models2.Business.Idea.ResetSummary';

        // Guard Clause
        if (!targetId) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        const target = document.getElementById(targetId);


        // Guard Clause
        if (!target) {
            console.log(`${logPrefix}Element with id ${targetId} missing`);
            return;
        }


        SilkFlo.DataAccess.UpdateTargetElement ( target );
    }

};