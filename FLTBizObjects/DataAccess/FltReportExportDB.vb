' Copyright (c) 2026 CODE Éxitos. All rights reserved.
' Athanor Workshop License 1.0 - see LICENSE at the repository root. Not open source.
Imports System
Imports System.Data
Imports System.Data.Common
Imports System.Data.SqlClient
Imports Contoso.Utilities
Imports FLTBizObjects.FLT.BO
Imports FLTBizObjects.FLT.BL

Namespace FLT.Dal

    Public Class FltReportExportDB

        Private Shared _commandTimeout As Integer = 600

        Public Shared Function GetDailyUsage(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal FlightDate As Date) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptDailyUsage", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@Locationid", LocationId)
                    myDbCommand.Parameters.AddWithValue("@FlightDate", FlightDate)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetPilotComments(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptPilotComments", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@Locationid", LocationId)
                    myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
                    myDbCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
                    myDbCommand.Parameters.AddWithValue("@DateThru", DateThru)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetFuelBurn(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal LiquidGroupId As Integer) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptFuelBurn", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
                    myDbCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
                    myDbCommand.Parameters.AddWithValue("@DateThru", DateThru)
                    myDbCommand.Parameters.AddWithValue("@LiquidGroupId", LiquidGroupId)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetAircraftComments(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal ComponentId As Integer, ByVal RatingLow As Int16, ByVal RatingHigh As Int16, ByVal DateFrom As Date, ByVal DateThru As Date) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptAircraftComments", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@Locationid", LocationId)
                    myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
                    myDbCommand.Parameters.AddWithValue("@ComponentId", ComponentId)
                    myDbCommand.Parameters.AddWithValue("@RatingLow", RatingLow)
                    myDbCommand.Parameters.AddWithValue("@RatingHigh", RatingHigh)
                    myDbCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
                    myDbCommand.Parameters.AddWithValue("@DateThru", DateThru)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetAircraftData(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptAircraftData", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@Locationid", LocationId)
                    myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetFlightData(ByVal ProgId As Integer, ByVal LocationId As Integer, ByVal AircraftId As Integer, ByVal DateRangeStart As Date, ByVal DateRangeEnd As Date, ByVal FuelLiquidGroupId As Integer, ByVal OilLiquidGroupId As Integer, ByVal FluidsLiquidGroupId As Integer) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptFlightData", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@Locationid", LocationId)
                    myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
                    myDbCommand.Parameters.AddWithValue("@DateRangeStart", DateRangeStart)
                    myDbCommand.Parameters.AddWithValue("@DateRangeEnd", DateRangeEnd)
                    myDbCommand.Parameters.AddWithValue("@FuelLiquidGroupId", FuelLiquidGroupId)
                    myDbCommand.Parameters.AddWithValue("@OilLiquidGroupId", OilLiquidGroupId)
                    myDbCommand.Parameters.AddWithValue("@FluidsLiquidGroupId", FluidsLiquidGroupId)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetFlightStatistics(ByVal ProgId As Integer, ByVal DateRangeStart As Date, ByVal DateRangeEnd As Date) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptFlightStatistics", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@DateBegin", Format(DateRangeStart, "yyyy-MM-dd"))
                    myDbCommand.Parameters.AddWithValue("@DateEnd", Format(DateRangeEnd, "yyyy-MM-dd"))

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetFlightOutliers(ByVal ProgId As Integer, ByVal DateRangeStart As Date, ByVal DateRangeEnd As Date, ByVal NbrOfDeviations As Integer) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptFlightOutliers", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@DateBegin", Format(DateRangeStart, "yyyy-MM-dd"))
                    myDbCommand.Parameters.AddWithValue("@DateEnd", Format(DateRangeEnd, "yyyy-MM-dd"))
                    myDbCommand.Parameters.AddWithValue("@NbrOfStDevs", NbrOfDeviations)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Public Shared Function GetFuelBurnByRefill(ByVal ProgId As Integer, ByVal AircraftId As Integer, ByVal DateFrom As Date, ByVal DateThru As Date, ByVal LiquidGroupId As Integer) As DataTable

            Dim myDataTable As DataTable = New DataTable

            Using myDbConnection As SqlConnection = New SqlConnection(AppConfiguration.connectionString)

                Using myDbCommand As SqlCommand = New SqlCommand("rptFuelBurnByRefill", myDbConnection)

                    myDbCommand.CommandType = CommandType.StoredProcedure
                    myDbCommand.CommandTimeout = _commandTimeout

                    myDbCommand.Parameters.AddWithValue("@ProgId", ProgId)
                    myDbCommand.Parameters.AddWithValue("@AircraftId", AircraftId)
                    myDbCommand.Parameters.AddWithValue("@DateFrom", DateFrom)
                    myDbCommand.Parameters.AddWithValue("@DateThru", DateThru)
                    myDbCommand.Parameters.AddWithValue("@LiquidGroupId", LiquidGroupId)

                    Try
                        myDbConnection.Open()

                        Using mySqlDataReader As SqlDataReader = myDbCommand.ExecuteReader(CommandBehavior.CloseConnection)

                            Try
                                myDataTable.Load(mySqlDataReader)

                            Catch myException As Exception
                                Logger.Write(myException, myDbCommand.CommandText)
                                Throw New ApplicationException("Application encountered a data error.")
                            End Try

                        End Using

                    Catch myException As Exception
                        Logger.Write(myException, myDbCommand.CommandText)
                        Throw New ApplicationException("Application encountered a data error.")
                    End Try

                End Using

            End Using

            Return myDataTable

        End Function

        Private Sub New()

        End Sub

    End Class

End Namespace
