using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace TURN.Models
{
    public class ClientService
    {
        private readonly string _connectionString;

        // Constructor to initialize the connection string
        public ClientService(string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException(nameof(connectionString), "Connection string cannot be null or empty.");

            _connectionString = connectionString;
        }

        public void InsertClient(string FirstName, string MiddleName, string lastName, string suffix, DateTime dateOfBirth,
                                 string gender, string contactNumber, string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_SaveClient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@FirstName", string.IsNullOrWhiteSpace(FirstName) ? DBNull.Value : FirstName);
                        command.Parameters.AddWithValue("@MiddleName", string.IsNullOrWhiteSpace(MiddleName) ? DBNull.Value : MiddleName);
                        command.Parameters.AddWithValue("@LastName", string.IsNullOrWhiteSpace(lastName) ? DBNull.Value : lastName);
                        command.Parameters.AddWithValue("@Suffix", string.IsNullOrWhiteSpace(suffix) ? DBNull.Value : suffix);
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth == default ? DBNull.Value : dateOfBirth);
                        command.Parameters.AddWithValue("@Gender", string.IsNullOrWhiteSpace(gender) ? DBNull.Value : gender);
                        command.Parameters.AddWithValue("@ContactNumber", string.IsNullOrWhiteSpace(contactNumber) ? DBNull.Value : contactNumber);
                        command.Parameters.AddWithValue("@Email", string.IsNullOrWhiteSpace(email) ? DBNull.Value : email);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw new Exception("An error occurred while inserting the client data.", ex);
            }
        }

        public List<ClientModel> GetClients()
        {
            var clients = new List<ClientModel>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("sp_GetClients", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                clients.Add(new ClientModel
                                {
                                    ClientID = reader.GetInt32(0),
                                    FirstName = reader.GetString(1),
                                    MiddleName = reader.IsDBNull(2) ? null : reader.GetString(2),
                                    LastName = reader.GetString(3),
                                    Suffix = reader.IsDBNull(4) ? null : reader.GetString(4),
                                    DateOfBirth = reader.GetDateTime(5),
                                    Gender = reader.GetString(6),
                                    ContactNumber = reader.GetString(7),
                                    Email = reader.GetString(8)
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"SQL Error: {ex.Message}");
                throw;
            }

            return clients;
        }

        public void UpdateClient(int clientId, string firstName, string middleName, string lastName, string suffix,
                          DateTime? dateOfBirth, string gender, string contactNumber, string email)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_UpdateClient", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ClientID", clientId);
                command.Parameters.AddWithValue("@FirstName", firstName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@MiddleName", middleName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@LastName", lastName ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Suffix", suffix ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Gender", gender ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@ContactNumber", contactNumber ?? (object)DBNull.Value);
                command.Parameters.AddWithValue("@Email", email ?? (object)DBNull.Value);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteClient(int clientId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("sp_DeleteClient", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@ClientID", clientId);

                command.ExecuteNonQuery();
            }
        }


    }
}
