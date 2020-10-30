using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;


namespace PruebaLDAP
{
    class Program
    {
        static void Main(string[] args)
        {
            string cadena = "uno,dos";

            string[] ele = cadena.Split(',');

            using (var context = new PrincipalContext(ContextType.Domain, "10.104.175.245", "OU=Users,OU=TLT,DC=nac,DC=ppg,DC=com"))
            {
                UserPrincipal userPrincipal = new UserPrincipal(context);
                userPrincipal.EmailAddress = "asolorzanotapia@ppg.com";

                using (var searcher = new PrincipalSearcher(userPrincipal))
                {
                    //var list = searcher.FindAll().Where(n => (n.GetUnderlyingObject() as DirectoryEntry).Properties["mail"].Value == "asolorzanotapia@ppg.com").FirstOrDefault();

                    List<UserPrincipal> ListUserLdap = searcher.FindAll().Cast<UserPrincipal>().ToList();

                    var sff = ListUserLdap.Where(n => n.EmailAddress.ToLower() == "asolorzanotapia@ppg.com").FirstOrDefault();

                    var we = ListUserLdap.Count();

                    UserPrincipal usss = ListUserLdap.FirstOrDefault() as UserPrincipal;

                    var dd =searcher.FindAll().Where(n => n.Name.Contains("Solorzano Tapia")).FirstOrDefault();

                   

                    var ww = dd.GetUnderlyingObject() as UserPrincipal;                                      

                    var ddd = dd.GetUnderlyingObject() as DirectoryEntry;

                    var qq = ddd.Properties["mail"];

                    var uno = searcher.FindAll().Where(n => (n.GetUnderlyingObject() as DirectoryEntry).Properties["mail"].Value != null && (n.GetUnderlyingObject() as DirectoryEntry).Properties["mail"].Value.ToString() == "asolorzanotapia@ppg.com").FirstOrDefault();
                    
                    foreach (var result in searcher.FindAll())
                    {
                        

                        DirectoryEntry de = result.GetUnderlyingObject() as DirectoryEntry;
                        PropertyCollection pro = de.Properties;
                        var dos = pro["EmailAddress"].Value;
                        DirectorySearcher search = new DirectorySearcher(de);
                        //search.Filter = "(&(sAMAccountName=*)(objectclass=user)(objectcategory=person))";

                        //SearchResultCollection sear = search.FindAll();

                        //CN = Solorzano Tapia\, Alan Michel[C], OU = Users, OU = TLT, DC = nac, DC = ppg, DC = com

                        Console.WriteLine("First Name: " + de.Properties["givenName"].Value);
                        Console.WriteLine("Last Name : " + de.Properties["sn"].Value);
                        Console.WriteLine("SAM account name   : " + de.Properties["samAccountName"].Value);
                        Console.WriteLine("User principal name: " + de.Properties["userPrincipalName"].Value);
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}
