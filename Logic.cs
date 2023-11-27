using Microsoft.SharePoint.Client;

namespace WPFClient
{
    public class Logic
    {
        // Set some constants 
        private const string SITE = "http://mySite";
        private const string LIST_NAME = "SPUG_Demo_Contacts";

        /// <summary>
        /// Get list items from the list
        /// </summary>
        /// <returns>ListItemCollection</returns>
        public static ListItemCollection GetList()
        {
            using(ClientContext ctx = new ClientContext(SITE))
            {
                Web web = ctx.Web;
                List list = web.Lists.GetByTitle(LIST_NAME);
                CamlQuery query = new CamlQuery();
                query.ViewXml = "<View>" +
                                    "<Query>" +
                                        "<OrderBy>" +
                                            "<FieldRef Name='Title'/>" +
                                            "<FieldRef Name='FirstName'/>" +
                                        "</OrderBy>" +
                                    "</Query>" +
                                    "<ViewFields>" +
                                        "<FieldRef Name='ID'/>" +
                                        "<FieldRef Name='Title'/>" +
                                        "<FieldRef Name='FirstName'/>" +
                                        "<FieldRef Name='WorkAddress'/>" +
                                        "<FieldRef Name='WorkCity'/>" +
                                        "<FieldRef Name='WorkState'/>" +
                                        "<FieldRef Name='WorkZip'/>" +
                                    "</ViewFields>" +
                                "</View>";

                ListItemCollection listItems = list.GetItems(query);
                ctx.Load(listItems);
                ctx.ExecuteQuery();

                return listItems;
            }
        }

        /// <summary>
        /// Update item matching given ID
        /// </summary>
        /// <param name="id">ID of item to update</param>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="postalCode"></param>
        public static void Update(int id, string firstName, string lastName, string address,
            string city, string state, string postalCode)
        {
            using(ClientContext ctx = new ClientContext(SITE))
            {
                Web web = ctx.Web;
                List list = web.Lists.GetByTitle(LIST_NAME);

                // Get the item being updated
                ListItem item = list.GetItemById(id);

                // Update the FieldValues
                item["Title"] = lastName;
                item["FirstName"] = firstName;
                item["WorkAddress"] = address;
                item["WorkCity"] = city;
                item["WorkState"] = state;
                item["WorkZip"] = postalCode;

                // Must make sure to call this
                item.Update();

                // Commit the change
                ctx.ExecuteQuery();
            }
        }

        /// <summary>
        /// Add a new contact
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="city"></param>
        /// <param name="state"></param>
        /// <param name="postalCode"></param>
        public static void AddContact(string firstName, string lastName, string address,
            string city, string state, string postalCode)
        {
            using(ClientContext ctx = new ClientContext(SITE))
            {
                Web web = ctx.Web;
                List list = web.Lists.GetByTitle(LIST_NAME);

                // Create the Listitem
                // ListItemCreationInformation can bee null for root folder
                ListItemCreationInformation createInfo = null;

                // Or for adding item to a folder
                //ListItemCreationInformation createInfo = new ListItemCreationInformation();
                //createInfo.FolderUrl = "site/lists/listname/folder";

                ListItem item = list.AddItem(createInfo);

                // Set the FieldValues
                item["Title"] = lastName;
                item["FirstName"] = firstName;
                item["WorkAddress"] = address;
                item["WorkCity"] = city;
                item["WorkState"] = state;
                item["WorkZip"] = postalCode;

                // Save changes
                item.Update();

                // Commit
                ctx.ExecuteQuery();
            }
        }

        /// <summary>
        /// Delete the contact matching given ID
        /// </summary>
        /// <param name="id">ID of contact to delete</param>
        public static void Delete(int id)
        {
            using(ClientContext ctx = new ClientContext(SITE))
            {
                Web web = ctx.Web;
                List list = web.Lists.GetByTitle(LIST_NAME);

                // Get the item being deleted
                ListItem item = list.GetItemById(id);
                item.DeleteObject();

                // Commit
                ctx.ExecuteQuery();
            }
        }
    }
}
