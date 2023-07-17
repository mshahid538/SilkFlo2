using System.Collections.Generic;
using System.Linq;

namespace SilkFlo.Web.ViewModels.Elements.ToolBar
{
    public class Bar
    {
        private readonly List<Button> _buttons = new();

        public Bar(int buttonCount = 5)
        {
            if (buttonCount < 5)
                buttonCount = 5;



            // 0 Edit
            _buttons.Add(new()
            {
                Label = "Edit",
                Class = "btn btn-primary hide",
                Sort = 0
            });

            // 1 Save
            _buttons.Add(new()
            {
                Label = "Save",
                Class = "btn btn-primary hide",
                Sort = 1
            });

            // 2 Delete
            _buttons.Add(new()
            {
                Label = "Delete",
                Class = "btn btn-danger hide",
                Sort = 2
            });

            // 3 Cancel
            _buttons.Add(new()
            {
                Label = "Cancel",
                Class = "btn btn-warning hide",
                Sort = 3
            });

            // 4 New
            _buttons.Add(new()
            {
                Label = "New",
                Class = "btn btn-success",
                Sort = 4
            });
            


            buttonCount -= 5;

            for (var i = 0; i < buttonCount; i++)
            {
                _buttons.Add(new Button
                {
                    Label = "Custom" + i,
                    Class = "btn btn-primary",
                    Sort = (buttonCount - i) * -1
                });
            }


            Buttons = _buttons.OrderBy(x => x.Sort)
                             .ToArray();
        }

        public Button[] Buttons { get; set; }

        public string Title { get; set; }

        #region Search
        public string SearchOnKeyUp { get; set; }
        public string SearchMaxWidth { get; set; } = "400px";
        public string SearchId { get; set; }
        public string SearchName { get; set; }
        public string SearchPlaceHolder { get; set; } = "Search Here ...";
        #endregion


        #region Edit
        //public string EditLabel => Buttons[0].Label;
        public string EditLabel
        {
            get => _buttons[0].Label;
            set => _buttons[0].Label = value;
        }

        public string EditOnClick
        {
            get => _buttons[0].OnClick;
            set => _buttons[0].OnClick = value;
        }

        public string EditName
        {
            get => _buttons[0].Name;
            set => _buttons[0].Name = value;
        }

        public string EditId
        {
            get => _buttons[0].Id;
            set => _buttons[0].Id = value;
        }

        public string EditModalId
        {
            get => _buttons[0].ModalId;
            set => _buttons[0].ModalId = value;
        }

        public string EditClass
        {
            get => _buttons[0].Class;
            set => _buttons[0].Class = value;
        }
        #endregion


        #region Save
        public string SaveLabel
        {
            get => _buttons[1].Label;
            set => _buttons[1].Label = value;
        }

        public string SaveOnClick
        {
            get => _buttons[1].OnClick;
            set => _buttons[1].OnClick = value;
        }

        public string SaveName
        {
            get => _buttons[1].Name;
            set => _buttons[1].Name = value;
        }

        public string SaveId
        {
            get => _buttons[1].Id;
            set => _buttons[1].Id = value;
        }

        public string SaveModalId
        {
            get => _buttons[1].ModalId;
            set => _buttons[1].ModalId = value;
        }

        public string SaveClass
        {
            get => _buttons[1].Class;
            set => _buttons[1].Class = value;
        }
        #endregion


        #region Delete
        public string DeleteLabel
        {
            get => _buttons[2].Label;
            set => _buttons[2].Label = value;
        }

        public string DeleteOnClick
        {
            get => _buttons[2].OnClick;
            set => _buttons[2].OnClick = value;
        }

        public string DeleteName
        {
            get => _buttons[2].Name;
            set => _buttons[2].Name = value;
        }

        public string DeleteId
        {
            get => _buttons[2].Id;
            set => _buttons[2].Id = value;
        }

        public string DeleteModalId
        {
            get => _buttons[2].ModalId;
            set => _buttons[2].ModalId = value;
        }

        public string DeleteClass
        {
            get => _buttons[2].Class;
            set => _buttons[2].Class = value;
        }
        #endregion


        #region Cancel
        public string CancelLabel
        {
            get => _buttons[3].Label;
            set => _buttons[3].Label = value;
        }

        public string CancelOnClick
        {
            get => _buttons[3].OnClick;
            set => _buttons[3].OnClick = value;
        }

        public string CancelName
        {
            get => _buttons[3].Name;
            set => _buttons[3].Name = value;
        }

        public string CancelId
        {
            get => _buttons[3].Id;
            set => _buttons[3].Id = value;
        }

        public string CancelModalId
        {
            get => _buttons[3].ModalId;
            set => _buttons[3].ModalId = value;
        }

        public string CancelClass
        {
            get => _buttons[3].Class;
            set => _buttons[3].Class = value;
        }
        #endregion


        #region New
        public string NewLabel
        {
            get => _buttons[4].Label;
            set => _buttons[4].Label = value;
        }

        public string NewOnClick
        {
            get => _buttons[4].OnClick;
            set => _buttons[4].OnClick = value;
        }

        public string NewName
        {
            get => _buttons[4].Name;
            set => _buttons[4].Name = value;
        }

        public string NewId
        {
            get => _buttons[4].Id;
            set => _buttons[4].Id = value;
        }

        public string NewModalId
        {
            get => _buttons[4].ModalId;
            set => _buttons[4].ModalId = value;
        }

        public string NewClass
        {
            get => _buttons[4].Class;
            set => _buttons[4].Class = value;
        }
        #endregion

        public Element Message { get; } = new();
    }
}