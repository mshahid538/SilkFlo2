var HotSpot = {
    Card: null,
    CardPosition: null,
    AttributeName: {
        Name: 'silkflo-hotspot'
    },

    // HotSpot.Get
    Get: function (parent)
    {
        const logPrefix = 'SilkFlo.DataAccess.GetHotSpots: ';

        // Guard Clause
        if (!parent) {
            console.log(`${logPrefix}parent parameter missing`);
            return;
        }

        // Get the element with the Silkflo-url attribute
        const elements = parent.querySelectorAll(`[${HotSpot.AttributeName.Name}]`);

        if (elements.length === 0)
            return;

        for (let i = 0; i < elements.length; i++) {
            const element = elements[i];

            const value = element.getAttribute(HotSpot.AttributeName.Name);

            if (!value)
                continue;

            const http = new XMLHttpRequest();
            http.open(
                'GET',
                `/api/GetHotSpotContent/${value}`,
                true); // false for synchronous request

            http.setRequestHeader(
                'Content-type',
                'application/x-www-form-urlencoded');

            http.onreadystatechange = function () {
                return function () {
                    if (http.readyState === XMLHttpRequest.DONE) {

                        if (http.status === 200) {
                            const response = http.responseText;

                            if (response)
                                HotSpot.Add(
                                    element,
                                    response);
                        }
                    }
                };
            }(this);


            http.send();
        }
    },


    // HotSpot.Add
    Add: function (
             element,
             innerHtml)
    {
        const logPrefix = 'HotSpot.Add: ';

        // Guard Clause
        if (!element) {
            console.log(`${logPrefix}element parameter missing`);
            return;
        }

        // Create the card
        if (!HotSpot.Card)
        {
            const card = document.createElement ( 'div' );
            card.setAttribute (
                'style',
                'width: 20rem; display:none; position:absolute; ' );
            card.setAttribute (
                'class',
                'card HotSpotCard' );
            document.body.appendChild(card);


            const header = document.createElement('div');
            header.setAttribute(
                'class',
                'HotSpotHeader');

            card.appendChild(header);


            const button = document.createElement('button');
            button.setAttribute(
                'aria-label',
                'Cancel');
            button.setAttribute(
                'type',
                'button');
            button.setAttribute(
                'class',
                'btn-close');

            button.onclick = (e) => {
                HotSpot.Card.Close();
                e.preventDefault();
                e.stopPropagation();
            };

            header.appendChild(button);


            const content = document.createElement('div');

            content.setAttribute(
                'class',
                'HotSpotContent');

            content.innerHTML = innerHtml;
            card.Content = content;

            card.appendChild(content);
            card.Close = function () {
                this.style.transform = '';
                this.style.display = 'none';
            };

            HotSpot.Card = card;
        }



        const isBackgroundDark = HotSpot.IsBackgroundDark(element);

        const img = document.createElement('img');
        img.setAttribute(
            'src',
            '/icons/Question.svg');

        if (isBackgroundDark)
            img.setAttribute(
                'class',
                'QuestionIconLight');
        else
            img.setAttribute(
                'class',
                'QuestionIcon');

        const container = document.createElement('span');
        container.appendChild(img);
        container.Text = innerHtml;
        //container.setAttribute(
        //    'style',
        //    'position: relative;');


        container.onclick = (e) => {

            e.preventDefault();
            e.stopPropagation();

            if (HotSpot.Card.Content.innerHTML === container.Text && HotSpot.Card.style.display === '')
            {
                HotSpot.Card.Close();
                return;
            }

            HotSpot.Card.style.display = '';
            HotSpot.Position (
                e.target,
                container);
            HotSpot.Card.Content.innerHTML = container.Text;
        };




        element.appendChild(container);

        SilkFlo.ReplaceImgWithSVG(container);
    },

    // HotSpot.Position
    Position: function (
                  button,
                  container)
    {
        const logPrefix = 'HotSpot.Position: ';


        // Guard Clause
        if (!button) {
            console.log(`${logPrefix}button parameter missing`);
            return;
        }

        // Guard Clause
        if (!container) {
            console.log(`${logPrefix}container parameter missing`);
            return;
        }




        // Do the business
        const buttonRectangle = HotSpot.GetGeometry(button);

        if (!HotSpot.CardPosition)
            HotSpot.CardPosition = HotSpot.GetGeometry(HotSpot.Card);



        // Get delta X
        let deltaX = buttonRectangle.left - HotSpot.CardPosition.left - window.scrollX;



        const windowHWidth = window.innerWidth;
        if (HotSpot.CardPosition.width > windowHWidth)
            deltaX = 0;
        else if (deltaX + HotSpot.CardPosition.width > windowHWidth)
            deltaX = windowHWidth - HotSpot.CardPosition.width - 50;


        // Get delta Y
        let deltaY =
            (HotSpot.CardPosition.top - buttonRectangle.top - buttonRectangle.height - HotSpot.CardPosition.y - window.scrollY) * -1;


        //const windowHeight = window.innerHeight;
        //if (windowHeight - buttonRectangle.top - buttonRectangle.height - HotSpot.CardPosition.height < 0) {
        //    //Set menu top
        //    deltaY -= HotSpot.CardPosition.height;
        //}

        const matrix = `matrix( 1, 0, 0, 1, ${deltaX}, ${deltaY})`;
        HotSpot.Card.style.transform = matrix;
    },

    // HotSpot.GetGeometry
    GetGeometry: function (element)
    {
        if (!element) {
            const logPrefix = 'HotSpot.GetGeometry: ';
            console.log(`${logPrefix}element parameter missing`);
            return null;
        }

        return element.getBoundingClientRect();
    },

    // HotSpot.IsBackgroundDark
    IsBackgroundDark: function (element)
    {
        if (!element) {
            const logPrefix = 'HotSpot.IsBackgroundDark: ';
            console.log(`${logPrefix}element parameter missing`);
            return false;
        }

        const backgroundTransparent = 'rgba(0, 0, 0, 0)';
        let background = backgroundTransparent;
        let parent = element;

        while (background === backgroundTransparent)
        {
            background = window.getComputedStyle(parent, null).getPropertyValue('background-color');
            parent = parent.parentElement;
        }

        background = background.replace(
            'rgba(',
            '');

        background = background.replace(
            'rgb(',
            '');

        background = background.replace(
            ')',
            '');



        const lst = background.split(', ');

        let sum = 0;
        const length = lst.length;
        for (var i = 0; i < length; i++) {
            const number = parseInt(lst[i]);
            sum += number;
        }

        if (length === 3)
            return sum < 382;

        return sum < 510;
    }
}