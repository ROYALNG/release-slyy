using GHIBMS.Common;
using GHIBMS.Interface;
using Modbus.Data;
using Modbus.Device;
using Modbus.Message;
using Modbus.Unme.Common;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace GHIBMS.Server.Controller
{
    /*
     *变量的UniteAddress,Modbus Server 寄存器地址，从1开始，要求地址间隔4个字，1，5，9
     *变量数据类型可以设置为Double,Modscan读时设置，配置->显示选项->双精度浮点数->最不重要的寄存器优先
     */
    public class ModbusSlaveController
    {
        private Thread _thread;
        private ModbusSlave _modbusSlave;
        private TcpListener _listener;
        private IPAddress _ip;
        private DataStore _dataStore;
        public event CommMsgDelegate OnCommMsg;
        public void SendCommEvent(Severity severity, CommunicationEvent commMsgType, string wParamm, string lParamm)
        {
            if (OnCommMsg != null)
                OnCommMsg(this, severity, commMsgType, wParamm, lParamm);
        }
        public void Start()
        {
            try
            {
                if (ServerConfig.ModbusServerEnable)
                {
                    _dataStore = DataStoreFactory.CreateDefaultDataStore();
                    _ip = new IPAddress(new byte[] { 0, 0, 0, 0 }); //new IPAddress(new byte[] { 127, 0, 0, 1 });
                    _listener = new TcpListener(_ip, ServerConfig.ModbusServerPort);

                    _modbusSlave = ModbusTcpSlave.CreateTcp(1, _listener);
                    _modbusSlave.DataStore = _dataStore;
                    _modbusSlave.ModbusSlaveRequestReceived += requestReceiveHandler;

                    _thread = new Thread(_modbusSlave.Listen) { Name = ServerConfig.ModbusServerPort.ToString() };
                    _thread.Start();
                    SendCommEvent(Severity.信息, CommunicationEvent.COMM_INFO, "系统用户", "Modbus Slave Server启动成功！");
                    Logger.GetInstance().LogMsg("Modbus Slave Server启动成功！");

                }
            }
            catch (Exception)
            {

            }

        }
        public void Stop()
        {
            try
            {

                _modbusSlave.Dispose();
                //_listener.Stop();
                _thread.Interrupt();
            }
            catch
            {

            }
        }
        private void requestReceiveHandler(object sender, ModbusSlaveRequestEventArgs e)//收到请求
        {
            try
            {
                //待完成
                //Debug.WriteLine("Modbus Slave收到写Modbus寄存器！");
                IModbusMessage message = e.Message;


                if (message.FunctionCode == 6)//6是写单个寄存器
                {
                    Console.WriteLine(e.Message);
                    WriteSingleRegisterRequestResponse rsp = new WriteSingleRegisterRequestResponse();
                    rsp.Initialize(message.MessageFrame);
                    int addr = rsp.StartAddress + 1;
                    ushort data = rsp.Data.Take(1).ToArray()[0];
                    byte[] values = new byte[2];
                    values[1] = (byte)(data >> 8);
                    values[0] = (byte)(data & 0x00FF);
                    IVariable var = getVriableByAddr(addr);
                    object value;
                    if (var != null)
                    {
                        switch (var.ValueType)
                        {
                            case TypeCode.SByte:
                            case TypeCode.Int16:
                            case TypeCode.Int32:
                            case TypeCode.UInt64:
                                value = BitConverter.ToInt16(values, 0);
                                break;
                            default:
                                value = BitConverter.ToUInt16(values, 0);
                                break;

                        }
                        if (value != null)
                            var.WriteValue(value);

                    }
                }
                else if (message.FunctionCode == 16)//16是写多个模拟量寄存器
                {
                    Console.WriteLine(e.Message);
                    WriteMultipleRegistersRequest rsp = new WriteMultipleRegistersRequest();
                    rsp.Initialize(message.MessageFrame);
                    int count = rsp.NumberOfPoints;
                    int addr = rsp.StartAddress + 1;
                    ushort[] datas = rsp.Data.Take(count).ToArray();
                    IVariable var = getVriableByAddr(addr);
                    object value;
                    if (var != null)
                    {
                        switch (var.ValueType)
                        {
                            case TypeCode.SByte:
                            case TypeCode.Int16:
                                {
                                    value = BitConverter.ToInt16(ConverterByte2(datas), 0);
                                }
                                break;

                            case TypeCode.Byte:
                            case TypeCode.UInt16:
                                {
                                    value = BitConverter.ToUInt16(ConverterByte2(datas), 0);
                                }
                                break;
                            case TypeCode.Int32:
                                {
                                    if (datas.Length >= 2)
                                    {
                                        value = BitConverter.ToInt32(ConverterByte4(datas), 0);
                                    }
                                    else
                                    {
                                        value = BitConverter.ToInt16(ConverterByte2(datas), 0);
                                    }
                                    break;
                                }
                            case TypeCode.UInt32:
                                {
                                    if (datas.Length >= 2)
                                    {
                                        value = BitConverter.ToUInt32(ConverterByte4(datas), 0);
                                    }
                                    else
                                    {
                                        value = BitConverter.ToUInt16(ConverterByte2(datas), 0);
                                    }
                                    break;
                                }
                            case TypeCode.UInt64:
                                {
                                    if (datas.Length >= 4)
                                    {
                                        value = BitConverter.ToUInt64(ConverterByte8(datas), 0);
                                    }
                                    else
                                    {
                                        value = BitConverter.ToUInt16(ConverterByte2(datas), 0);
                                    }
                                    break;
                                }
                            case TypeCode.Int64:
                                {
                                    if (datas.Length >= 4)
                                    {
                                        value = BitConverter.ToInt64(ConverterByte8(datas), 0);
                                    }
                                    else
                                    {
                                        value = BitConverter.ToInt16(ConverterByte2(datas), 0);
                                    }
                                    break;
                                }
                            case TypeCode.Single:
                                {
                                    if (datas.Length >= 2)
                                    {
                                        value = BitConverter.ToSingle(ConverterByte4(datas), 0);
                                    }
                                    else
                                    {
                                        value = BitConverter.ToInt16(ConverterByte2(datas), 0);
                                    }
                                    break;
                                }
                            case TypeCode.Double:
                                {
                                    if (datas.Length >= 4)
                                    {
                                        value = BitConverter.ToDouble(ConverterByte8(datas), 0);
                                    }
                                    else
                                    {
                                        value = BitConverter.ToInt16(ConverterByte2(datas), 0);
                                    }
                                    break;
                                }

                            default:
                                {
                                    value = BitConverter.ToUInt16(ConverterByte2(datas), 0);
                                }
                                break;

                        }
                        if (value != null)
                            var.WriteValue(value);

                    }



                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        byte[] ConverterByte2(ushort[] datas)
        {
            byte[] values = new byte[2];
            values[1] = (byte)(datas[0] >> 8);
            values[0] = (byte)(datas[0] & 0x00FF);
           return  values;
        }
        byte[] ConverterByte4(ushort[] datas)
        {
            byte[] values = new byte[4];
            values[3] = (byte)(datas[1] >> 8);
            values[2] = (byte)(datas[1] & 0x00FF);

            values[1] = (byte)(datas[0] >> 8);
            values[0] = (byte)(datas[0] & 0x00FF);
            return values;
        }
        byte[] ConverterByte8(ushort[] datas)
        {
            byte[] values = new byte[8];
            values[7] = (byte)(datas[3] >> 8);
            values[6] = (byte)(datas[3] & 0x00FF);

            values[5] = (byte)(datas[2] >> 8);
            values[4] = (byte)(datas[2] & 0x00FF);

            values[3] = (byte)(datas[1] >> 8);
            values[2] = (byte)(datas[1] & 0x00FF);

            values[1] = (byte)(datas[0] >> 8);
            values[0] = (byte)(datas[0] & 0x00FF);
            return values;
        }
        public bool setHoldRegister(int offset, TypeCode dataType, string value)
        {
            return setValueUniverse(3, offset, dataType, value);
        }
        bool setValueUniverse(int functionCode, int offset, TypeCode dataType, string valueStr)
        {

            try
            {

                switch (dataType)
                {

                    case TypeCode.SByte:
                    case TypeCode.Int16:
                        setValue16(functionCode, offset, (ushort)pubFun.IsInt(valueStr, 0));
                        break;
                    case TypeCode.Byte:
                    case TypeCode.UInt16:
                        setValue16(functionCode, offset, (ushort)pubFun.IsUInt(valueStr, 0));
                        break;
                    case TypeCode.UInt32:
                        setValue32(functionCode, offset, (uint)pubFun.IsUInt(valueStr, 0));
                        break;
                    case TypeCode.Int32:
                        setValue32(functionCode, offset, (int)pubFun.IsInt(valueStr, 0));
                        break;
                    case TypeCode.Single:

                        setValue32(functionCode, offset, pubFun.IsSingle(valueStr, 0));
                        break;
                    case TypeCode.Double:
                        setValue64(functionCode, offset, pubFun.IsDouble(valueStr, 0));
                        break;
                    default:
                        setValue16(functionCode, offset, (ushort)pubFun.IsUInt(valueStr, 0));
                        break;
                }
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public void setValue16(int functionCode, int offset, ushort value)
        {
            ModbusDataCollection<ushort> data = getRegisterGroup(functionCode);
            data[offset] = value;
        }
        public void setValue32(int functionCode, int offset, uint value)
        {
            byte[] valueBuf = BitConverter.GetBytes(value);
            ushort lowOrderValue = BitConverter.ToUInt16(valueBuf, 0);
            ushort highOrderValue = BitConverter.ToUInt16(valueBuf, 2);

            ModbusDataCollection<ushort> data = getRegisterGroup(functionCode);
            data[offset] = lowOrderValue;
            data[offset + 1] = highOrderValue;
        }
        public void setValue32(int functionCode, int offset, int value)
        {
            byte[] valueBuf = BitConverter.GetBytes(value);
            ushort lowOrderValue = BitConverter.ToUInt16(valueBuf, 0);
            ushort highOrderValue = BitConverter.ToUInt16(valueBuf, 2);

            ModbusDataCollection<ushort> data = getRegisterGroup(functionCode);
            data[offset] = lowOrderValue;
            data[offset + 1] = highOrderValue;
        }
        public void setValue32(int functionCode, int offset, float value)
        {
            ushort lowOrderValue = BitConverter.ToUInt16(BitConverter.GetBytes(value), 0);
            ushort highOrderValue = BitConverter.ToUInt16(BitConverter.GetBytes(value), 2);
            ModbusDataCollection<ushort> data = getRegisterGroup(functionCode);
            data[offset] = lowOrderValue;
            data[offset + 1] = highOrderValue;
        }
        public void setValue64(int functionCode, int offset, double value)
        {
            byte[] valueBuf = BitConverter.GetBytes(value);
            ushort lowOrderValue = BitConverter.ToUInt16(valueBuf, 0);
            ushort highOrderValue = BitConverter.ToUInt16(valueBuf, 2);

            ushort lowOrderValue1 = BitConverter.ToUInt16(valueBuf, 4);
            ushort highOrderValue1 = BitConverter.ToUInt16(valueBuf, 6);

            ModbusDataCollection<ushort> data = getRegisterGroup(functionCode);
            data[offset] = lowOrderValue;
            data[offset + 1] = highOrderValue;
            data[offset + 2] = lowOrderValue1;
            data[offset + 3] = highOrderValue1;
        }
        public void setValue16(int functionCode, int offset, bool value)
        {
            byte[] valueBuf = BitConverter.GetBytes(value);//用1代替true
            ushort lowOrderValue = BitConverter.ToUInt16(valueBuf, 0);

            ModbusDataCollection<ushort> data = getRegisterGroup(functionCode);
            data[offset] = lowOrderValue;

        }
        ModbusDataCollection<ushort> getRegisterGroup(int functionCode)//根据3或4返回适合的寄存器
        {

            switch (functionCode)
            {
                case 3: return _dataStore.HoldingRegisters; //可由moddbus修改
                case 4: return _dataStore.InputRegisters;   //不可通过modbus修改
                default: return _dataStore.InputRegisters;
            }
        }
        IVariable getVriableByAddr(int addr)
        {
            foreach(IChannel ch in Rtdb.ChanList)
                foreach(IController con in ch.ConList)
                    foreach(IVariable var in con.VarList)
                    {
                        if (addr == var.UniteAddress)
                            return var;
                    }
            return null;
        }
    }
}
