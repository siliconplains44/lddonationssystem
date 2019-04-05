using System;
using System.Collections.Generic;

using System.Data;

using ldvdbclasslibrary;

namespace ldvdbdal
{
    public class individualsRepository : Repository<individuals>, IindividualsRepository
    {
        private DbContext _context;
        public individualsRepository(DbContext context)
            : base(context)
        {
            _context = context;
        }
        public long Create(individuals newindividuals)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "INSERT INTO individuals (LastName, MiddleName, FirstName, FatherIndividualID, MotherIndividualID, Birthdate, MobilePhoneNumber, HomePhoneNumber, AddressLine1, AddressLine2, City, State, Zip) VALUES (@LastName, @MiddleName, @FirstName, @FatherIndividualID, @MotherIndividualID, @Birthdate, @MobilePhoneNumber, @HomePhoneNumber, @AddressLine1, @AddressLine2, @City, @State, @Zip)";
                var LastNameparam = command.CreateParameter();
                LastNameparam.ParameterName = "@LastName";
                LastNameparam.Value = newindividuals.LastName;
                command.Parameters.Add(LastNameparam);
                var MiddleNameparam = command.CreateParameter();
                MiddleNameparam.ParameterName = "@MiddleName";
                MiddleNameparam.Value = newindividuals.MiddleName;
                command.Parameters.Add(MiddleNameparam);
                var FirstNameparam = command.CreateParameter();
                FirstNameparam.ParameterName = "@FirstName";
                FirstNameparam.Value = newindividuals.FirstName;
                command.Parameters.Add(FirstNameparam);
                var FatherIndividualIDparam = command.CreateParameter();
                FatherIndividualIDparam.ParameterName = "@FatherIndividualID";
                FatherIndividualIDparam.Value = newindividuals.FatherIndividualID;
                command.Parameters.Add(FatherIndividualIDparam);
                var MotherIndividualIDparam = command.CreateParameter();
                MotherIndividualIDparam.ParameterName = "@MotherIndividualID";
                MotherIndividualIDparam.Value = newindividuals.MotherIndividualID;
                command.Parameters.Add(MotherIndividualIDparam);
                var Birthdateparam = command.CreateParameter();
                Birthdateparam.ParameterName = "@Birthdate";
                Birthdateparam.Value = newindividuals.Birthdate;
                command.Parameters.Add(Birthdateparam);
                var MobilePhoneNumberparam = command.CreateParameter();
                MobilePhoneNumberparam.ParameterName = "@MobilePhoneNumber";
                MobilePhoneNumberparam.Value = newindividuals.MobilePhoneNumber;
                command.Parameters.Add(MobilePhoneNumberparam);
                var HomePhoneNumberparam = command.CreateParameter();
                HomePhoneNumberparam.ParameterName = "@HomePhoneNumber";
                HomePhoneNumberparam.Value = newindividuals.HomePhoneNumber;
                command.Parameters.Add(HomePhoneNumberparam);
                var AddressLine1param = command.CreateParameter();
                AddressLine1param.ParameterName = "@AddressLine1";
                AddressLine1param.Value = newindividuals.AddressLine1;
                command.Parameters.Add(AddressLine1param);
                var AddressLine2param = command.CreateParameter();
                AddressLine2param.ParameterName = "@AddressLine2";
                AddressLine2param.Value = newindividuals.AddressLine2;
                command.Parameters.Add(AddressLine2param);
                var Cityparam = command.CreateParameter();
                Cityparam.ParameterName = "@City";
                Cityparam.Value = newindividuals.City;
                command.Parameters.Add(Cityparam);
                var Stateparam = command.CreateParameter();
                Stateparam.ParameterName = "@State";
                Stateparam.Value = newindividuals.State;
                command.Parameters.Add(Stateparam);
                var Zipparam = command.CreateParameter();
                Zipparam.ParameterName = "@Zip";
                Zipparam.Value = newindividuals.Zip;
                command.Parameters.Add(Zipparam);
                command.ExecuteNonQuery();
                command.CommandText = "SELECT LAST_INSERT_ID(); ";
                return Convert.ToInt64(command.ExecuteScalar());
            }
        }
        public int Update(individuals existingindividuals)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "UPDATE individuals SET LastName = @LastName, MiddleName = @MiddleName, FirstName = @FirstName, FatherIndividualID = @FatherIndividualID, MotherIndividualID = @MotherIndividualID, Birthdate = @Birthdate, MobilePhoneNumber = @MobilePhoneNumber, HomePhoneNumber = @HomePhoneNumber, AddressLine1 = @AddressLine1, AddressLine2 = @AddressLine2, City = @City, State = @State, Zip = @Zip WHERE IndividualID = @IndividualID";
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = existingindividuals.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                var LastNameparam = command.CreateParameter();
                LastNameparam.ParameterName = "@LastName";
                LastNameparam.Value = existingindividuals.LastName;
                command.Parameters.Add(LastNameparam);
                var MiddleNameparam = command.CreateParameter();
                MiddleNameparam.ParameterName = "@MiddleName";
                MiddleNameparam.Value = existingindividuals.MiddleName;
                command.Parameters.Add(MiddleNameparam);
                var FirstNameparam = command.CreateParameter();
                FirstNameparam.ParameterName = "@FirstName";
                FirstNameparam.Value = existingindividuals.FirstName;
                command.Parameters.Add(FirstNameparam);
                var FatherIndividualIDparam = command.CreateParameter();
                FatherIndividualIDparam.ParameterName = "@FatherIndividualID";
                FatherIndividualIDparam.Value = existingindividuals.FatherIndividualID;
                command.Parameters.Add(FatherIndividualIDparam);
                var MotherIndividualIDparam = command.CreateParameter();
                MotherIndividualIDparam.ParameterName = "@MotherIndividualID";
                MotherIndividualIDparam.Value = existingindividuals.MotherIndividualID;
                command.Parameters.Add(MotherIndividualIDparam);
                var Birthdateparam = command.CreateParameter();
                Birthdateparam.ParameterName = "@Birthdate";
                Birthdateparam.Value = existingindividuals.Birthdate;
                command.Parameters.Add(Birthdateparam);
                var MobilePhoneNumberparam = command.CreateParameter();
                MobilePhoneNumberparam.ParameterName = "@MobilePhoneNumber";
                MobilePhoneNumberparam.Value = existingindividuals.MobilePhoneNumber;
                command.Parameters.Add(MobilePhoneNumberparam);
                var HomePhoneNumberparam = command.CreateParameter();
                HomePhoneNumberparam.ParameterName = "@HomePhoneNumber";
                HomePhoneNumberparam.Value = existingindividuals.HomePhoneNumber;
                command.Parameters.Add(HomePhoneNumberparam);
                var AddressLine1param = command.CreateParameter();
                AddressLine1param.ParameterName = "@AddressLine1";
                AddressLine1param.Value = existingindividuals.AddressLine1;
                command.Parameters.Add(AddressLine1param);
                var AddressLine2param = command.CreateParameter();
                AddressLine2param.ParameterName = "@AddressLine2";
                AddressLine2param.Value = existingindividuals.AddressLine2;
                command.Parameters.Add(AddressLine2param);
                var Cityparam = command.CreateParameter();
                Cityparam.ParameterName = "@City";
                Cityparam.Value = existingindividuals.City;
                command.Parameters.Add(Cityparam);
                var Stateparam = command.CreateParameter();
                Stateparam.ParameterName = "@State";
                Stateparam.Value = existingindividuals.State;
                command.Parameters.Add(Stateparam);
                var Zipparam = command.CreateParameter();
                Zipparam.ParameterName = "@Zip";
                Zipparam.Value = existingindividuals.Zip;
                command.Parameters.Add(Zipparam);
                return command.ExecuteNonQuery();
            }
        }
        public int Delete(individuals existingindividuals)
        {
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "DELETE FROM individuals WHERE IndividualID = @IndividualID";
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = existingindividuals.IndividualID;
                command.Parameters.Add(IndividualIDparam);
                return command.ExecuteNonQuery();
            }
        }
        public individuals RetrieveByID(long existingindividualsid)
        {
            individuals ret = null;
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM individuals WHERE IndividualID = @IndividualID";
                var IndividualIDparam = command.CreateParameter();
                IndividualIDparam.ParameterName = "@IndividualID";
                IndividualIDparam.Value = existingindividualsid;
                command.Parameters.Add(IndividualIDparam);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     ret = new individuals();
                     ret.IndividualID = reader.GetInt64(0);
                     ret.LastName = reader.IsDBNull(1) ? null : reader.GetString(1);
                     ret.MiddleName = reader.IsDBNull(2) ? null : reader.GetString(2);
                     ret.FirstName = reader.IsDBNull(3) ? null : reader.GetString(3);
                     ret.FatherIndividualID = reader.IsDBNull(4) ? (long?)null : reader.GetInt64(4);
                     ret.MotherIndividualID = reader.IsDBNull(5) ? (long?)null : reader.GetInt64(5);
                     ret.Birthdate = reader.IsDBNull(6) ? (DateTime?)null : reader.GetDateTime(6);
                     ret.MobilePhoneNumber = reader.IsDBNull(7) ? null : reader.GetString(7);
                     ret.HomePhoneNumber = reader.IsDBNull(8) ? null : reader.GetString(8);
                     ret.AddressLine1 = reader.IsDBNull(9) ? null : reader.GetString(9);
                     ret.AddressLine2 = reader.IsDBNull(10) ? null : reader.GetString(10);
                     ret.City = reader.IsDBNull(11) ? null : reader.GetString(11);
                     ret.State = reader.IsDBNull(12) ? null : reader.GetString(12);
                     ret.Zip = reader.IsDBNull(13) ? null : reader.GetString(13);
                }
                return ret;
            }
        }
        public List<individuals> RetrieveWithWhereClauseindividuals(string WhereClause)
        {
            var ret = new List<individuals>();
            using (var command = _context.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT * FROM individuals WHERE " + WhereClause;
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                     var itemtoadd = new individuals();
                     itemtoadd.IndividualID = reader.GetInt64(0);
                     itemtoadd.LastName = reader.GetString(1);
                     itemtoadd.MiddleName = reader.GetString(2);
                     itemtoadd.FirstName = reader.GetString(3);
                     itemtoadd.FatherIndividualID = reader.GetInt64(4);
                     itemtoadd.MotherIndividualID = reader.GetInt64(5);
                     itemtoadd.Birthdate = reader.GetDateTime(6);
                     itemtoadd.MobilePhoneNumber = reader.GetString(7);
                     itemtoadd.HomePhoneNumber = reader.GetString(8);
                     itemtoadd.AddressLine1 = reader.GetString(9);
                     itemtoadd.AddressLine2 = reader.GetString(10);
                     itemtoadd.City = reader.GetString(11);
                     itemtoadd.State = reader.GetString(12);
                     itemtoadd.Zip = reader.GetString(13);
                    ret.Add(itemtoadd);
                }
                return ret;
            }
        }
    }
}
