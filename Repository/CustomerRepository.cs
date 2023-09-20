using PequeñoCRUD.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using PequeñoCRUD.Repository;

namespace PequeñoCRUD.Repository
{
    public class CustomerRepository: ICustomerRepository
    {
        //Conexión 
        private string StringConnection;

        public CustomerRepository(String stringConnection)
        {
            StringConnection = stringConnection;
        }

        public SqlConnection connection()
        {
            return new SqlConnection(StringConnection);
        }

        #region Crear y guardar cliente
        public async Task<bool> SaveCustomer(Customer customer)
        {
            Boolean customerCreated = false;
            SqlConnection sqlConn = connection();
            SqlCommand cmd = null;

            try
            {
                sqlConn.Open();
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = "dbo.UsersTall";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = customer.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 500).Value = customer.Email;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = customer.Phone;
                cmd.Parameters.Add("@DateTall", SqlDbType.DateTime).Value = DateTime.Now;

                await cmd.ExecuteNonQueryAsync();
                customerCreated = true;
            }
            catch (SqlException ex)
            {
                throw new Exception("No se pudieron guardar los datos del nuevo cliente " + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();
            }

            return customerCreated;
        }
		#endregion

        # region Mostrar lista de clientes
		public async Task<IEnumerable<Customer>> CustomerAlls()
        {
            List<Customer> list = new List<Customer>();
            SqlConnection sqlConn = connection();
            SqlCommand cmd = null;

            try
            {
                sqlConn.Open();
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = "dbo.UsersList";
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                while (reader.Read()) 
                {
                    Customer customer = new Customer();
                    customer.Id = Convert.ToInt32(reader["Id"]);
                    customer.Name = reader["Name"].ToString();
                    customer.Email = reader["Email"].ToString();
                    customer.Phone = reader["Phone"].ToString();
                    list.Add(customer);
                }
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al guardar los datos del cliente" + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                sqlConn.Close();
                sqlConn.Dispose();
            }
            return list;
        }
		#endregion

        # region Editar cliente
		public async Task<Customer> GetDataCustomer(int id) 
        {
            Customer c = new Customer();
            SqlConnection sqlConnection = connection();
            SqlCommand cmd = null;
            try
            {
                sqlConnection.Open();
                cmd = sqlConnection.CreateCommand();
                cmd.CommandText = "dbo.UsersList";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                SqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.Read())
                {
                    c.Id = Convert.ToInt32(reader["Id"]);
                    c.Name = reader["Name"].ToString();
                    c.Email = reader["Email"].ToString();
                    c.Phone = reader["Phone"].ToString();
                }
                reader.Close();
            }
            catch(SqlException ex) 
            {
                throw new Exception("Error al cargar el dato al cliente" + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return c;
        }

        public async Task<bool> EditCustomer(Customer customer)
        {
            Boolean customerModified = false;
            SqlConnection sqlConn = connection();
            SqlCommand cmd = null;

            try
            {
                sqlConn.Open();
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = "dbo.UsersTall";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Name", SqlDbType.VarChar, 500).Value = customer.Name;
                cmd.Parameters.Add("@Email", SqlDbType.VarChar, 500).Value = customer.Email;
                cmd.Parameters.Add("@Phone", SqlDbType.VarChar, 100).Value = customer.Phone;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = customer.Id;

                if(customer.Name != null && customer.Email != null && customer.Phone != null) 
                {
                    await cmd.ExecuteNonQueryAsync();
                }

                customerModified = true;
            }
            catch (SqlException ex) 
            {
                throw new Exception("Error cargando los datos del cliente" + ex.Message);
            }
            finally
            {

            }
            return customerModified;
        }

        #endregion

        #region Eliminar cliente 
        public async Task<bool> DeleteCustomer(int id)
        {
            Boolean customerDeleted = false;
            SqlConnection sqlConn = connection();
            SqlCommand cmd = null;

            try
            {
                sqlConn.Open();
                cmd = sqlConn.CreateCommand();
                cmd.CommandText = "dbo.DeleteCustomer";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;

                if (id > 0)
                    await cmd.ExecuteNonQueryAsync();

                customerDeleted = true;
            }
            catch(SqlException ex) 
            {
                throw new Exception("Error al borrar el cliente" + ex.Message);
            }
            finally
            {
                cmd.Dispose();
                sqlConn.Close(); 
                sqlConn.Dispose();
            }

            return customerDeleted;
        }
		#endregion

		#region Búsqueda de clientes
		public async Task<IEnumerable<Customer>> CustomerAlls(string searched)
		{
			List<Customer> list = new List<Customer>();
			SqlConnection sqlConn = connection();
			SqlCommand cmd = null;

			try
			{
				sqlConn.Open();
				cmd = sqlConn.CreateCommand();
				cmd.CommandText = "dbo.UsersList";
				cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@searched", SqlDbType.VarChar, 500).Value = searched;
				SqlDataReader reader = await cmd.ExecuteReaderAsync();
				while (reader.Read())
				{
					Customer customer = new Customer();
					customer.Id = Convert.ToInt32(reader["Id"]);
					customer.Name = reader["Name"].ToString();
					customer.Email = reader["Email"].ToString();
					customer.Phone = reader["Phone"].ToString();
					list.Add(customer);
				}
				reader.Close();
			}
			catch (SqlException ex)
			{
				throw new Exception("Error al guardar los datos del cliente" + ex.Message);
			}
			finally
			{
				cmd.Dispose();
				sqlConn.Close();
				sqlConn.Dispose();
			}
			return list;
		}
		#endregion
	}
}
