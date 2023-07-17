SilkFlo.Analytics = {

    AttributeName: {
        Action: 'silkflo-action',
        NoLogging: 'NoLogging'
    },


    // SilkFlo.Analytics.AssignClickEvents
    AssignClickEvents: function (parent)
    {
        // Guard Clause
        if (!parent)
        {
            const logPrefix = 'SilkFlo.Analytics.AssignClickEvents: ';
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        // Get the element with the silkflo-action attribute
        const query = `[${SilkFlo.Analytics.AttributeName.Action}]`;
        const elements = parent.querySelectorAll(query);


        const length = elements.length;
        if (length === 0) {
            return;
        }


        for (let i = 0; i < length; i++)
        {
            const element = elements[i];
            const action = element.getAttribute(SilkFlo.Analytics.AttributeName.Action);

            SilkFlo.Analytics.AssignClickEvent(
                element,
                action);
        }
    },

    AssignClickEvent: function (element, action)
    {
        const logPrefix = 'SilkFlo.Analytics.AssignClickEvent: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }


        // Guard Clause
        if (!action) {
            console.log(`${logPrefix + SilkFlo.DataAccess.AttributeName.Action} is missing for element ${element}.`);
            return;
        }

        element.SilkFloAction = action;
        element.removeAttribute (SilkFlo.Analytics.AttributeName.Action);
        element.addEventListener(
            'click',
            SilkFlo.Analytics.Click);
    },

    // SilkFlo.Analytics.Click
    Click: function (event)
    {
        const element = event.target;
        const action = SilkFlo.Analytics.GetAction (element);

        SilkFlo.Analytics.PostURL ( action );
    },

    GetAction: function (element)
    {
        // Guard Clause
        if (!element)
        {
            const logPrefix = 'SilkFlo.Analytics.GetAction: ';
            console.log(`${logPrefix}element parameter missing`);
            return '';
        }


        if (element.SilkFloAction)
        {
            return element.SilkFloAction;
        }

        if (element.parentElement)
        {
            return SilkFlo.Analytics.GetAction ( element.parentElement );
        }

        return '';
    },

    GetLanguage: function () {

        return (navigator.languages && navigator.languages.length) ? navigator.languages[0] : navigator.language;
    },

    //SilkFlo.Analytics.PostURL
    PostURL: function (action) {

        //https://www.w3schools.com/jsref/tryit.asp?filename=tryjsref_nav_all

        if (SilkFlo.CheckCookie(SilkFlo.Analytics.AttributeName.NoLogging))
            return;


        if (!action)
            action = '';

        const date = new Date();
        const dateTime = date.getFullYear() + '-' + (date.getMonth() + 1) + '-' + date.getDate() + 'T' + date.getTime() + 'Z';
        document.cookie = `DateTime =${dateTime}`;
        const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;
        const language = this.GetLanguage();
        const platform = navigator.platform;
        const userAgent = navigator.userAgent;


        // Get or create a user tracker
        const userTrackerName = 'UserTracker';
        let userTracker = SilkFlo.GetCookie(userTrackerName);
        if (userTracker.length === 0) {
            userTracker = this.GUID();
            document.cookie = `${userTrackerName}=${userTracker}; expires=Fri, 31 Dec 9999 23:59:59 GMT`;
        }

        // Get or create a session tracker
        const sessionTrackerName = 'SessionTracker';
        let sessionTracker = SilkFlo.GetCookie(sessionTrackerName);
        if (sessionTracker.length === 0) {
            sessionTracker = this.GUID();
            document.cookie = sessionTrackerName + '=' + sessionTracker;
        }

        const model = {
            DateTime: dateTime,
            URL: window.location.href,
            Action: action,
            UserTracker: userTracker,
            SessionTracker: sessionTracker,
            TimeZone: timeZone,
            Language: language,
            Platform: platform,
            UserAgent: userAgent
        };


        const json = JSON.stringify(model);

        const url = '/api/analytic/post';
        const http = new XMLHttpRequest();
        http.open(
            'POST',
            url,
            true);
        http.setRequestHeader('Content-type', 'application/json');
        http.send(json);
    },

    Clear: function () {

        if (confirm('Are you want to delete all the analytics?')) {

            const url = '/api/analytic/clearAll';

            const http = new XMLHttpRequest();
            http.open('DELETE', url, true);
            http.setRequestHeader('Content-type', 'application/x-www-form-urlencoded');
            http.onreadystatechange = function () {
                return function () {
                    if (http.readyState === XMLHttpRequest.DONE
                        && http.status === 200) {

                        window.location.href = '../Administration/analytic';
                    }
                };
            }(this);
            http.send();
        }
    },

    // Pretty good GUID creator, but not cryptographic quality
    GUID: function () {

        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            let r = Math.random() * 16 | 0, v = c === 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    },
};