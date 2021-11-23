using Azure.Storage.Queues;
using Dapper;
using DevOpsWebApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DevOpsWebApp.Repository
{
    public class EmpRepository
    {
        public SqlConnection con;
        QueueClient queueClient = null;

        //To Handle connection related activities
        private void connection()
        {
            string secretIdentifier = ConfigurationManager.AppSettings["SqlConnectionSecretIdentifier"].ToString();
            string constr = KeyVaultHelper.GetSecretValue(secretIdentifier).Result;
            con = new SqlConnection(constr);
        }
        //To Add Employee details
        public void AddEmployee(EmployeeModel objEmp)
        {
            //Adding the employess
            try
            {
                connection();
                con.Open();
                con.Execute("AddNewEmpDetails", objEmp, commandType: CommandType.StoredProcedure);
                con.Close();
                string message = JsonSerializer.Serialize(objEmp);
                InsertMessage(message);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //To view employee details with generic list 
        public List<EmployeeModel> GetAllEmployees()
        {
            try
            {
                connection();
                con.Open();
                IList<EmployeeModel> EmpList = SqlMapper.Query<EmployeeModel>(
                                  con, "GetEmployees").ToList();
                con.Close();
                return EmpList.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //To Update Employee details
        public void UpdateEmployee(EmployeeModel objUpdate)
        {
            try
            {
                connection();
                con.Open();
                con.Execute("UpdateEmpDetails", objUpdate, commandType: CommandType.StoredProcedure);
                con.Close();
            }
            catch (Exception)
            {
                throw;
            }
        }
        //To delete Employee details
        public bool DeleteEmployee(int Id)
        {
            try
            {
                DynamicParameters param = new DynamicParameters();
                param.Add("@EmpId", Id);
                connection();
                con.Open();
                con.Execute("DeleteEmpById", param, commandType: CommandType.StoredProcedure);
                con.Close();
                return true;
            }
            catch (Exception ex)
            {
                //Log error as per your need 
                throw ex;
            }
        }

        public void InsertMessage(string message)
        {
            // Get the connection string from app settings
            string connectionString = ConfigurationManager.AppSettings["StorageConnectionString"];
            string queueName = ConfigurationManager.AppSettings["QueueName"];
            // Instantiate a QueueClient which will be used to create and manipulate the queue
            if (queueClient == null)
            {
                queueClient = new QueueClient(connectionString, queueName);
            }

            // Create the queue if it doesn't already exist
            queueClient.CreateIfNotExists();

            if (queueClient.Exists())
            {
                // Send a message to the queue
                var painTextBytes = System.Text.Encoding.UTF8.GetBytes(message); 
                 queueClient.SendMessage(Convert.ToBase64String(painTextBytes));
            }
        }
    }
}