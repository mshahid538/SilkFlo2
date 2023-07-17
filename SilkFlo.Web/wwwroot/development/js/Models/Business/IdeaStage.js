if (!SilkFlo)
    SilkFlo = {};

if (!SilkFlo.Models)
    SilkFlo.Models = {};

if (!SilkFlo.Models.Business)
    SilkFlo.Models.Business = {};

SilkFlo.Models.Business.IdeaStage = {
    /* GetModelFromParentById() Overview
     * ---------------------------------
     * This function will return a model of the Business.IdeaStage
     * based on the value attributes of named elements in in a parent element.
     *
     *
     * Parameters
     * ----------
     * parentElementId - The element id for the element containing the elements representing the model.
     * names           - A string collection of field/property names that we want to collect from the UI.
     * fieldNamePrefix - This is defaulted to 'Business.IdeaStage.',
     *                   but can be overridden.
     *                   All fields assigned to the model must have its name attribute prefixed.
     *   For example:
     *      Business.IdeaStage.Id
     *      Business.IdeaStage.Name
     *
     * This function will also:
     *  - Attach the model object to the parent element using the name Model.
     *        i.e. parentElement.Model.
     *  - Attach a collection called ModelElements to the parent element. This will contain a list of element holding the field data.
     *        i.e. parentElement.ModelElements
     *
     * Return Value
     * ------------
     * The function will return a JavaScript object consisting of fields that match the names in the names parameter collection.
     */
    GetModelFromParentById: function (parentElementId,
                                      names = [
                                               'Id',
                                               'DateEnd',
                                               'DateEndEstimate',
                                               'DateStart',
                                               'DateStartEstimate',
                                               'IdeaId',
                                               'IsInWorkFlow',
                                               'StageId',
                                               'IsSelected',
                                               'ReturnURL'
                                         ],
                                         fieldNamePrefix = 'Business.IdeaStage.')
    {
        const logPrefix = 'SilkFlo.Models.Business.IdeaStage.GetModelFromParentById: ';

        // Guard Clause
        if (!parentElementId)
        {
            console.log
                (`${logPrefix}parentElementId parameter missing`);
            return null;
        }

        // Guard Clause
        if (!names)
        {
            console.log
                (`${logPrefix}names parameter missing`);
            return null;
        }

        const parentElement = document.getElementById
            (parentElementId);

        // Guard Clause
        if (!parentElement)
        {
            console.log
                (`${logPrefix}parentElementId with value "${parentElementId}" parameter does not produce an element`); 
        }

        return SilkFlo.Models.Abstract.GetModelFromParent
                    (parentElement,
                     names,
                     fieldNamePrefix);
    },


    /* GetModelFromParent() Overview
     * -----------------------------
     * This function will return a model of the Business.IdeaStage
     * based on the value attributes of named elements in in a parent element.
     * Parameters
     * ----------
     * parentElement   - The element containing the elements representing the model.
     * names           - A string collection of field/property names that we want to collect from the UI.
     * fieldNamePrefix - This is defaulted to 'Business.IdeaStage.',
     *                   but can be overridden.
     *                   All fields assigned to the model must have its name attribute prefixed.
     *   For example:
     *      Business.IdeaStage.Id
     *      Business.IdeaStage.Name
     *
     * This function will also:
     *  - Attach the model object to the parent element using the name Model.
     *        i.e. parentElement.Model.
     *  - Attach a collection called ModelElements to the parent element. This will contain a list of element holding the field data.
     *        i.e. parentElement.ModelElements
     *
     * Return Value
     * ------------
     * The function will return a JavaScript object consisting of fields that match the names in the names parameter collection.
     */
    GetModelFromParent: function (parentElement,
                                  names = [
                                           'Id',
                                           'DateEnd',
                                           'DateEndEstimate',
                                           'DateStart',
                                           'DateStartEstimate',
                                           'IdeaId',
                                           'IsInWorkFlow',
                                           'StageId',
                                           'IsSelected',
                                           'ReturnURL'],
                                  fieldNamePrefix = 'Business.IdeaStage.',
                                  logErrors)
    {
        const logPrefix = 'SilkFlo.Models.Business.IdeaStage.GetModelFromParent: ';

        // Guard Clause
        if (!parentElement)
        {
            console.log
                (`${logPrefix}parentElement parameter missing`);
            return null;
        }

        // Guard Clause
        if (!names)
        {
            console.log
                (`${logPrefix}names parameter missing`);
            return null;
        }


        return SilkFlo.Models.Abstract.GetModelFromParent
                    (parentElement,
                     names,
                     fieldNamePrefix,
                     logErrors);
    },


    /* GetModelCollectionFromParentById() Overview
     * -------------------------------------------
     * Get a collection of models from within a supplied parent element with the supplied id.
     *
     *
     * Parameters
     * ----------
     * parentElementId   - Id for the parent element.
     * modelName         - The name of the element containing the models field elements. Default = 'Business.IdeaStage'.
     *                     This value appears in the name attribute of the element containing the models field elements.
     * names           - A string collection of field/property names that we want to collect from the UI.
     * fieldNamePrefix   - The prefix of the fully field name for each field element in the model element. 
     *                     Default = 'Business.IdeaStage.'
     *                     Example: Business.IdeaStage.Id.
     *
     *
     * Return Value
     * ------------
     * A collection of the Business.IdeaStage.
     */
    GetModelCollectionFromParentById: function (parentElementId,
                                                modelName = 'Business.IdeaStage',
                                                names = [
                                                          'Id',
                                                          'DateEnd',
                                                          'DateEndEstimate',
                                                          'DateStart',
                                                          'DateStartEstimate',
                                                          'IdeaId',
                                                          'IsInWorkFlow',
                                                          'StageId',
                                                          'IsSelected',
                                                          'ReturnURL'],
                                                fieldNamePrefix = 'Business.IdeaStage.')
    {
        const logPrefix = 'SilkFlo.Models.Business.IdeaStage.GetModelCollectionFromParentById: ';

        // Guard Clause
        if (!parentElementId)
        {
            console.log
                (`${logPrefix}parentElementId parameter missing`);
            return null;
        }

        // Guard Clause
        if (!modelName)
        {
            console.log
                (`${logPrefix}modelName parameter missing`);
            return null;
        }

        // Guard Clause
        if (!names)
        {
            console.log
                (`${logPrefix}names parameter missing`);
            return null;
        }

        const parentElement = document.getElementById(parentElementId);

        // Guard Clause
        if (!parentElement)
        {
            console.log
                (`${logPrefix}parentElementId with value "${parentElementId}" does not produce an element.`);
            return null;
        }

        return SilkFlo.Models.Abstract.GetModelCollectionFromParent (
            parentElement,
            modelName,
            names,
            fieldNamePrefix);
    },


    /* GetModelCollectionFromParent() Overview
     * ---------------------------------------
     * Get a collection of models from within a supplied parent element.
     *
     *
     * Parameters
     * ----------
     * parentElement   - The parent element.
     * modelName       - The name of the element containing the models field elements. Default = 'Business.IdeaStage'.
     *                   This value appears in the name attribute of the element containing the models field elements.
     * names           - A string collection of field/property names that we want to collect from the UI.
     * fieldNamePrefix - The prefix of the fully field name for each field element in the model element. 
     *                   Default = 'Business.IdeaStage.'
     *                   Example: Business.IdeaStage.Id.
     *
     *
     * Return Value
     * ------------
     * A collection of the Business.IdeaStage.
     */
    GetModelCollectionFromParent: function (parentElement,
                                            modelName = 'Business.IdeaStage',
                                            names = [
                                                     'Id',
                                                     'DateEnd',
                                                     'DateEndEstimate',
                                                     'DateStart',
                                                     'DateStartEstimate',
                                                     'IdeaId',
                                                     'IsInWorkFlow',
                                                     'StageId',
                                                     'IsSelected',
                                                     'ReturnURL'],
                                            fieldNamePrefix = 'Business.IdeaStage.')
    {
        const logPrefix = 'SilkFlo.Models.Business.IdeaStage.GetModelCollectionFromParent: ';

        // Guard Clause
        if (!parentElement)
        {
            console.log
                (`${logPrefix}parentElement parameter missing`);
            return null;
        }

        // Guard Clause
        if (!modelName)
        {
            console.log
                (`${logPrefix}modelName parameter missing`);
            return null;
        }

        // Guard Clause
        if (!names)
        {
            console.log
                (`${logPrefix}names parameter missing`);
            return null;
        }


        return SilkFlo.Models.Abstract.GetModelCollectionFromParent(parentElement,
                                            modelName,
                                            names,
                                            fieldNamePrefix);
    },



    /* Save() Overview
     * ---------------------------------
     * Save the supplied model back to the server.
     *
     *
     * Parameters
     * ----------
     * model                - The Business/IdeaStage model.
     * callbackStatus200   - [optional] This function is ran if the response is Ok.
     * callbackStatusOther - [optional] This function is ran if the response is not Ok.
     * targetElementId      - [optional] Supply an id for the element that will be updated 
     * postURL              - The post URL
     * verb                 - POST or PUT
     *
     *
     * Example Server C# Method
     * -------------------
     * [HttpPost("/api/Models/Business/IdeaStage/Post")]
     * public async Task<IActionResult> Post([FromBody] Models.Business.IdeaStage model)
     * {
     *     // Stay awesome!
     * }
     */
    Save: function (
        model,
        callbackStatus200 = null,
        callbackStatusOther = null,
        targetElementId = null,
        url = '/api/Business/IdeaStage/Post',
        verb = SilkFlo.Models.Abstract.Verb.POST)
    {
        const logPrefix = 'SilkFlo.Models.Business.IdeaStage.Save: ';

  
        if (!model)
        {
            console.log
                (`${logPrefix}model parameter missing`);
            return;
        }


        SilkFlo.Models.Abstract.Save (
            model,
            callbackStatus200,
            callbackStatusOther,
            targetElementId,
            url,
            verb);
    },

    /* Delete() Overview
     * ---------------------------------
     * Delete item with supplied id.
     * A confirmation message box will be displayed prior to deletion request.
     *
     *
     * Parameters
     * ----------
     * callbackStatus200   - [optional] This function is ran if the response is Ok.
     * callbackStatusOther - [optional] This function is ran if the response is not Ok.
     * id                  - The primary key for the item to be deleted.
     * url                 - The URL. For example ... delete/id/{id}
     * name                - Name of the item to be deleted.
     *                       This will appear in the confirmation message.
     *                       Default = Idea Stage
     * additionalMessage   - [optional] Any additional message to appear in the confirmation dialogue.
     * parentId            - [optional] The id of the parent element.
     *
     *
     * Example Server C# Method
     * -------------------
     * [HttpDelete("/api/Models/Business/IdeaStage/Delete/Id/{id}")]
     * public async Task<IActionResult> Delete(string id)
     * {
     *     // Stay awesome!
     * }
     */
    Delete: function (
        callbackStatus200,
        callbackStatusOther,
        id,
        name = 'Idea Stage',
        additionalMessage = '',
        url = '/api/Models/Business/IdeaStage/Delete/Id/',
        parentId)
    {
        const logPrefix = 'SilkFlo.Models.Business.IdeaStage.Delete: ';

  
        // Guard Clause
        if (!id)
        {
            console.log
                (`${logPrefix}id parameter missing`);
            return;
        }


        // Guard Clause
        if (!name) {
            console.log(`${logPrefix}name parameter missing`);
            return;
        }

        url += id;

        SilkFlo.Models.Abstract.Delete (
            url,
            name,
            additionalMessage,
            callbackStatus200,
            callbackStatusOther,
            parentId);
    }
};