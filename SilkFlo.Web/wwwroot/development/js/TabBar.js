if (!SilkFlo)
    SilkFlo = {};


SilkFlo.TabBar = {

    // SilkFlo.TabBar.Setup
    SetUp: function (parent)
    {
        // Guard Clause
        if (!parent) {
            const logPrefix = 'SilkFlo.TabBar.Setup: ';
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        const tabBars = parent.querySelectorAll(`.silkflo-tab`);

        const length = tabBars.length;
        for (let i = 0; i < length; i++)
        {
            const tabBar = tabBars[i];
            const children = tabBar.querySelectorAll('.silkflo-tab-label');

            const length2 = children.length;
            for (let j = 0; j < length2; j++)
            {
                const tab = children[j];
                SilkFlo.TabBar.GetAttributes(tab);
            }
        }
    },


    // SilkFlo.TabBar.GetAttributes
    GetAttributes: function (element)
    {
        const logPrefix = 'SilkFlo.TabBar.GetAttributes: ';

        //console.log(location.hostname);

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return null;
        }


        if (element.SilkFlo)
            return element.SilkFlo;


        const deleteAttributes =
            SilkFlo.DataAccess.ProductionHostName.toLowerCase() === location.hostname.toLowerCase();


        // Get the attributes
        let attributeName = 'name';
        const name = element.getAttribute(attributeName);

        // Guard Clause
        if (!name)
            return null;
        

        if (deleteAttributes)
            element.removeAttribute(attributeName);



        attributeName = 'parentid';
        const parentId = element.getAttribute(attributeName);

        // Guard Clause
        if (!parentId) {
            console.log(`${logPrefix}${attributeName} attribute missing`);
            console.log(element);
            return null;
        }

        if (deleteAttributes)
            element.removeAttribute(attributeName);



        attributeName = 'getOnSelect';
        const getOnSelect = element.hasAttribute(attributeName);
        if (deleteAttributes)
            element.removeAttribute(attributeName);

        attributeName = 'silkflo-loadOnce';
        const loadOnce = element.hasAttribute(attributeName);
        if (deleteAttributes)
            element.removeAttribute(attributeName);

        attributeName = 'silkflo-getManually';
        const getManually = element.hasAttribute(attributeName);
        if (deleteAttributes)
            element.removeAttribute(attributeName);

        attributeName = 'displayPath';
        const path = element.getAttribute(attributeName);

        // Guard Clause
        if (!path) {
            console.log(`${logPrefix}${attributeName} attribute missing`);
            console.log(element);
            return null;
        }

        if (deleteAttributes)
            element.removeAttribute(attributeName);



        element.addEventListener (
            'click',
            SilkFlo.TabBar.Selector );

        element.SilkFlo = {
            Name: name,
            ParentId: parentId,
            GetOnSelect: getOnSelect,
            Path: path,
            LoadOnce: loadOnce,
            GetManually: getManually
        }


        return element.SilkFlo;
    },


    // SilkFlo.TabBar.Selector
    Selector: function (event)
    {
        const logPrefix = 'SilkFlo.TabBar.Selector: ';

        // Guard Clause
        if (!event) {
            console.log(`${logPrefix}event parameter missing`);
            return;
        }

        if (HotSpot.Card)
            HotSpot.Card.Close();

        const element = event.target;
        SilkFlo.TabBar.SelectorByElement ( element );
    },

    /* Usage:
     * Tab element attributes:
     *  - name: Must be the same name as the matching content element, but suffixed with '.Tab'
     *          For example:
     *          name="Settings.PlatformSetup.BusinessUnits.Tab"
     *
     *  - displayPath: The relative URL path.
     *                 For example: /Settings/tenant/Platform-Setup/Business-Units
     *
     *  - parentId: The id of the parent element containing the tab bar and tab contents.
     *              For example: Settings.PlatformSetup
     *
     *  - onclick: Reference to this function.
     *             For example: SilkFlo.TabBar.Selector(this);
     *
     *  Example:
     *
     * <div class="silkflo-tab">
     *     <h3 class="silkflo-tab-label active"
     *         name="Settings.PlatformSetup.BusinessUnits.Tab"
     *         displayPath="Settings/tenant/Platform-Setup/Business-Units"
     *         parentId="Settings.PlatformSetup"
     *         onclick="SilkFlo.TabBar.Selector(this)">
     *         Business Units
     *     </h3>
     * </div>
     *
     * Tab content elements must have the same name as their corresponding tab element,
     * but suffixed with '.Content' as opposed to '.Tab'.
     *
     * For Example:
     *  <div name="Settings.PlatformSetup.BusinessUnits.Content">Loading Content</div>
     *
     * All Tab content elements must be in a parent div with the name 'container'.
     *
     * For Example:
     * <div name="container">
     *     <div name="Settings.PlatformSetup.BusinessUnits.Content">Loading Content</div>
     * </div>
     */

    // SilkFlo.TabBar.SelectorByElement
    SelectorByElement: function (element)
    {
        const logPrefix = 'SilkFlo.TabBar.SelectorByElement: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        let attributes = SilkFlo.TabBar.GetAttributes ( element );
        if (!attributes)
            return;



        const parent = document.getElementById( attributes.ParentId );

        // Guard Clause
        if (!parent)
        {
            console.log(`${logPrefix}Element with id ${attributes.ParentId} missing` );
            return;
        }


        let name = 'container';
        const container = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!container)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }



        if (attributes.Path)
            window.history.pushState (
                '',
                location.hostname,
                `/${attributes.Path}` );


        name = attributes.Name.replace (
            '.Tab',
            '.Content' );
        const elementContent = parent.querySelector ( `[name="${name}"]` );

        // Guard Clause
        if (!elementContent)
        {
            console.log ( `${logPrefix}Element with name ${name} missing` );
            return;
        }


        if (elementContent.hasAttribute ( SilkFlo.DataAccess.AttributeName.GetManually ))
            SilkFlo.DataAccess.UpdateTargetElement ( elementContent );

        // Do the business
        const children = container.children;
        const length = children.length;

        for (let i = 0; i < length; i++)
        {
            const child = children[i];
            child.style.display = 'none';
            child.setAttribute ( SilkFlo.DataAccess.AttributeName.GetManually, '' );
        }

        SilkFlo.SVGTools.AnimatePaths ( elementContent );
        elementContent.style.display = 'block';


        SilkFlo.TabBar.SetActive ( element );
    },


    // Apply the 'active' CSS class to the correct tab element

    //SilkFlo.TabBar.SetActive
    SetActive: function (selectedElement) {
        // Guard Clause
        if (!selectedElement) {
            const logPrefix = 'SilkFlo.TabBar.SetActive: ';
            console.log(logPrefix + 'selectedElement parameter missing');
            return;
        }

        const parentElement = selectedElement.parentElement;

        const children = parentElement.children;

        const length = children.length;
        for (let i = 0; i < length; i++) {
            const element = children[i];
            element.classList.remove('active');
        }

        selectedElement.classList.add('active');
    }
};