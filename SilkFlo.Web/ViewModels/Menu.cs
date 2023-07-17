//using System.Collections.Generic;

//namespace SilkFlo.Web.ViewModels
//{
//    public class Menu
//    {
//        public static List<MenuItem> Create()
//        {
//            List<MenuItem> menu = new List<MenuItem>();

//            var menuItemExplore = new MenuItem("Explore", true, "/Explore/Ideas");
//            menuItemExplore.Menu.Add(new MenuItem("Ideas", true, "/Explore/Ideas"));
//            menuItemExplore.Menu.Add(new MenuItem("Improvements", false, "/Explore/Automations"));
//            menuItemExplore.Menu.Add(new MenuItem("People", false, "/Explore/People"));
//            menuItemExplore.Menu.Add(new MenuItem("Leaderboard", false, "/Explore/Leaderboard"));
//            menuItemExplore.Menu.Add(new MenuItem("Components", false, "/Explore/Components"));
//            menu.Add(menuItemExplore);

//            var menuItemWorkshop = new MenuItem("Workshop", false, "/Workshop");
//            menu.Add(menuItemWorkshop);

//            var menuDashboards = new MenuItem("Dashboards", false, "/Dashboards");
//            menu.Add(menuDashboards);

//            return menu;
//        }
//    }
//}
