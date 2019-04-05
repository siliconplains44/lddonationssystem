using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class CustomDataAccess : ICustomDataAccess
    {
        private DbContext _context;
        public CustomDataAccess(DbContext context)            
        {
            _context = context;
        }

        public bool RegisterUserAccountByRegistrationId(string registrationId)
        {
            var ret = false;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT COUNT(*) FROM Users WHERE RegistrationCode = @RegistrationCode";
                var RegistrationCodeParam = command.CreateParameter();
                RegistrationCodeParam.ParameterName = "@RegistrationCode";
                RegistrationCodeParam.Value = registrationId;
                command.Parameters.Add(RegistrationCodeParam);
                var countFound = Convert.ToInt64(command.ExecuteScalar());
                
                if (countFound > 0)
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE Users SET IsActive = 1 WHERE RegistrationCode = @RegistrationCode";
                    command.Parameters.Clear();                    
                    RegistrationCodeParam.ParameterName = "@RegistrationCode";
                    RegistrationCodeParam.Value = registrationId;
                    command.Parameters.Add(RegistrationCodeParam);
                    if (1 == command.ExecuteNonQuery())
                    {
                        ret = true;
                    }
                }
            }

            return ret;
        }

        public bool CreateUserAccount(string email, string password, AccountType accountType, string registrationId)
        {
            var ret = false;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Users WHERE Email = @Email";
                var Emailparam = command.CreateParameter();
                Emailparam.ParameterName = "@Email";
                Emailparam.Value = email;
                command.Parameters.Add(Emailparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ret = false;
                    return ret;
                }                
            }

            long userId = -1;

            var unitOfWork = _context.CreateUnitOfWork();

            using (var command = _context.CreateCommand())
            {
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO users(Email, Password, IsActive, RegistrationCode, PasswordResetCode, AccountTypeID) VALUES (@Email, @Password, @IsActive, @RegistrationCode, @PasswordResetCode, @AccountTypeID)";
                var Emailparam = command.CreateParameter();
                Emailparam.ParameterName = "@Email";
                Emailparam.Value = email;
                command.Parameters.Add(Emailparam);
                var Passwordparam = command.CreateParameter();
                Passwordparam.ParameterName = "@Password";
                Passwordparam.Value = password;
                command.Parameters.Add(Passwordparam);
                var IsActiveparam = command.CreateParameter();
                IsActiveparam.ParameterName = "@IsActive";
                IsActiveparam.Value = 0;
                command.Parameters.Add(IsActiveparam);
                var RegistrationCodeparam = command.CreateParameter();
                RegistrationCodeparam.ParameterName = "@RegistrationCode";
                RegistrationCodeparam.Value = registrationId;
                command.Parameters.Add(RegistrationCodeparam);
                var PasswordResetCodeparam = command.CreateParameter();
                PasswordResetCodeparam.ParameterName = "@PasswordResetCode";
                PasswordResetCodeparam.Value = "";
                command.Parameters.Add(PasswordResetCodeparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = Convert.ToInt32(accountType);
                command.Parameters.Add(AccountTypeIDparam);
                if (0 < command.ExecuteNonQuery())
                {
                    command.CommandText = "SELECT LAST_INSERT_ID(); ";
                    userId = Convert.ToInt64(command.ExecuteScalar());
                }
            }

            var repoind = new individualsRepository(_context);
            var newindividual = new individuals();
            var indid = repoind.Create(newindividual);

            switch(accountType)
            {
                case AccountType.Administrator:
                    {
                        var repo = new adminsRepository(_context);
                        var newadmin = new admins();
                        newadmin.UserID = userId;
                        newadmin.IndividualID = indid;
                        newadmin.IsDeleted = 0;
                        repo.Create(newadmin);
                    }
                    break;
                case AccountType.Client:
                    {
                        var repo = new clientsRepository(_context);
                        var newclient = new clients();
                        newclient.UserID = userId;
                        newclient.IndividualID = indid;
                        newclient.IsDeleted = 0;
                        repo.Create(newclient);
                    }
                    break;
                case AccountType.Donor:
                    {
                        var repo = new donorsRepository(_context);
                        var newdonor = new donors();
                        newdonor.UserID = userId;
                        newdonor.IndividualID = indid;
                        newdonor.IsDeleted = 0;
                        repo.Create(newdonor);
                    }
                    break;
            }

            unitOfWork.SaveChanges();
            ret = true;

            return ret;
        }

        public Tuple<displaylogin, string> LoginSystemUser(string email)
        {
            var ret = new Tuple<displaylogin, string>(null, string.Empty);

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Users WHERE Email = @Email AND IsActive = 1";
                var Emailparam = command.CreateParameter();
                Emailparam.ParameterName = "@Email";
                Emailparam.Value = email;
                command.Parameters.Add(Emailparam);                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ret = new Tuple<displaylogin, string>(
                        new displaylogin()
                        {
                            SystemUserID = reader.GetInt64(0),
                            AccountTypeID = reader.GetInt64(6)
                        }, reader.GetString(2));
                }

                reader.Close();
            }

            return ret;
        }

        public long GetSystemUserAccountType(long userId)
        {
            var ret = -1L;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT AccountTypeID FROM Users WHERE UserID = @UserID";
                var UserIdparam = command.CreateParameter();
                UserIdparam.ParameterName = "@UserID";
                UserIdparam.Value = userId;
                command.Parameters.Add(UserIdparam);
                ret = Convert.ToInt64(command.ExecuteScalar());
            }

            return ret;
        }

        public long SendPasswordResetEmail(string email, string passwordResetCode)
        {
            var ret = -1L;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT Count(*) FROM Users WHERE email = @email";
                var emailparam = command.CreateParameter();
                emailparam.ParameterName = "@email";
                emailparam.Value = email;
                command.Parameters.Add(emailparam);
                ret = Convert.ToInt64(command.ExecuteScalar());

                if (ret > 0)
                {
                    command.Parameters.Clear();
                    command.CommandType = CommandType.Text;
                    command.CommandText = "UPDATE Users SET PasswordResetCode = @PasswordResetCode WHERE Email = @Email";
                    emailparam = command.CreateParameter();
                    emailparam.ParameterName = "@Email";
                    emailparam.Value = email;
                    command.Parameters.Add(emailparam);
                    var resetparam = command.CreateParameter();
                    resetparam.ParameterName = "@PasswordResetCode";
                    resetparam.Value = passwordResetCode;
                    command.Parameters.Add(resetparam);
                    if (1 == command.ExecuteNonQuery())
                    {
                        ret = 0;
                    }
                }
            }

            return ret;
        }

        public long UpdateUserPassword(string passwordResetCode, string password)
        {
            var ret = -1L;

            using (var command = _context.CreateCommand())
            {                              
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE Users SET Password = @Password WHERE PasswordResetCode = @PasswordResetCode";
                var passwordparam = command.CreateParameter();
                passwordparam.ParameterName = "@Password";
                passwordparam.Value = password;
                command.Parameters.Add(passwordparam);
                var resetparam = command.CreateParameter();
                resetparam.ParameterName = "@PasswordResetCode";
                resetparam.Value = passwordResetCode;
                command.Parameters.Add(resetparam);
                if (1 == command.ExecuteNonQuery())
                {
                    ret = 0;
                }                
            }

            return ret;
        }

        public individuals RetrieveIndividualInformationBySystemUserID(long systemUserID)
        {
            var ret = new individuals();

            ret.LastName = string.Empty;
            ret.MiddleName = string.Empty;
            ret.FirstName = string.Empty;
            ret.FatherIndividualID = -1;
            ret.MotherIndividualID = -1;
            ret.Birthdate = DateTime.Now;
            ret.MobilePhoneNumber = string.Empty;
            ret.HomePhoneNumber = string.Empty;
            ret.AddressLine1 = string.Empty;
            ret.AddressLine2 = string.Empty;
            ret.City = string.Empty;
            ret.State = string.Empty;
            ret.Zip = string.Empty;

            var foundRecord = false;
            long individualID = -1;

            // search admin table

            using (var command = _context.CreateCommand())
            {
                var admin = new admins();

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM admins WHERE UserID = @UserID";
                var userparam = command.CreateParameter();
                userparam.ParameterName = "@UserID";
                userparam.Value = systemUserID;
                command.Parameters.Add(userparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foundRecord = true;
                    admin.AdminID = reader.GetInt64(0);
                    admin.UserID = reader.GetInt64(1);
                    admin.IndividualID = reader.GetInt64(2);
                    individualID = admin.IndividualID;
                }
                reader.Close();
            }

            // search clients table

            if (false == foundRecord)
            {
                using (var command = _context.CreateCommand())
                {
                    var client = new clients();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM clients WHERE UserID = @UserID";
                    var userparam = command.CreateParameter();
                    userparam.ParameterName = "@UserID";
                    userparam.Value = systemUserID;
                    command.Parameters.Add(userparam);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundRecord = true;
                        client.ClientID = reader.GetInt64(0);
                        client.UserID = reader.GetInt64(1);
                        client.IndividualID = reader.GetInt64(2);
                        individualID = client.IndividualID;
                    }
                    reader.Close();
                }
            }

            // search donor table

            if (false == foundRecord)
            {
                using (var command = _context.CreateCommand())
                {
                    var donor = new donors();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM donors WHERE UserID = @UserID";
                    var userparam = command.CreateParameter();
                    userparam.ParameterName = "@UserID";
                    userparam.Value = systemUserID;
                    command.Parameters.Add(userparam);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundRecord = true;
                        donor.DonorID = reader.GetInt64(0);
                        donor.UserID = reader.GetInt64(1);
                        donor.IndividualID = reader.GetInt64(2);
                        individualID = donor.IndividualID;
                    }
                    reader.Close();
                }
            }

            if (foundRecord == true)
            {
                using (var command = _context.CreateCommand())
                {
                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM Individuals WHERE IndividualID = @IndividualID";
                    var individualparam = command.CreateParameter();
                    individualparam.ParameterName = "@IndividualID";
                    individualparam.Value = individualID;
                    command.Parameters.Add(individualparam);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (false == reader.IsDBNull(1))
                        {
                            ret.LastName = reader.GetString(1);
                        }
                        else
                        {
                            ret.LastName = string.Empty;
                        }

                        if (false == reader.IsDBNull(2))
                        {
                            ret.MiddleName = reader.GetString(2);
                        }
                        else
                        {
                            ret.MiddleName = string.Empty;
                        }

                        if (false == reader.IsDBNull(3))
                        {
                            ret.FirstName = reader.GetString(3);
                        }
                        else
                        {
                            ret.FirstName = string.Empty;
                        }

                        if (false == reader.IsDBNull(4))
                        {
                            ret.FatherIndividualID = reader.GetInt64(4);
                        }
                        else
                        {
                            ret.FatherIndividualID = -1;
                        }

                        if (false == reader.IsDBNull(5))
                        {
                            ret.MotherIndividualID = reader.GetInt64(5);
                        }
                        else
                        {
                            ret.MotherIndividualID = -1;
                        }

                        if (false == reader.IsDBNull(6))
                        {
                            ret.Birthdate = reader.GetDateTime(6);
                        }
                        else
                        {
                            ret.Birthdate = DateTime.Now;
                        }

                        if (false == reader.IsDBNull(7))
                        {
                            ret.MobilePhoneNumber = reader.GetString(7);
                        }
                        else
                        {
                            ret.MobilePhoneNumber = string.Empty;
                        }

                        if (false == reader.IsDBNull(8))
                        {
                            ret.HomePhoneNumber = reader.GetString(8);
                        }
                        else
                        {
                            ret.HomePhoneNumber = string.Empty;
                        }                        

                        if (false == reader.IsDBNull(9))
                        {
                            ret.AddressLine1 = reader.GetString(9);
                        }
                        else
                        {
                            ret.AddressLine1 = string.Empty;
                        }

                        if (false == reader.IsDBNull(10))
                        {
                            ret.AddressLine2 = reader.GetString(10);
                        }
                        else
                        {
                            ret.AddressLine2 = string.Empty;
                        }

                        if (false == reader.IsDBNull(11))
                        {
                            ret.City = reader.GetString(11);
                        }
                        else
                        {
                            ret.City = string.Empty;
                        }

                        if (false == reader.IsDBNull(12))
                        {
                            ret.State = reader.GetString(12);
                        }
                        else
                        {
                            ret.State = string.Empty;
                        }

                        if (false == reader.IsDBNull(13))
                        {
                            ret.Zip = reader.GetString(13);
                        }
                        else
                        {
                            ret.Zip = string.Empty;
                        }
                    }
                    reader.Close();
                }
            }

            return ret;
        }

        public bool SaveIndividualInformation(long systemUserID, individuals individualInformation)
        {
            var ret = false;

            var foundRecord = false;
            long individualID = -1;

            // search admin table

            using (var command = _context.CreateCommand())
            {
                var admin = new admins();

                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM admins WHERE UserID = @UserID";
                var userparam = command.CreateParameter();
                userparam.ParameterName = "@UserID";
                userparam.Value = systemUserID;
                command.Parameters.Add(userparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    foundRecord = true;
                    admin.AdminID = reader.GetInt64(0);
                    admin.UserID = reader.GetInt64(1);
                    admin.IndividualID = reader.GetInt64(2);
                    individualID = admin.IndividualID;
                }
                reader.Close();
            }

            // search clients table

            if (false == foundRecord)
            {
                using (var command = _context.CreateCommand())
                {
                    var client = new clients();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM clients WHERE UserID = @UserID";
                    var userparam = command.CreateParameter();
                    userparam.ParameterName = "@UserID";
                    userparam.Value = systemUserID;
                    command.Parameters.Add(userparam);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundRecord = true;
                        client.ClientID = reader.GetInt64(0);
                        client.UserID = reader.GetInt64(1);
                        client.IndividualID = reader.GetInt64(2);
                        individualID = client.IndividualID;
                    }
                    reader.Close();
                }
            }

            // search donor table

            if (false == foundRecord)
            {
                using (var command = _context.CreateCommand())
                {
                    var donor = new donors();

                    command.CommandType = CommandType.Text;
                    command.CommandText = "SELECT * FROM donors WHERE UserID = @UserID";
                    var userparam = command.CreateParameter();
                    userparam.ParameterName = "@UserID";
                    userparam.Value = systemUserID;
                    command.Parameters.Add(userparam);
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        foundRecord = true;
                        donor.DonorID = reader.GetInt64(0);
                        donor.UserID = reader.GetInt64(1);
                        donor.IndividualID = reader.GetInt64(2);
                        individualID = donor.IndividualID;
                    }
                    reader.Close();
                }
            }

            if (false == foundRecord)
            {
                ret = false;
                return ret;
            }

            var indRepo = new individualsRepository(this._context);

            individualInformation.IndividualID = individualID;

            indRepo.Update(individualInformation);

            ret = true;

            return ret;
        }

        public List<programs> RetrieveAllPrograms()
        {
            var ret = new List<programs>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programs";                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var program = new programs()
                    {
                        ProgramID = reader.GetInt64(0),
                        Name = reader.GetString(1),
                        Description = reader.GetString(2),
                        IsPublished = reader.GetInt64(3),
                        Year = reader.GetInt64(4)
                    };

                    ret.Add(program);
                }
                reader.Close();
            }

            return ret;
        }

        public List<programevents> RetrieveAllProgramEventsByProgramId(long programId)
        {
            var ret = new List<programevents>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM programevents WHERE ProgramID = @ProgramId";
                var programidparam = command.CreateParameter();
                programidparam.ParameterName = "@ProgramId";
                programidparam.Value = programId;
                command.Parameters.Add(programidparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var programevent = new programevents()
                    {
                        ProgramEventID = reader.GetInt64(0),
                        ProgramID = reader.GetInt64(1),
                        IsSingleDate = reader.GetInt64(2),
                        FromDate = reader.GetDateTime(3),
                        ToDate = reader.GetDateTime(4),
                        Description = reader.GetString(5),
                        Name = reader.GetString(6)
                    };

                    ret.Add(programevent);
                }
                reader.Close();
            }

            return ret;
        }

        public List<DisplayAdministrator> RetrieveAllAdministrators()
        {
            var ret = new List<DisplayAdministrator>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select a.AdminID, i.LastName, i.FirstName, i.MobilePhoneNumber, u.Email from admins a
                                        INNER JOIN individuals i on i.IndividualID = a.IndividualID
                                        INNER JOIN users u on u.UserID = a.UserID
                                        WHERE a.IsDeleted = 0";                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var displayadmin = new DisplayAdministrator()
                    {
                        AdminID = reader.GetInt64(0),
                        LastName = reader.GetString(1),
                        FirstName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        EmailAddress = reader.GetString(4),                       
                    };

                    ret.Add(displayadmin);
                }
                reader.Close();
            }

            return ret;
        }

        public bool AddAdministrator(DisplayAdministrator displayAdministrator, string registrationId)
        {
            var ret = false;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM Users WHERE Email = @Email";
                var Emailparam = command.CreateParameter();
                Emailparam.ParameterName = "@Email";
                Emailparam.Value = displayAdministrator.EmailAddress;
                command.Parameters.Add(Emailparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ret = false;
                    return ret;
                }
            }

            long userId = -1;

            var unitOfWork = _context.CreateUnitOfWork();

            using (var command = _context.CreateCommand())
            {
                command.Parameters.Clear();
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO users(Email, Password, IsActive, RegistrationCode, PasswordResetCode, AccountTypeID) VALUES (@Email, @Password, @IsActive, @RegistrationCode, @PasswordResetCode, @AccountTypeID)";
                var Emailparam = command.CreateParameter();
                Emailparam.ParameterName = "@Email";
                Emailparam.Value = displayAdministrator.EmailAddress;
                command.Parameters.Add(Emailparam);
                var Passwordparam = command.CreateParameter();
                Passwordparam.ParameterName = "@Password";
                Passwordparam.Value = displayAdministrator.Password;
                command.Parameters.Add(Passwordparam);
                var IsActiveparam = command.CreateParameter();
                IsActiveparam.ParameterName = "@IsActive";
                IsActiveparam.Value = 0;
                command.Parameters.Add(IsActiveparam);
                var RegistrationCodeparam = command.CreateParameter();
                RegistrationCodeparam.ParameterName = "@RegistrationCode";
                RegistrationCodeparam.Value = registrationId;
                command.Parameters.Add(RegistrationCodeparam);
                var PasswordResetCodeparam = command.CreateParameter();
                PasswordResetCodeparam.ParameterName = "@PasswordResetCode";
                PasswordResetCodeparam.Value = "";
                command.Parameters.Add(PasswordResetCodeparam);
                var AccountTypeIDparam = command.CreateParameter();
                AccountTypeIDparam.ParameterName = "@AccountTypeID";
                AccountTypeIDparam.Value = AccountType.Administrator;
                command.Parameters.Add(AccountTypeIDparam);
                if (0 < command.ExecuteNonQuery())
                {
                    command.CommandText = "SELECT LAST_INSERT_ID(); ";
                    userId = Convert.ToInt64(command.ExecuteScalar());
                }
            }

            var repoind = new individualsRepository(_context);
            var newindividual = new individuals();
            newindividual.LastName = displayAdministrator.LastName;
            newindividual.FirstName = displayAdministrator.FirstName;
            newindividual.MobilePhoneNumber = displayAdministrator.PhoneNumber;
            var indid = repoind.Create(newindividual);
            
            var repo = new adminsRepository(_context);
            var newadmin = new admins();
            newadmin.UserID = userId;
            newadmin.IndividualID = indid;
            newadmin.IsDeleted = 0;
            repo.Create(newadmin);                  

            unitOfWork.SaveChanges();
            ret = true;

            return ret;
        }

        public void DeleteAdministrator(long adminId)
        {
            var unitOfWork = _context.CreateUnitOfWork();

            var adminrepo = new adminsRepository(_context);
            var userrepo = new usersRepository(_context);

            var existingadmin = adminrepo.RetrieveByID(adminId);

            var existinguser = userrepo.RetrieveByID(existingadmin.UserID);

            existingadmin.IsDeleted = 1;

            adminrepo.Update(existingadmin);

            existinguser.IsActive = 0;

            userrepo.Update(existinguser);

            unitOfWork.SaveChanges();
        }

        public List<displayclient> RetrieveAllClients()
        {
            var ret = new List<displayclient>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select c.ClientID, u.Email, i.LastName, i.MiddleName, i.FirstName,  
                                        i.Birthdate, i.MobilePhoneNumber, i.HomePhoneNumber, i.AddressLine1,
                                        i.AddressLine2, i.City, i.State, i.Zip
                                        from clients c
                                        INNER JOIN individuals i on i.IndividualID = c.IndividualID
                                        LEFT OUTER JOIN users u on u.UserID = c.UserID
                                        WHERE c.IsDeleted = 0";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var displayclient = new displayclient()
                    {
                        ClientID = reader.GetInt64(0),
                        EmailAddress = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        LastName = reader.IsDBNull(2) ? "" : reader.GetString(2),
                        MiddleName = reader.IsDBNull(3) ? "" : reader.GetString(3),
                        FirstName = reader.IsDBNull(4) ? "" : reader.GetString(4),
                        BirthDate = reader.IsDBNull(5) ?  DateTime.Now : reader.GetDateTime(5),
                        MobilePhoneNumber = reader.IsDBNull(6) ? "" : reader.GetString(6),
                        HomePhoneNumber = reader.IsDBNull(7) ? "" : reader.GetString(7),
                        AddressLine1 = reader.IsDBNull(8) ? "" : reader.GetString(8),
                        AddressLine2 = reader.IsDBNull(9) ? "" : reader.GetString(9),
                        City = reader.IsDBNull(10) ? "" : reader.GetString(10),
                        State = reader.IsDBNull(11) ? "" : reader.GetString(11),
                        Zip = reader.IsDBNull(12) ? "" : reader.GetString(12)
                    };

                    ret.Add(displayclient);
                }
                reader.Close();
            }

            return ret;
        }

        public bool AddClient(displayclient displayClient)
        {
            bool ret = false;

            var newClient = new clients();
            var newIndividual = new individuals();
            var newUser = new users();

            var unitOfWork = _context.CreateUnitOfWork();

            var clientrepo = new clientsRepository(_context);
            var indrepo = new individualsRepository(_context);
            var userrepo = new usersRepository(_context);

            // fill out user
            newUser.IsActive = 0;
            newUser.Password = "";
            newUser.AccountTypeID = (long)AccountType.Client;
            newUser.Email = displayClient.EmailAddress;

            // fill out individual
            newIndividual.IndividualID = -1;
            newIndividual.LastName = displayClient.LastName;
            newIndividual.MiddleName = displayClient.MiddleName;
            newIndividual.FirstName = displayClient.FirstName;
            newIndividual.FatherIndividualID = displayClient.FatherIndividualID;
            newIndividual.MotherIndividualID = displayClient.MotherIndividualID;
            newIndividual.Birthdate = displayClient.BirthDate;
            newIndividual.MobilePhoneNumber = displayClient.MobilePhoneNumber;
            newIndividual.HomePhoneNumber = displayClient.HomePhoneNumber;
            newIndividual.AddressLine1 = displayClient.AddressLine1;
            newIndividual.AddressLine2 = displayClient.AddressLine2;
            newIndividual.City = displayClient.City;
            newIndividual.State = displayClient.State;
            newIndividual.Zip = displayClient.Zip;

            newClient.IndividualID = indrepo.Create(newIndividual);
            newClient.UserID = userrepo.Create(newUser);

            newClient.IsDeleted = 0;

            if (clientrepo.Create(newClient) > 0)
                ret = true;

            unitOfWork.SaveChanges();

            return ret;
        }

        public bool ModifyClient(displayclient displayClient)
        {
            bool ret = false;            

            var clientrepo = new clientsRepository(_context);
            var indrepo = new individualsRepository(_context);
            var userrepo = new usersRepository(_context);

            var existingclient = clientrepo.RetrieveByID(displayClient.ClientID);
            var existinguser = userrepo.RetrieveByID(existingclient.UserID);
            var existingind = indrepo.RetrieveByID(existingclient.IndividualID);

            existingind.IndividualID = -1;
            existingind.LastName = displayClient.LastName;
            existingind.MiddleName = displayClient.MiddleName;
            existingind.FirstName = displayClient.FirstName;
            existingind.FatherIndividualID = displayClient.FatherIndividualID;
            existingind.MotherIndividualID = displayClient.MotherIndividualID;
            existingind.Birthdate = displayClient.BirthDate;
            existingind.MobilePhoneNumber = displayClient.MobilePhoneNumber;
            existingind.HomePhoneNumber = displayClient.HomePhoneNumber;
            existingind.AddressLine1 = displayClient.AddressLine1;
            existingind.AddressLine2 = displayClient.AddressLine2;
            existingind.City = displayClient.City;
            existingind.State = displayClient.State;
            existingind.Zip = displayClient.Zip;

            if (indrepo.Update(existingind) > 0)
                ret = true;

            return ret;
        }

        public void DeleteClient(long clientId)
        {
            var unitOfWork = _context.CreateUnitOfWork();

            var clientrepo = new clientsRepository(_context);
            var userrepo = new usersRepository(_context);

            var existingclient = clientrepo.RetrieveByID(clientId);

            var existinguser = userrepo.RetrieveByID(existingclient.UserID);

            existingclient.IsDeleted = 1;

            clientrepo.Update(existingclient);

            existinguser.IsActive = 0;

            userrepo.Update(existinguser);

            unitOfWork.SaveChanges();
        }

        public displayclient RetrieveClientInformationByClientID(long clientId)
        {
            displayclient ret = null;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select c.ClientID, u.Email, i.LastName, i.MiddleName, i.FirstName,
                                        i.FatherIndividualID, i.MotherIndividualID,  
                                        i.Birthdate, i.MobilePhoneNumber, i.HomePhoneNumber, i.AddressLine1,
                                        i.AddressLine2, i.City, i.State, i.Zip
                                        from clients c
                                        INNER JOIN individuals i on i.IndividualID = c.IndividualID
                                        LEFT OUTER JOIN users u on u.UserID = c.UserID
                                        WHERE c.IsDeleted = 0 AND c.ClientID = @ClientId";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@ClientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ret = new displayclient();
                    ret.ClientID = reader.GetInt64(0);
                    ret.EmailAddress = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    ret.LastName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    ret.MiddleName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    ret.FirstName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    ret.FatherIndividualID = reader.IsDBNull(5) ? -1 : reader.GetInt64(5);
                    ret.MotherIndividualID = reader.IsDBNull(6) ? -1 : reader.GetInt64(6);
                    ret.BirthDate = reader.IsDBNull(7) ? DateTime.Now : reader.GetDateTime(7);
                    ret.MobilePhoneNumber = reader.IsDBNull(8) ? "" : reader.GetString(8);
                    ret.HomePhoneNumber = reader.IsDBNull(9) ? "" : reader.GetString(9);
                    ret.AddressLine1 = reader.IsDBNull(10) ? "" : reader.GetString(10);
                    ret.AddressLine2 = reader.IsDBNull(11) ? "" : reader.GetString(11);
                    ret.City = reader.IsDBNull(12) ? "" : reader.GetString(12);
                    ret.State = reader.IsDBNull(13) ? "" : reader.GetString(13);
                    ret.Zip = reader.IsDBNull(14) ? "" : reader.GetString(14);
                }
                reader.Close();
            }

            return ret;
        }

        public bool SaveClientInformation(displayclient displayClient)
        {
            var ret = false;

            var clientrepo = new clientsRepository(_context);
            var clientrecord = clientrepo.RetrieveByID(displayClient.ClientID);

            var indrepo = new individualsRepository(_context);

            var individual = new individuals();

            individual.IndividualID = clientrecord.IndividualID;
            individual.LastName = displayClient.LastName;
            individual.MiddleName = displayClient.MiddleName;
            individual.FirstName = displayClient.FirstName;
            individual.FatherIndividualID = displayClient.FatherIndividualID;
            individual.MotherIndividualID = displayClient.MotherIndividualID;
            individual.Birthdate = displayClient.BirthDate;
            individual.MobilePhoneNumber = displayClient.MobilePhoneNumber;
            individual.HomePhoneNumber = displayClient.HomePhoneNumber;
            individual.AddressLine1 = displayClient.AddressLine1;
            individual.AddressLine2 = displayClient.AddressLine2;
            individual.City = displayClient.City;
            individual.State = displayClient.State;
            individual.Zip = displayClient.Zip;

            if (0 < indrepo.Update(individual))
                ret = true;

            return ret;
        }

        public List<displayclient> RetrieveClientsNotInExclusionList(List<long> clientIdExclusionList)
        {
            var ret = new List<displayclient>();

            var exclusionList = String.Join(",", clientIdExclusionList);

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select c.ClientID, u.Email, i.LastName, i.MiddleName, i.FirstName,  
                                        i.Birthdate, i.MobilePhoneNumber, i.HomePhoneNumber, i.AddressLine1,
                                        i.AddressLine2, i.City, i.State, i.Zip
                                        from clients c
                                        INNER JOIN individuals i on i.IndividualID = c.IndividualID
                                        LEFT OUTER JOIN users u on u.UserID = c.UserID
                                        WHERE c.IsDeleted = 0 AND c.ClientID NOT IN (" + exclusionList + ")";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var displayclient = new displayclient()
                    {
                        ClientID = reader.GetInt64(0),
                        EmailAddress = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        LastName = reader.GetString(2),
                        MiddleName = reader.GetString(3),
                        FirstName = reader.GetString(4),
                        BirthDate = reader.GetDateTime(5),
                        MobilePhoneNumber = reader.GetString(6),
                        HomePhoneNumber = reader.GetString(7),
                        AddressLine1 = reader.GetString(8),
                        AddressLine2 = reader.GetString(9),
                        City = reader.GetString(10),
                        State = reader.GetString(11),
                        Zip = reader.GetString(12)
                    };

                    ret.Add(displayclient);
                }
                reader.Close();
            }

            return ret;
        }        

        public individuals RetrieveClientFatherByClientID(long clientId)
        {
            individuals ret = null;

            var displayClient = RetrieveClientInformationByClientID(clientId);

            if (displayClient.FatherIndividualID == -1)
                return null;
            else
            {
                var individualsrepo = new individualsRepository(_context);
                ret = individualsrepo.RetrieveByID(displayClient.FatherIndividualID);                
            }

            return ret;
        }

        public bool SaveClientFather(long clientId, long clientFatherId)
        {
            var ret = false;

            var indrepo = new individualsRepository(_context);
            var clientrepo = new clientsRepository(_context);

            var clientToEdit = clientrepo.RetrieveByID(clientId);
            var individualToEdit = indrepo.RetrieveByID(clientToEdit.IndividualID);
            var clientToMakeFather = clientrepo.RetrieveByID(clientFatherId);

            individualToEdit.FatherIndividualID = clientToMakeFather.IndividualID;

            if (0 < indrepo.Update(individualToEdit))
                ret = true;

            return ret;
        }

        public individuals RetrieveClientMotherByClientId(long clientId)
        {
            individuals ret = null;

            var displayClient = RetrieveClientInformationByClientID(clientId);

            if (displayClient.MotherIndividualID == -1)
                return ret;
            else
            {
                var individualsrepo = new individualsRepository(_context);
                ret = individualsrepo.RetrieveByID(displayClient.MotherIndividualID);
            }

            return ret;
        }

        public bool SaveClientMother(long clientId, long clientMotherId)
        {
            var ret = false;

            var indrepo = new individualsRepository(_context);
            var clientrepo = new clientsRepository(_context);

            var clientToEdit = clientrepo.RetrieveByID(clientId);
            var individualToEdit = indrepo.RetrieveByID(clientToEdit.IndividualID);
            var clientToMakeMother = clientrepo.RetrieveByID(clientMotherId);

            individualToEdit.MotherIndividualID = clientToMakeMother.IndividualID;

            if (0 < indrepo.Update(individualToEdit))
                ret = true;

            return ret;
        }

        public bool DoesClientHaveSystemUserAccount(long clientId)
        {
            var ret = false;

            var clientrepo = new clientsRepository(_context);
            var usersrepo = new usersRepository(_context);

            var client = clientrepo.RetrieveByID(clientId);

            var useraccount = usersrepo.RetrieveByID(client.UserID);

            if (useraccount.Password.Length > 0)
            {
                ret = true;
            }

            return ret;
        }

        public bool CreateClientSystemUserAccount(long clientId, string password, string emailAddress, string registrationId)
        {
            var ret = false;

            var clientrepo = new clientsRepository(_context);
            var usersrepo = new usersRepository(_context);

            var client = clientrepo.RetrieveByID(clientId);

            var useraccount = usersrepo.RetrieveByID(client.UserID);

            useraccount.Password = password;
            useraccount.Email = emailAddress;
            useraccount.RegistrationCode = registrationId;
            useraccount.IsActive = 1;

            if (0 < usersrepo.Update(useraccount))
                ret = true;

            return ret;
        }

        public bool DeactivateClientSystemUserAccount(long clientId)
        {
            var ret = false;

            var clientrepo = new clientsRepository(_context);
            var usersrepo = new usersRepository(_context);

            var client = clientrepo.RetrieveByID(clientId);

            var useraccount = usersrepo.RetrieveByID(client.UserID);

            useraccount.Password = "";
            useraccount.RegistrationCode = "";
            useraccount.IsActive = 0;

            if (0 < usersrepo.Update(useraccount))
                ret = true;

            return ret;
        }

        public List<displayprogram> RetrieveListOfProgramsClientIsEnlistedIn(long clientId)
        {
            var ret = new List<displayprogram>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select p.programid, p.Name, p.Description, p.IsPublished, p.Year FROM" +
                    " clientprogramenlistments cpe INNER JOIN programs p ON p.ProgramId = cpe.ProgramId" +
                    " WHERE cpe.clientid = @ClientId";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@ClientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var displayprogram = new displayprogram()
                    {
                        ProgramID = reader.GetInt64(0),
                        Name = reader.IsDBNull(1) ? "" : reader.GetString(1),
                        Description = reader.GetString(2),
                        IsPublished = reader.GetInt16(3),
                        Year = reader.GetInt32(4),                     
                    };

                    ret.Add(displayprogram);
                }
                reader.Close();
            }

            return ret;
        }

        public List<displayprogramevent> RetrieveListOfProgramEventsClientIsEnlistedIn(long clientId, long programId)
        {
            var ret = new List<displayprogramevent>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select pe.ProgramEventId, pe.Name, pe.Description, pe.FromDate, pe.ToDate, p.programid, p.Name, p.Description, p.IsPublished, p.Year FROM" +
                                      " clientprogrameventenlistments cpee INNER JOIN programevents pe ON pe.ProgramEventId = cpee.ProgramEventId" +
                                      " INNER JOIN programs p on p.ProgramId = pe.ProgramId" +
                                      " WHERE cpee.clientid = @clientId AND p.ProgramId = @programId";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var programIdParam = command.CreateParameter();
                programIdParam.ParameterName = "@programId";
                programIdParam.Value = programId;
                command.Parameters.Add(programIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayprogramevent = new displayprogramevent() { };
                    adisplayprogramevent.program = new programs();

                    adisplayprogramevent.ProgramEventID = reader.GetInt64(0);
                    adisplayprogramevent.Name = reader.GetString(1);
                    adisplayprogramevent.Description = reader.GetString(2);
                    adisplayprogramevent.FromDate = reader.GetDateTime(3);
                    adisplayprogramevent.ToDate = reader.GetDateTime(4);
                    adisplayprogramevent.program.ProgramID = reader.GetInt64(5);
                    adisplayprogramevent.program.Name = reader.GetString(6);
                    adisplayprogramevent.program.Description = reader.GetString(7);
                    adisplayprogramevent.program.IsPublished = reader.GetInt16(8);
                    adisplayprogramevent.program.Year = reader.GetInt64(9);

                    ret.Add(adisplayprogramevent);
                }
                reader.Close();
            }

            return ret;
        }

        public bool EnlistClientInProgram(long clientId, long programId)
        {
            var ret = false;

            var clientprogramenlistmentsrepo = new clientprogramenlistmentsRepository(_context);

            var newenlistment = new clientprogramenlistments();

            newenlistment.ClientID = clientId;
            newenlistment.ProgramID = programId;

            if (0 < clientprogramenlistmentsrepo.Create(newenlistment))
            {
                ret = true;
            }

            return ret;
        }

        public bool UnenlistClientInProgram(long clientId, long programId)
        {
            var ret = false;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM clientprogramenlistments WHERE ClientId = @ClientId AND ProgramId = @ProgramId";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@clientId";
                ClientIDparam.Value = clientId;
                command.Parameters.Add(ClientIDparam);
                var ProgramIDparam = command.CreateParameter();
                ProgramIDparam.ParameterName = "@ProgramId";
                ProgramIDparam.Value = programId;
                command.Parameters.Add(ProgramIDparam);
                if (0 < command.ExecuteNonQuery())
                    ret = true;
            }

            return ret;
        }

        public bool EnlistClientInProgramEvent(long clientId, long programEventId)
        {
            var ret = false;

            var clientprogrameventenlistmentsrepo = new clientprogrameventenlistmentsRepository(_context);

            var newenlistment = new clientprogrameventenlistments();

            newenlistment.ClientID = clientId;
            newenlistment.ProgramEventID = programEventId;

            if (0 < clientprogrameventenlistmentsrepo.Create(newenlistment))
            {
                ret = true;
            }

            return ret;
        }

        public bool UnenlistClientInProgramEvent(long clientId, long programEventId)
        {
            var ret = false;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM clientprogrameventenlistments WHERE ClientId = @ClientId AND programEventId = @programEventId";
                var ClientIDparam = command.CreateParameter();
                ClientIDparam.ParameterName = "@clientId";
                ClientIDparam.Value = clientId;
                command.Parameters.Add(ClientIDparam);
                var ProgramEventIDparam = command.CreateParameter();
                ProgramEventIDparam.ParameterName = "@programEventId";
                ProgramEventIDparam.Value = clientId;
                command.Parameters.Add(ProgramEventIDparam);
                if (0 < command.ExecuteNonQuery())
                    ret = true;
            }

            return ret;
        }

        public List<programdonorcommitments> RetrieveDonorAssignedClientRequests(long clientId)
        {
            return new List<programdonorcommitments>();
        }

        public List<displaydonor> RetrieveAllDonors()
        {
            var ret = new List<displaydonor>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select d.DonorID, u.Email, i.LastName, i.MiddleName, i.FirstName,  
                                        i.Birthdate, i.MobilePhoneNumber, i.HomePhoneNumber, i.AddressLine1,
                                        i.AddressLine2, i.City, i.State, i.Zip
                                        from donors d
                                        INNER JOIN individuals i on i.IndividualID = d.IndividualID
                                        LEFT OUTER JOIN users u on u.UserID = d.UserID
                                        WHERE d.IsDeleted = 0";
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var displaydonor = new displaydonor();

                    displaydonor.DonorID = reader.GetInt64(0);
                    displaydonor.EmailAddress = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    displaydonor.LastName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    displaydonor.MiddleName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    displaydonor.FirstName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    displaydonor.BirthDate = reader.IsDBNull(5) ? DateTime.Now: reader.GetDateTime(5);
                    displaydonor.MobilePhoneNumber = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    displaydonor.HomePhoneNumber = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    displaydonor.AddressLine1 = reader.IsDBNull(8) ? "" : reader.GetString(8);
                    displaydonor.AddressLine2 = reader.IsDBNull(9) ? "" : reader.GetString(9);
                    displaydonor.City = reader.IsDBNull(10) ? "" : reader.GetString(10);
                    displaydonor.State = reader.IsDBNull(11) ? "" : reader.GetString(11);
                    displaydonor.Zip = reader.IsDBNull(12) ? "" : reader.GetString(12);

                    ret.Add(displaydonor);
                }
                reader.Close();
            }

            return ret;
        }

        public bool AddDonor(displaydonor displayDonor, string registrationCode)
        {
            bool ret = false;

            var newDonor = new donors();
            var newIndividual = new individuals();
            var newUser = new users();

            var unitOfWork = _context.CreateUnitOfWork();

            var donorrepo = new donorsRepository(_context);
            var indrepo = new individualsRepository(_context);
            var userrepo = new usersRepository(_context);

            // fill out user
            newUser.IsActive = 0;
            newUser.Password = displayDonor.Password;
            newUser.AccountTypeID = (long)AccountType.Donor;
            newUser.Email = displayDonor.EmailAddress;
            newUser.RegistrationCode = registrationCode;

            // fill out individual
            newIndividual.IndividualID = -1;
            newIndividual.LastName = displayDonor.LastName;
            newIndividual.MiddleName = displayDonor.MiddleName;
            newIndividual.FirstName = displayDonor.FirstName;            
            newIndividual.Birthdate = displayDonor.BirthDate;
            newIndividual.MobilePhoneNumber = displayDonor.MobilePhoneNumber;
            newIndividual.HomePhoneNumber = displayDonor.HomePhoneNumber;
            newIndividual.AddressLine1 = displayDonor.AddressLine1;
            newIndividual.AddressLine2 = displayDonor.AddressLine2;
            newIndividual.City = displayDonor.City;
            newIndividual.State = displayDonor.State;
            newIndividual.Zip = displayDonor.Zip;

            newDonor.IndividualID = indrepo.Create(newIndividual);
            newDonor.UserID = userrepo.Create(newUser);

            newDonor.IsDeleted = 0;

            if (donorrepo.Create(newDonor) > 0)
                ret = true;

            unitOfWork.SaveChanges();

            return ret;
        }

        public bool ModifyDonor(displaydonor displayDonor)
        {
            bool ret = false;

            var donorrepo = new donorsRepository(_context);
            var indrepo = new individualsRepository(_context);
            var userrepo = new usersRepository(_context);

            var existingDonor = donorrepo.RetrieveByID(displayDonor.DonorID);
            var existinguser = userrepo.RetrieveByID(existingDonor.UserID);
            var existingind = indrepo.RetrieveByID(existingDonor.IndividualID);

            existingind.IndividualID = -1;
            existingind.LastName = displayDonor.LastName;
            existingind.MiddleName = displayDonor.MiddleName;
            existingind.FirstName = displayDonor.FirstName;            
            existingind.Birthdate = displayDonor.BirthDate;
            existingind.MobilePhoneNumber = displayDonor.MobilePhoneNumber;
            existingind.HomePhoneNumber = displayDonor.HomePhoneNumber;
            existingind.AddressLine1 = displayDonor.AddressLine1;
            existingind.AddressLine2 = displayDonor.AddressLine2;
            existingind.City = displayDonor.City;
            existingind.State = displayDonor.State;
            existingind.Zip = displayDonor.Zip;

            if (indrepo.Update(existingind) > 0)
                ret = true;

            return ret;
        }

        public bool DeleteDonor(long donorId)
        {
            var unitOfWork = _context.CreateUnitOfWork();

            var donorrepo = new donorsRepository(_context);
            var userrepo = new usersRepository(_context);

            var existingdonor = donorrepo.RetrieveByID(donorId);

            var existinguser = userrepo.RetrieveByID(existingdonor.UserID);

            existingdonor.IsDeleted = 1;

            donorrepo.Update(existingdonor);

            existinguser.IsActive = 0;

            userrepo.Update(existinguser);

            unitOfWork.SaveChanges();

            return true;
        }

        public displaydonor RetrieveDonorInformationByDonorID(long donorId)
        {
            displaydonor ret = null;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select d.DonorID, u.Email, i.LastName, i.MiddleName, i.FirstName,                                        
                                        i.Birthdate, i.MobilePhoneNumber, i.HomePhoneNumber, i.AddressLine1,
                                        i.AddressLine2, i.City, i.State, i.Zip
                                        from donors d
                                        INNER JOIN individuals i on i.IndividualID = d.IndividualID
                                        LEFT OUTER JOIN users u on u.UserID = d.UserID
                                        WHERE d.IsDeleted = 0 AND d.DonorID = @DonorId";
                var donorIdParam = command.CreateParameter();
                donorIdParam.ParameterName = "@DonorId";
                donorIdParam.Value = donorId;
                command.Parameters.Add(donorIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ret = new displaydonor();
                    ret.DonorID = reader.GetInt64(0);
                    ret.EmailAddress = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    ret.LastName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    ret.MiddleName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    ret.FirstName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    ret.BirthDate = reader.IsDBNull(5) ? DateTime.Now : reader.GetDateTime(5);
                    ret.MobilePhoneNumber = reader.IsDBNull(6) ? "" : reader.GetString(6);
                    ret.HomePhoneNumber = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    ret.AddressLine1 = reader.IsDBNull(8) ? "" : reader.GetString(8);
                    ret.AddressLine2 = reader.IsDBNull(9) ? "" : reader.GetString(9);
                    ret.City = reader.IsDBNull(10) ? "" : reader.GetString(10);
                    ret.State = reader.IsDBNull(11) ? "" : reader.GetString(11);
                    ret.Zip = reader.IsDBNull(12) ? "" : reader.GetString(12);
                }
                reader.Close();
            }

            return ret;
        }

        public bool SaveDonorInformation(displaydonor displayDonor)
        {
            var ret = false;

            var donorrepo = new donorsRepository(_context);
            var donorrecord = donorrepo.RetrieveByID(displayDonor.DonorID);

            var indrepo = new individualsRepository(_context);

            var individual = new individuals();

            individual.IndividualID = donorrecord.IndividualID;
            individual.LastName = displayDonor.LastName;
            individual.MiddleName = displayDonor.MiddleName;
            individual.FirstName = displayDonor.FirstName;            
            individual.Birthdate = displayDonor.BirthDate;
            individual.MobilePhoneNumber = displayDonor.MobilePhoneNumber;
            individual.HomePhoneNumber = displayDonor.HomePhoneNumber;
            individual.AddressLine1 = displayDonor.AddressLine1;
            individual.AddressLine2 = displayDonor.AddressLine2;
            individual.City = displayDonor.City;
            individual.State = displayDonor.State;
            individual.Zip = displayDonor.Zip;

            if (0 < indrepo.Update(individual))
                ret = true;

            return ret;
        }

        public List<programs> RetrieveListOfProgramsDonorIsEnlistedIn(long donorId)
        {
            var ret = new List<programs>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"select p.ProgramId, p.Name, p.Description, p.Year, p.IsPublished FROM programs p 
                    INNER JOIN donorprogramenlistmenets dpl ON p.programId = dpl.programId
                    d.DonorId = @donorId";
                var donorIdParam = command.CreateParameter();
                donorIdParam.ParameterName = "@donorId";
                donorIdParam.Value = donorId;
                command.Parameters.Add(donorIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var program = new programs();

                    program.ProgramID = reader.GetInt64(0);
                    program.Name = reader.GetString(1);
                    program.Description = reader.GetString(2);
                    program.Year = reader.GetInt32(3);
                    program.IsPublished = reader.GetInt16(4);

                    ret.Add(program);
                }
                reader.Close();
            }

            return ret;
        }

        public List<clientrequests> RetrieveClientRequestsByProgramId(long programId, long clientId)
        {
            var clientrequestsrepo = new clientrequestsRepository(_context);

            return clientrequestsrepo.RetrieveWithWhereClauseclientrequests("ProgramId = " + programId.ToString() + " AND ClientId = " + clientId.ToString());
        }

        public bool SendInternalMessage(messages message, List<messagerecipients> listMessageRecipients)
        {
            var ret = false;

            var unitOfWork = _context.CreateUnitOfWork();

            var messagerepo = new messagesRepository(_context);
            var messagerecipientrepo = new messagerecipientsRepository(_context);

            long messageId = messagerepo.Create(message);

            foreach (var messageRecipient in listMessageRecipients)
            {
                messageRecipient.MessageID = messageId;
                messagerecipientrepo.Create(messageRecipient);
            }

            unitOfWork.SaveChanges();

            ret = true;

            return ret;
        }

        public List<displaymessage> RetrieveAllInboxMessages(long accountID, long accountTypeId)
        {
            var ret = new List<displaymessage>();

            using (var command = _context.CreateCommand())
            {                
                command.CommandType = CommandType.Text;

                if (accountTypeId == 1)
                {
                    command.CommandText = @"" +
                                      " select  " +
                                      " m.MessageID,  " +
                                      " mr.MessageRecipientID,  " +
                                      " m.FromAccountID AS FromAccount,  " +
                                      " m.FromAccountTypeID AS FromAccountType, " +
                                      " mr.AccountID AS ToAccount, " +
                                      " mr.AccountTypeID AS ToAccountTypeID,  " +
                                      " m.MessageSentDateTime,  " +
                                      " m.Subject,  " +
                                      " m.Body,  " +
                                      " mr.MessageRead, " +
                                      " i.LastName, " +
                                      " i.FirstName " +
                                      " FROM messages m     " +
                                      " INNER JOIN messagerecipients mr ON m.MessageId = mr.MessageId " +
                                      " LEFT OUTER JOIN admins a on a.AdminID = mr.AccountID " +
                                      " LEFT OUTER JOIN individuals i on i.IndividualID = a.IndividualID " +
                                      " WHERE mr.AccountTypeID = @accountTypeId and mr.AccountID = @accountId" +
                                      " ORDER BY m.MessageSentDateTime DESC";
                }
                else if (accountTypeId == 2)
                {
                    command.CommandText = @"select  " +
                                      " m.MessageID,  " +
                                      " mr.MessageRecipientID,  " +
                                      " m.FromAccountID AS FromAccount,  " +
                                      " m.FromAccountTypeID AS FromAccountType, " +
                                      " mr.AccountID AS ToAccount, " +
                                      " mr.AccountTypeID AS ToAccountTypeID,  " +
                                      " m.MessageSentDateTime,  " +
                                      " m.Subject,  " +
                                      " m.Body,  " +
                                      " mr.MessageRead, " +
                                      " i.LastName, " +
                                      " i.FirstName " +
                                      " FROM messages m     " +
                                      " INNER JOIN messagerecipients mr ON m.MessageId = mr.MessageId " +
                                      " LEFT OUTER JOIN admins a on a.AdminID = mr.AccountID " +
                                      " LEFT OUTER JOIN individuals i on i.IndividualID = a.IndividualID " +
                                      " WHERE mr.AccountTypeID = @accountTypeId and mr.AccountID = @accountId" +
                                      " ORDER BY m.MessageSentDateTime DESC"; ;
                }
                else if (accountTypeId == 3)
                {
                    command.CommandText = @"select " +
                                          " m.MessageID,  " +
                                          " mr.MessageRecipientID,  " +
                                          " m.FromAccountID AS FromAccount,  " +
                                          " m.FromAccountTypeID AS FromAccountType, " +
                                          " mr.AccountID AS ToAccount, " +
                                          " mr.AccountTypeID AS ToAccountTypeID,  " +
                                          " m.MessageSentDateTime,  " +
                                          " m.Subject,  " +
                                          " m.Body,  " +
                                          " mr.MessageRead, " +
                                          " i.LastName, " +
                                          " i.FirstName " +
                                          " FROM messages m   " +
                                          " INNER JOIN messagerecipients mr ON m.MessageId = mr.MessageId " +
                                          " LEFT OUTER JOIN clients c on c.ClientID = mr.AccountID " +
                                          " LEFT OUTER JOIN individuals i on i.IndividualID = c.IndividualID " +
                                          " WHERE mr.AccountTypeID = @accountTypeId and mr.AccountID = @accountId " +
                                          " ORDER BY m.MessageSentDateTime DESC"; ;
                }

                
                var accountIdParam = command.CreateParameter();
                accountIdParam.ParameterName = "@accountId";
                accountIdParam.Value = accountID;
                command.Parameters.Add(accountIdParam);
                var accountTypeIdParam = command.CreateParameter();
                accountTypeIdParam.ParameterName = "@accountTypeId";
                accountTypeIdParam.Value = accountTypeId;
                command.Parameters.Add(accountTypeIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayMessage = new displaymessage();                    

                    adisplayMessage.MessageID = reader.GetInt64(0);
                    adisplayMessage.messageRecipientId = reader.GetInt64(1);
                    adisplayMessage.FromAccountID = reader.GetInt64(2);
                    adisplayMessage.FromAccountTypeID = reader.GetInt64(3);
                    adisplayMessage.MessageSentDateTime = reader.GetDateTime(6);
                    adisplayMessage.Subject = reader.GetString(7);
                    adisplayMessage.Body = reader.GetString(8);
                    adisplayMessage.messageRead = reader.IsDBNull(9) ? (DateTime?)null : reader.GetDateTime(9);
                    adisplayMessage.FromLastName = reader.IsDBNull(10) ? "" : reader.GetString(10);
                    adisplayMessage.FromFirstName = reader.IsDBNull(11) ? "": reader.GetString(11);

                    ret.Add(adisplayMessage);
                }
                reader.Close();
            }

            return ret;
        }

        public List<displayprogram> RetrieveAllProgramsClientNotEnlistedIn(long clientId)
        {
            var ret = new List<displayprogram>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"SELECT p.ProgramID, p.`Name`, p.Description, p.IsPublished, p.`Year` FROM programs p WHERE p.ProgramID " +
                    " NOT IN (SELECT cpe.programID from clientprogramenlistments cpe WHERE cpe.clientid = @clientId)";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var aprogram = new displayprogram();

                    aprogram.ProgramID = reader.GetInt64(0);
                    aprogram.Name = reader.GetString(1);
                    aprogram.Description = reader.GetString(2);
                    aprogram.Year = reader.GetInt32(3);
                    aprogram.IsPublished = reader.GetInt16(4);

                    ret.Add(aprogram);
                }
                reader.Close();
            }

            return ret;
        }

        public List<displayprogramevent> RetrieveAllProgramEventsClientNotEnlistedIn(long clientId, long programId)
        {
            var ret = new List<displayprogramevent>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"SELECT p.ProgramID, p.`Name`, p.Description, p.IsPublished, p.`Year`, " +
                                      " pe.ProgramEventID, pe.`Name`, pe.Description, pe.FromDate, pe.ToDate " +
                                      " FROM programs p  " +
                                      " INNER JOIN programevents pe ON pe.ProgramID = p.ProgramID " +
                                      " INNER JOIN clientprogramenlistments cpe ON cpe.ProgramID = p.ProgramID " +
                                      " WHERE pe.ProgramEventID " +
                                      " NOT IN  " +
                                      " (SELECT cpee.programEventID from clientprogrameventenlistments cpee WHERE cpee.clientid = @clientId) " +
                                      " AND pe.ProgramID IN (SELECT cpe.ProgramID FROM clientprogramenlistments cpe WHERE cpe.ClientID = @clientId)";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var programIdParam = command.CreateParameter();
                programIdParam.ParameterName = "@programId";
                programIdParam.Value = clientId;
                command.Parameters.Add(programIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayprogramevent = new displayprogramevent() { };
                    adisplayprogramevent.program = new programs();

                    adisplayprogramevent.ProgramID = reader.GetInt64(0);
                    adisplayprogramevent.Name = reader.GetString(1);
                    adisplayprogramevent.Description = reader.GetString(2);
                    adisplayprogramevent.program.IsPublished = reader.GetInt16(3);
                    adisplayprogramevent.program.Year = reader.GetInt64(4);
                    adisplayprogramevent.ProgramEventID = reader.GetInt64(5);
                    adisplayprogramevent.program.Name = reader.GetString(6);
                    adisplayprogramevent.program.Description = reader.GetString(7);
                    adisplayprogramevent.FromDate = reader.IsDBNull(8) ? DateTime.Now : reader.GetDateTime(8);
                    adisplayprogramevent.ToDate = reader.IsDBNull(9) ? DateTime.Now : reader.GetDateTime(9);                                     

                    ret.Add(adisplayprogramevent);
                }
                reader.Close();
            }

            return ret;
        }

        public List<displayprogram> RetrieveAllProgramsDonorNotEnlistedIn(long donorId)
        {
            var ret = new List<displayprogram>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @"SELECT p.ProgramID, p.`Name`, p.Description, p.IsPublished, p.`Year` FROM programs p WHERE p.ProgramID " +
                                      " NOT IN (SELECT dpe.programID from donorprogramenlistments dpe WHERE dpe.donorId = @donorId)";
                var donorIdParam = command.CreateParameter();
                donorIdParam.ParameterName = "@donorId";
                donorIdParam.Value = donorId;
                command.Parameters.Add(donorIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var aprogram = new displayprogram();

                    aprogram.ProgramID = reader.GetInt64(0);
                    aprogram.Name = reader.GetString(1);
                    aprogram.Description = reader.GetString(2);
                    aprogram.Year = reader.GetInt32(3);
                    aprogram.IsPublished = reader.GetInt16(4);

                    ret.Add(aprogram);
                }
                reader.Close();
            }

            return ret;
        }

        public displayaccount RetrieveDisplayAccountByLoggedInUserID(long loggedInUserID)
        {
            var ret = new displayaccount();

            var userrepo = new usersRepository(_context);

            var user = userrepo.RetrieveByID(loggedInUserID);

            ret.FromAccountTypeID = user.AccountTypeID;

            switch (user.AccountTypeID)
            {
                case 1:
                    var adminsrepo = new adminsRepository(_context);
                    var listadmins = adminsrepo.RetrieveWithWhereClauseadmins(" UserID = " + user.UserID);
                    ret.FromAccountID = listadmins[0].AdminID;
                    break;
                case 2:
                    var donorsrepo = new donorsRepository(_context);
                    var listdonors = donorsrepo.RetrieveWithWhereClausedonors(" UserID = " + user.UserID);
                    ret.FromAccountID = listdonors[0].DonorID;
                    break;
                case 3:
                    var clientsrepo = new clientsRepository(_context);
                    var listclients = clientsrepo.RetrieveWithWhereClauseclients(" UserID = " + user.UserID);
                    ret.FromAccountID = listclients[0].ClientID;
                    break;
            }

            return ret;
        }

        public List<displayhappyclientphotos> RetrieveHappyClientPhotosByProgramID(long clientId, long programId)
        {
            var ret = new List<displayhappyclientphotos>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" select hcp.HappyClientPictureID, " +
                                      " hcp.ProgramID, hcp.RecipientClientID, hcp.FileUploadID, " +
                                      "  fu.Filename, fu.Size, fu.Created, fu.`Data` FROM happyclientpictures hcp  " +
                                      "  INNER JOIN fileuploads fu on fu.FileUploadID = hcp.FileUploadID " +
                                      "  WHERE hcp.RecipientClientID = @clientId AND hcp.ProgramID = @programId " ;
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var programIdParam = command.CreateParameter();
                programIdParam.ParameterName = "@programId";
                programIdParam.Value = clientId;
                command.Parameters.Add(programIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayhappyclientphoto = new displayhappyclientphotos();
                    adisplayhappyclientphoto.aHappyClientPicture = new happyclientpictures();
                    adisplayhappyclientphoto.aFileUpload = new fileuploads();

                    adisplayhappyclientphoto.aHappyClientPicture.HappyClientPictureID = reader.GetInt64(0);                    
                    adisplayhappyclientphoto.aHappyClientPicture.ProgramID = reader.GetInt64(1);
                    adisplayhappyclientphoto.aHappyClientPicture.RecipientClientID = reader.GetInt64(2);
                    adisplayhappyclientphoto.aHappyClientPicture.FileUploadID = reader.GetInt64(3);                    
                    adisplayhappyclientphoto.aFileUpload.Filename = reader.GetString(4);
                    adisplayhappyclientphoto.aFileUpload.Size = reader.GetInt64(5);
                    adisplayhappyclientphoto.aFileUpload.Created = reader.GetDateTime(6);
                    byte[] buffer = new byte[2000000000];
                    long read = reader.GetBytes(7, 0, buffer, 0, 2000000000);
                    adisplayhappyclientphoto.aFileUpload.Data = new byte[read];
                    Buffer.BlockCopy(buffer, 0, adisplayhappyclientphoto.aFileUpload.Data, 0, Convert.ToInt32(read));

                    adisplayhappyclientphoto.base64Photo = System.Convert.ToBase64String(adisplayhappyclientphoto.aFileUpload.Data);
                    adisplayhappyclientphoto.aFileUpload.Data = null;

                    ret.Add(adisplayhappyclientphoto);
                }
                reader.Close();
            }

            return ret;
        }

        public bool AddHappyClientPhoto(long clientId, long programId, byte[] photoData, string fileName, long fileSize)
        {
            bool ret = false;

            var unitOfWork = _context.CreateUnitOfWork();

            var happyPhotosRepo = new happyclientpicturesRepository(_context);
            var fileUploadsRepo = new fileuploadsRepository(_context);

            var fileUpload = new fileuploads();

            fileUpload.Filename = fileName;
            fileUpload.Size = fileSize;
            fileUpload.Data = photoData;

            var fileUploadId = fileUploadsRepo.Create(fileUpload);

            var happyClientPhoto = new happyclientpictures();

            happyClientPhoto.ProgramID = programId;
            happyClientPhoto.RecipientClientID = clientId;
            happyClientPhoto.FileUploadID = fileUploadId;

            happyPhotosRepo.Create(happyClientPhoto);
            
            unitOfWork.SaveChanges();

            ret = true;

            return ret;
        }

        public bool DeleteHappyClientPhoto(long happyClientPictureID)
        {
            bool ret = false;

            var unitOfWork = _context.CreateUnitOfWork();

            var happyPhotosRepo = new happyclientpicturesRepository(_context);
            var fileUploadsRepo = new fileuploadsRepository(_context);

            var happyClientPicture = happyPhotosRepo.RetrieveByID(happyClientPictureID);

            var fileUpload = new fileuploads();

            fileUpload.FileUploadID = happyClientPicture.FileUploadID;

            fileUploadsRepo.Delete(fileUpload);
            happyPhotosRepo.Delete(happyClientPicture);

            unitOfWork.SaveChanges();

            ret = true;

            return ret;
        }

        public bool DeactivateClient(long clientId)
        {
            var ret = false;

            var unitOfWork = _context.CreateUnitOfWork();

            var clientrepo = new clientsRepository(_context);
            var usersrepo = new usersRepository(_context);

            var client = clientrepo.RetrieveByID(clientId);

            var useraccount = usersrepo.RetrieveByID(client.UserID);

            useraccount.IsActive = 0;

            usersrepo.Update(useraccount);

            client.IsDeleted = 1;

            clientrepo.Update(client);

            unitOfWork.SaveChanges();

            ret = true;

            return ret;
        }

        public List<displayclientrequest> RetrieveAllNonCommittedClientRequests()
        {
            var ret = new List<displayclientrequest>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" select cr.ClientRequestID, cr.RequestInformation, p.`Name`, p.Description, p.`Year`, " +
                                      " i.LastName, i.MiddleName, i.FirstName, i.Birthdate, i.city, i.Zip " +
                                        "   from clientrequests cr  " +
                                        "  INNER JOIN programs p on p.ProgramID = cr.ProgramID " +
                                        "  INNER JOIN clients c on c.ClientID = cr.ClientID " +
                                        "  INNER JOIN individuals i on i.IndividualID = c.IndividualID " +
                                        "  WHERE  " +
                                        "  cr.ClientRequestID NOT IN (SELECT pdc.ClientRequestID from programdonorcommitments pdc) ";                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayclientrequest = new displayclientrequest();

                    adisplayclientrequest.ClientRequestID = reader.GetInt64(0);
                    adisplayclientrequest.ClientRequest = reader.GetString(1);
                    adisplayclientrequest.ProgramName = reader.GetString(2);
                    adisplayclientrequest.ProgramDescription = reader.GetString(3);
                    adisplayclientrequest.ProgramYear = reader.GetInt64(4);
                    adisplayclientrequest.ClientLastName = reader.GetString(5);
                    adisplayclientrequest.ClientMiddleName = reader.GetString(6);
                    adisplayclientrequest.ClientFirstName = reader.GetString(7);
                    adisplayclientrequest.ClientBirthDate = reader.GetDateTime(8);
                    adisplayclientrequest.ClientCity = reader.GetString(9);
                    adisplayclientrequest.ClientZip = reader.GetString(10);

                    ret.Add(adisplayclientrequest);
                }
                reader.Close();
            }

            return ret;
        }

        public List<displayclientrequest> RetrieveAllCommittedClientRequestsByDonorId(long donorId)
        {
            var ret = new List<displayclientrequest>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" select cr.ClientRequestID, pdc.ProgramDonorCommitmentID, cr.RequestInformation, p.`Name`, p.Description, p.`Year`, " +
                                      " i.LastName, i.MiddleName, i.FirstName, i.Birthdate, i.city, i.Zip  " +
                                      " from programdonorcommitments pdc " +
                                      " INNER JOIN clientrequests cr on pdc.ClientRequestID = pdc.ClientRequestID " +
                                      " INNER JOIN programs p on p.ProgramID = cr.ProgramID  " +
                                      " INNER JOIN clients c on c.ClientID = cr.ClientID  " +
                                      " INNER JOIN individuals i on i.IndividualID = c.IndividualID  " +
                                      " WHERE   " +
                                      " cr.ClientRequestID IN (SELECT pdc.ClientRequestID from programdonorcommitments pdc) " +
                                      " AND pdc.DonorID = @donorId ";
                var donorIdParam = command.CreateParameter();
                donorIdParam.ParameterName = "@donorId";
                donorIdParam.Value = donorId;
                command.Parameters.Add(donorIdParam);                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayclientrequest = new displayclientrequest();

                    adisplayclientrequest.ClientRequestID = reader.GetInt64(0);
                    adisplayclientrequest.ProgramDonorCommittmentID = reader.GetInt64(1);
                    adisplayclientrequest.ClientRequest = reader.GetString(2);
                    adisplayclientrequest.ProgramName = reader.GetString(3);
                    adisplayclientrequest.ProgramDescription = reader.GetString(4);
                    adisplayclientrequest.ProgramYear = reader.GetInt64(5);
                    adisplayclientrequest.ClientLastName = reader.GetString(6);
                    adisplayclientrequest.ClientMiddleName = reader.GetString(7);
                    adisplayclientrequest.ClientFirstName = reader.GetString(8);
                    adisplayclientrequest.ClientBirthDate = reader.GetDateTime(9);
                    adisplayclientrequest.ClientCity = reader.GetString(10);
                    adisplayclientrequest.ClientZip = reader.GetString(11);

                    ret.Add(adisplayclientrequest);
                }
                reader.Close();
            }

            return ret;
        }

        public bool DeactivateDonor(long donorId)
        {
            var ret = false;

            var unitOfWork = _context.CreateUnitOfWork();

            var donorrepo = new donorsRepository(_context);
            var usersrepo = new usersRepository(_context);

            var donor = donorrepo.RetrieveByID(donorId);

            var useraccount = usersrepo.RetrieveByID(donor.UserID);

            useraccount.IsActive = 0;

            usersrepo.Update(useraccount);

            donor.IsDeleted = 1;

            donorrepo.Update(donor);

            unitOfWork.SaveChanges();

            ret = true;

            return ret;
        }

        public void SetMessageRecipientMessageAsRead(long messageRecipientId)
        {
            var messageRecipientsRepo = new messagerecipientsRepository(_context);

            var messageRecipientRecord = messageRecipientsRepo.RetrieveByID(messageRecipientId);

            messageRecipientRecord.MessageRead = DateTime.Now;

            messageRecipientsRepo.Update(messageRecipientRecord);
        }

        public List<displayavailablemessagerecipient> RetrieveAllAvailableMessageRecipientsWithExclusionList(
            List<displayavailablemessagerecipient> exclusionlist)
        {
            var ret = new List<displayavailablemessagerecipient>();

            using (var command = _context.CreateCommand())
            {
                var exclusionListClause = new StringBuilder();

                exclusionListClause.Append("(");

                foreach(var listItem in exclusionlist)
                {                  
                    exclusionListClause.Append("(");
                    exclusionListClause.Append(listItem.AccountID.ToString() + "," + listItem.AccountTypeID.ToString());
                    exclusionListClause.Append("),");                    
                }

                exclusionListClause.Append(")");

                if (exclusionlist.Count > 0)
                    exclusionListClause.Remove(exclusionListClause.Length - 2, 1);
                else
                {
                    exclusionListClause.Clear();
                    exclusionListClause.Append("((0,0))");
                }

                command.CommandType = CommandType.Text;
                command.CommandText = @" select " +
                                        " accounttypename, " +
                                        " accounttypeid, " +
                                        " accountid, " +
                                        " lastname, " +
                                        " middlename, " +
                                        " firstname, " +
                                        "  birthdate, " +
                                        " city, " +
                                        " state, " +
                                        " zip " +
                                        " from " +
                                        "  ( " +
                                        "     select " +
                                        " 'administrator' as accounttypename, " +
                                        " 1 as accounttypeid, " +
                                        "  a.AdminID as accountid, " +
                                        " i.LastName, " +
                                        " i.MiddleName, " +
                                        " i.FirstName, " +
                                        " i.Birthdate, " +
                                        " i.City, " +
                                        " i.State, " +
                                        " i.Zip " +
                                        " from admins a " +
                                        " inner " +
                                        " join individuals i on i.IndividualID = a.IndividualID " +
                                        " where a.IsDeleted = 0 " +
                                        " UNION ALL " +
                                        " select " +
                                        " 'client' as accounttypename, " +
                                        " 3 as accounttypeid, " +
                                        " c.ClientID as accountid, " +
                                        " i.LastName, " +
                                        " i.MiddleName, " +
                                        " i.FirstName, " +
                                        " i.Birthdate, " +
                                        " i.City, " +
                                        " i.State, " +
                                        " i.Zip " +
                                        "     from clients c " +
                                        " inner " +
                                        "     join individuals i on i.IndividualID = c.IndividualID " +
                                        " inner join users u on c.UserID = u.UserID " +
                                        " where c.IsDeleted = 0 AND u.IsActive = 1 " +
                                        " UNION ALL " +
                                        " select " +
                                        " 'donor' as accounttypename, " +
                                        "  2 as accounttypeid, " +
                                        " d.DonorID as accountid, " +
                                        " i.LastName, " +
                                        " i.MiddleName, " +
                                        " i.FirstName, " +
                                        " i.Birthdate, " +
                                        " i.City, " +
                                        " i.State, " +
                                        " i.Zip " +
                                        "     from donors d " +
                                        " inner " +
                                        "     join individuals i on i.IndividualID = d.IndividualID " +                                        
                                        "  where d.IsDeleted = 0) it0 " +
                                        " where(it0.accountid, it0.accounttypeid) NOT IN  " + exclusionListClause.ToString();                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayavailablemessagerecipient = new displayavailablemessagerecipient();

                    adisplayavailablemessagerecipient.AccountTypeName = reader.GetString(0);
                    adisplayavailablemessagerecipient.AccountTypeID = reader.GetInt64(1);
                    adisplayavailablemessagerecipient.AccountID = reader.GetInt64(2);
                    adisplayavailablemessagerecipient.LastName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    adisplayavailablemessagerecipient.MiddleName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    adisplayavailablemessagerecipient.FirstName = reader.IsDBNull(5) ? "" : reader.GetString(5);
                    adisplayavailablemessagerecipient.BirthDate = reader.IsDBNull(6) ? DateTime.Now : reader.GetDateTime(6);
                    adisplayavailablemessagerecipient.City = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    adisplayavailablemessagerecipient.State = reader.IsDBNull(8) ? "" : reader.GetString(8);
                    adisplayavailablemessagerecipient.Zip = reader.IsDBNull(9) ? "" : reader.GetString(9);

                    ret.Add(adisplayavailablemessagerecipient);
                }
                reader.Close();
            }

            return ret;
                   
        }

        public displayavailablemessagerecipient RetrieveMessageRecipientByAccountIDAndAccountTypeID(
            long accountId, long accountTypeId)
        {
            var ret = new displayavailablemessagerecipient();

            using (var command = _context.CreateCommand())
            {              
                command.CommandType = CommandType.Text;
                command.CommandText = @" select " +
                                        " accounttypename, " +
                                        " accounttypeid, " +
                                        " accountid, " +
                                        " lastname, " +
                                        " middlename, " +
                                        " firstname, " +
                                        "  birthdate, " +
                                        " city, " +
                                        " state, " +
                                        " zip " +
                                        " from " +
                                        "  ( " +
                                        "     select " +
                                        " 'administrator' as accounttypename, " +
                                        " 1 as accounttypeid, " +
                                        "  a.AdminID as accountid, " +
                                        " i.LastName, " +
                                        " i.MiddleName, " +
                                        " i.FirstName, " +
                                        " i.Birthdate, " +
                                        " i.City, " +
                                        " i.State, " +
                                        " i.Zip " +
                                        " from admins a " +
                                        " inner " +
                                        " join individuals i on i.IndividualID = a.IndividualID " +
                                        " where a.IsDeleted = 0 " +
                                        " UNION ALL " +
                                        " select " +
                                        " 'client' as accounttypename, " +
                                        " 3 as accounttypeid, " +
                                        " c.ClientID as accountid, " +
                                        " i.LastName, " +
                                        " i.MiddleName, " +
                                        " i.FirstName, " +
                                        " i.Birthdate, " +
                                        " i.City, " +
                                        " i.State, " +
                                        " i.Zip " +
                                        "     from clients c " +
                                        " inner " +
                                        "     join individuals i on i.IndividualID = c.IndividualID " +
                                        " where c.IsDeleted = 0 " +
                                        " UNION ALL " +
                                        " select " +
                                        " 'donor' as accounttypename, " +
                                        "  2 as accounttypeid, " +
                                        " d.DonorID as accountid, " +
                                        " i.LastName, " +
                                        " i.MiddleName, " +
                                        " i.FirstName, " +
                                        " i.Birthdate, " +
                                        " i.City, " +
                                        " i.State, " +
                                        " i.Zip " +
                                        "     from donors d " +
                                        " inner " +
                                        "     join individuals i on i.IndividualID = d.IndividualID " +
                                        "  where d.IsDeleted = 0) it0 " +
                                        " where it0.accountid = @accountId AND it0.accounttypeid = @accountTypeId";
                var accountIdParam = command.CreateParameter();
                accountIdParam.ParameterName = "@accountId";
                accountIdParam.Value = accountId;
                command.Parameters.Add(accountIdParam);
                var accountTypeIdParam = command.CreateParameter();
                accountTypeIdParam.ParameterName = "@accountTypeId";
                accountTypeIdParam.Value = accountTypeId;
                command.Parameters.Add(accountTypeIdParam);                
                var reader = command.ExecuteReader();
                while (reader.Read())
                { 
                    ret.AccountTypeName = reader.GetString(0);
                    ret.AccountTypeID = reader.GetInt64(1);
                    ret.AccountID = reader.GetInt64(2);
                    ret.LastName = reader.IsDBNull(3) ? "" : reader.GetString(3);
                    ret.MiddleName = reader.IsDBNull(4) ? "" : reader.GetString(4);
                    ret.FirstName = reader.IsDBNull(5) ? "" : reader.GetString(5);
                    ret.BirthDate = reader.IsDBNull(6) ? DateTime.Now : reader.GetDateTime(6);
                    ret.City = reader.IsDBNull(7) ? "" : reader.GetString(7);
                    ret.State = reader.IsDBNull(8) ? "" : reader.GetString(8);
                    ret.Zip = reader.IsDBNull(9) ? "" : reader.GetString(9);                    
                }
                reader.Close();
            }

            return ret;

        }

        public displaycontactinformation RetrieveContactInformationForSystemAccount(long accountId, long accountTypeId)
        {
            var ret = new displaycontactinformation();

            var sql = "";

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;

                switch (accountTypeId)
                {
                    case 1: // admin
                        sql = " SELECT " +
                              " ns.EnableEmailMessages, " +
                              " ns.EnableSMSMessages, " +
                              " i.MobilePhoneNumber, " +
                              " u.Email " +
                              "     FROM admins a " +
                              " INNER JOIN individuals i on a.IndividualID = i.IndividualID " +
                              " LEFT OUTER JOIN notificationsettings ns on ns.AccountID = a.AdminID " +
                              " INNER JOIN users u on u.UserID = a.UserID " +
                              " WHERE " +
                              " a.AdminID = @accountId";
                        break;
                    case 2: // donor
                        sql = " SELECT " +
                              " ns.EnableEmailMessages, " +
                              " ns.EnableSMSMessages, " +
                              " i.MobilePhoneNumber, " +
                              " u.Email " +
                              "     FROM donors d " +
                              " INNER JOIN individuals i on d.IndividualID = i.IndividualID " +
                              " LEFT OUTER JOIN notificationsettings ns on ns.AccountID = d.DonorID " +
                              " INNER JOIN users u on u.UserID = d.UserID " +
                              " WHERE " +
                              " d.DonorID = @accountId";
                        break;
                    case 3: // client
                        sql = " SELECT " +
                              " ns.EnableEmailMessages, " +
                              " ns.EnableSMSMessages, " +
                              " i.MobilePhoneNumber, " +
                              " u.Email " +
                              "     FROM clients c " +
                              " INNER JOIN individuals i on c.IndividualID = i.IndividualID " +
                              " LEFT OUTER JOIN notificationsettings ns on ns.AccountID = c.ClientID " +
                              " INNER JOIN users u on u.UserID = c.UserID " +
                              " WHERE " +
                              " c.ClientID = @accountId";
                        break;
                }

                command.CommandText = sql;
                var accountIdParam = command.CreateParameter();
                accountIdParam.ParameterName = "@accountId";
                accountIdParam.Value = accountId;
                command.Parameters.Add(accountIdParam);                
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        ret.EnableEmailMessages = true;
                    }
                    else
                    {
                        ret.EnableEmailMessages = reader.GetInt16(0) != 0;
                    }

                    if (reader.IsDBNull(1))
                    {
                        ret.EnableSmsMessages = true;
                    }
                    else
                    {
                        ret.EnableSmsMessages = reader.GetInt16(1) != 0;
                    }

                    ret.MobilePhoneNumber = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    ret.EmailAddress = reader.IsDBNull(3) ? "" : reader.GetString(3);
                }

                reader.Close();
            }            

            return ret; 
        }

        public displaynotificationsettings RetrieveNotificationSettingsByAccountIdAndAccountTypeId(long accountId,
            long accountTypeId)
        {
            var ret = new displaynotificationsettings();

            ret.EmailMessagesEnabled = true;
            ret.SmsMessagesEnabled = true;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText =
                    @" select EnableEmailMessages, EnableSMSMessages FROM notificationsettings ns WHERE " +
                    " ns.AccountID = @accountId AND ns.AccountTypeID = @accountTypeId ";
                var accountIdParam = command.CreateParameter();
                accountIdParam.ParameterName = "@accountId";
                accountIdParam.Value = accountId;
                command.Parameters.Add(accountIdParam);
                var accountTypeIdParam = command.CreateParameter();
                accountTypeIdParam.ParameterName = "@accountTypeId";
                accountTypeIdParam.Value = accountTypeId;
                command.Parameters.Add(accountTypeIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (reader.IsDBNull(0))
                    {
                        ret.EmailMessagesEnabled = true;
                    }
                    else
                    {
                        ret.EmailMessagesEnabled = reader.GetInt16(0) != 0;
                    }

                    if (reader.IsDBNull(1))
                    {
                        ret.SmsMessagesEnabled = true;
                    }
                    else
                    {
                        ret.SmsMessagesEnabled = reader.GetInt16(1) != 0;
                    }
                }
                reader.Close();
            }            

            return ret;
        }

        public bool SaveNotificationSettingsByAccountIdAndAccountTypeId(long accountId, long accountTypeId,
            displaynotificationsettings notifysettings)
        {
            var ret = false;

            Int64 notificationSettingID = -1;

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText =
                    @" select * FROM notificationsettings ns WHERE " +
                    " ns.AccountID = @accountId AND ns.AccountTypeID = @accountTypeId ";
                var accountIdParam = command.CreateParameter();
                accountIdParam.ParameterName = "@accountId";
                accountIdParam.Value = accountId;
                command.Parameters.Add(accountIdParam);
                var accountTypeIdParam = command.CreateParameter();
                accountTypeIdParam.ParameterName = "@accountTypeId";
                accountTypeIdParam.Value = accountTypeId;
                command.Parameters.Add(accountTypeIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    notificationSettingID = reader.GetInt64(0);                    
                }
                reader.Close();
            }

            var notifyRepo = new notificationsettingsRepository(_context);

            if (-1 == notificationSettingID) // add record with settings
            {
                notifyRepo.Create(new notificationsettings()
                {
                    EnableEmailMessages = notifysettings.EmailMessagesEnabled ? 1 : 0,
                    EnableSMSMessages = notifysettings.SmsMessagesEnabled ? 1 : 0,
                    AccountID = accountId,
                    AccountTypeID = accountTypeId
                });
            }
            else // update current record with settings
            {
                notifyRepo.Update(new notificationsettings()
                {
                    NotificationSettingID = notificationSettingID,
                    EnableEmailMessages = notifysettings.EmailMessagesEnabled ? 1 : 0,
                    EnableSMSMessages = notifysettings.SmsMessagesEnabled ? 1 : 0,
                    AccountID = accountId,
                    AccountTypeID = accountTypeId
                });
            }

            ret = true;

            return ret;
        }

        public List<displaychild> RetrieveAllChildrenByParentClientID(long parentClientId)
        {
            var ret = new List<displaychild>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" SELECT c.ClientID, i.LastName, i.MiddleName, i.FirstName " +
                                       " FROM individuals i  " +
                                       "  INNER JOIN clients c ON c.IndividualID = i.IndividualID " +
                                       "  WHERE FatherIndividualID IN  " +
                                       "  (SELECT individualid FROM clients WHERE clientid = @clientId) " +
                                       "  OR MotherIndividualID IN " +
                                       "  (SELECT individualid FROM clients WHERE clientid = @clientId) ";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = parentClientId;
                command.Parameters.Add(clientIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplaychild = new displaychild();

                    adisplaychild.clientId = reader.GetInt64(0);
                    adisplaychild.LastName = reader.IsDBNull(1) ? "" : reader.GetString(1);
                    adisplaychild.MiddleName = reader.IsDBNull(2) ? "" : reader.GetString(2);
                    adisplaychild.FirstName = reader.IsDBNull(3) ? "" : reader.GetString(3);

                    ret.Add(adisplaychild);
                }
                reader.Close();
            }

            return ret;
        }
        public List<programs> RetrieveAllProgramsChildIsEnlistedIn(long childClientId)
        {
            var ret = new List<programs>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" SELECT p.ProgramID, p.`Name`, p.Description, " +
                                       "  p.IsPublished, p.`Year` FROM " +
                                      "   clientprogramenlistments cpe " +
                                       "  INNER JOIN programs p on p.ProgramID = cpe.ProgramID " +
                                      "   WHERE cpe.ClientID = @clientId ";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = childClientId;
                command.Parameters.Add(clientIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var itemtoadd = new programs();
                    itemtoadd.ProgramID = reader.GetInt64(0);
                    itemtoadd.Name = reader.GetString(1);
                    itemtoadd.Description = reader.GetString(2);
                    itemtoadd.IsPublished = reader.GetInt64(3);
                    itemtoadd.Year = reader.GetInt64(4);
                    ret.Add(itemtoadd);
                }
                reader.Close();
            }

            return ret;
        }
        public List<clientrequests> RetrieveAllNonDonorCommittedClientRequestsByClientIdAndProgramId(long clientId, long programId)
        {
            var ret = new List<clientrequests>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" SELECT cr.ClientRequestID, cr.RequestInformation 
                                        FROM clientrequests cr 
                                        WHERE clientid = @clientId and ProgramID = @programId
                                        AND cr.ClientRequestID NOT IN
                                        (SELECT clientrequestid FROM programdonorcommitments pdc) ";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var programIdParam = command.CreateParameter();
                programIdParam.ParameterName = "@programId";
                programIdParam.Value = programId;
                command.Parameters.Add(programIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var itemtoadd = new clientrequests();
                    itemtoadd.ClientRequestID = reader.GetInt64(0);
                    itemtoadd.RequestInformation = reader.GetString(1);                    
                    ret.Add(itemtoadd);
                }
                reader.Close();
            }

            return ret;
        }
        public List<clientrequests> RetrieveAllDonorCommittedClientRequestsByClientIdAndProgramId(long clientId, long programId)
        {
            var ret = new List<clientrequests>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" SELECT cr.ClientRequestID, cr.RequestInformation 
                                        FROM clientrequests cr 
                                        WHERE clientid = @clientId and ProgramID = @programId
                                        AND cr.ClientRequestID IN
                                        (SELECT clientrequestid FROM programdonorcommitments pdc) ";
                var clientIdParam = command.CreateParameter();
                clientIdParam.ParameterName = "@clientId";
                clientIdParam.Value = clientId;
                command.Parameters.Add(clientIdParam);
                var programIdParam = command.CreateParameter();
                programIdParam.ParameterName = "@programId";
                programIdParam.Value = programId;
                command.Parameters.Add(programIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var itemtoadd = new clientrequests();
                    itemtoadd.ClientRequestID = reader.GetInt64(0);
                    itemtoadd.RequestInformation = reader.GetString(1);
                    ret.Add(itemtoadd);
                }
                reader.Close();
            }

            return ret;
        }

        public List<displayclientrequest> RetrieveAllNonDonorCommittedClientRequestsByProgramID(long programId)
        {
            var ret = new List<displayclientrequest>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" select cr.ClientRequestID, cr.RequestInformation, p.`Name`, p.Description, p.`Year`, " +
                                      " i.LastName, i.MiddleName, i.FirstName, i.Birthdate, i.city, i.Zip " +
                                        "   from clientrequests cr  " +
                                        "  INNER JOIN programs p on p.ProgramID = cr.ProgramID " +
                                        "  INNER JOIN clients c on c.ClientID = cr.ClientID " +
                                        "  INNER JOIN individuals i on i.IndividualID = c.IndividualID " +
                                        "  WHERE  p.ProgramID = @programId AND " +
                                        "  cr.ClientRequestID NOT IN (SELECT pdc.ClientRequestID from programdonorcommitments pdc) ";
                var programIdParam = command.CreateParameter();
                programIdParam.ParameterName = "@programId";
                programIdParam.Value = programId;
                command.Parameters.Add(programIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayclientrequest = new displayclientrequest();

                    adisplayclientrequest.ClientRequestID = reader.GetInt64(0);
                    adisplayclientrequest.ClientRequest = reader.GetString(1);
                    adisplayclientrequest.ProgramName = reader.GetString(2);
                    adisplayclientrequest.ProgramDescription = reader.GetString(3);
                    adisplayclientrequest.ProgramYear = reader.GetInt64(4);
                    adisplayclientrequest.ClientLastName = reader.GetString(5);
                    adisplayclientrequest.ClientMiddleName = reader.GetString(6);
                    adisplayclientrequest.ClientFirstName = reader.GetString(7);
                    adisplayclientrequest.ClientBirthDate = reader.GetDateTime(8);
                    adisplayclientrequest.ClientCity = reader.GetString(9);
                    adisplayclientrequest.ClientZip = reader.GetString(10);

                    ret.Add(adisplayclientrequest);
                }
                reader.Close();
            }

            return ret;
        }
        public List<displayclientrequest> RetrieveAllclientRequestsCommittedToByDonorID(long donorId)
        {
            var ret = new List<displayclientrequest>();

            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = @" SELECT " +
                               " cr.ClientRequestID, " +
                               "  cr.RequestInformation, " +
                               "  p.Name, " +
                               "  p.Description, " +
                               "  p.Year, " +
                               "  i.LastName,  " +
                               "  i.MiddleName, " +
                               "  i.FirstName, " +
                               "  i.Birthdate, " +
                               "  i.City, " +
                               "  i.Zip  " +
                               "  from clientrequests cr   " +
                               "  INNER JOIN programs p on p.ProgramID = cr.ProgramID  " +
                               "  INNER JOIN clients c on c.ClientID = cr.ClientID " +
                               "  INNER JOIN individuals i on i.IndividualID = c.IndividualID " +
                               "  WHERE  cr.ClientRequestID IN (SELECT pdc.ClientRequestID from programdonorcommitments pdc " +
                               "   WHERE pdc.DonorID = @donorId); ";
                var donorIdParam = command.CreateParameter();
                donorIdParam.ParameterName = "@donorId";
                donorIdParam.Value = donorId;
                command.Parameters.Add(donorIdParam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var adisplayclientrequest = new displayclientrequest();

                    adisplayclientrequest.ClientRequestID = reader.GetInt64(0);                    
                    adisplayclientrequest.ClientRequest = reader.GetString(1);
                    adisplayclientrequest.ProgramName = reader.GetString(2);
                    adisplayclientrequest.ProgramDescription = reader.GetString(3);
                    adisplayclientrequest.ProgramYear = reader.GetInt64(4);
                    adisplayclientrequest.ClientLastName = reader.GetString(5);
                    adisplayclientrequest.ClientMiddleName = reader.GetString(6);
                    adisplayclientrequest.ClientFirstName = reader.GetString(7);
                    adisplayclientrequest.ClientBirthDate = reader.GetDateTime(8);
                    adisplayclientrequest.ClientCity = reader.GetString(9);
                    adisplayclientrequest.ClientZip = reader.GetString(10);

                    ret.Add(adisplayclientrequest);
                }
                reader.Close();
            }

            return ret;
        }

        public string RetrieveWelcomeMessageByLoggedInUserID(long loggedInUserID)
        {            
            var displayAccount = RetrieveDisplayAccountByLoggedInUserID(loggedInUserID);

            var clientrepo = new clientsRepository(_context);
            var donorrepo = new donorsRepository(_context);
            var adminrepo = new adminsRepository(_context);
            var indrepo = new individualsRepository(_context);
            var usersrepo = new usersRepository(_context);

            var individualid = -1l;

            switch (displayAccount.FromAccountTypeID)
            {
                case 1:  // admin
                    var admin = adminrepo.RetrieveByID(displayAccount.FromAccountID);
                    individualid = admin.IndividualID;
                    break;
                case 2: // donor
                    var donor = donorrepo.RetrieveByID(displayAccount.FromAccountID);
                    individualid = donor.IndividualID;
                    break;
                case 3: // client
                    var client = clientrepo.RetrieveByID(displayAccount.FromAccountID);
                    individualid = client.IndividualID;
                    break;
            }

            var ind = indrepo.RetrieveByID(individualid);

            if (!string.IsNullOrEmpty(ind.FirstName))
                return ind.FirstName;

            if (!string.IsNullOrEmpty(ind.LastName))
                return ind.LastName;

            var user = usersrepo.RetrieveByID(loggedInUserID);

            if (!string.IsNullOrEmpty(user.Email))
                return user.Email;

            return string.Empty;            
        }
    }    
}
