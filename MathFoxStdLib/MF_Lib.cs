using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MathFoxStdLib
{
    //Основные сведения о гибких производственных системах
    public class ProdSysFlexible
    {
        //private fields
        private int nnp_m;
        private int bs_p;

        //properties
        public int NNP_M
        {
            get
            {
                return nnp_m;
            }
            set
            {
                if (value == 0)
                {
                    nnp_m = 1;
                }
            }
        }
        public int BS_P
        {
            get
            {
                return bs_p;
            }
            set
            {
                if (value == 0)
                {
                    bs_p = 1;
                }
            }
        }

        //constructor
        public ProdSysFlexible()
        {
            nnp_m = 1;
            bs_p = 1;
        }

        public ProdSysFlexible(int m, int p)
        {
            nnp_m = m;
            bs_p = p;
        }

        //public methods
        public double Koefficient_Km(int m)
        {
            double Km = 0;
            Km = (1 - (1 / m));
            return Km;
        }
        public double Koefficient_Kn(int p)
        {
            double Kn = 0;
            Kn = 1 / p;
            return Kn;
        }
    }

    //Специализация участков на базе станков с ЧПУ
    public class CNC_BaseSites
    {
        //private fields
        private float machFreeWorkTime_Tmc;
        private float employmentTime_Tz;
        private float employmentCoefficient_Kd;

        private float totalLabor_Ti;
        private int annualRelease_N;
        private int annualWorkFund_Feq;
        private float eqLoadCoefficient_Kz;

        //Количество и удельный вес операций, выполнение которых возможно на оборудовании с ЧПУ
        private Stack<double> totalNumCNCParts; //Суммарное кол-во операций на станках с ЧПУ по тех. проц. (i=1..n) изготовления всей совокупности деталей
        private Stack<double> totalNumParts; //Суммарное кол-во операций изготовления всей совокупн. деталей

        private Stack<double> spWeightCNCIntencity_Tcnc;
        private Stack<double> spTotalIntencityWeight_To;

        private Stack<double> spWeightCNCDifIntence_Tcncj;
        private Stack<double> spTotalDifIntence_Tj;

        //Количество и удельный вес видов работ на оборудовании с ручным управлением
        //а) с учетом изготовления номенклатуры  деталей в своем подразделении (цехе, участке)
        private Stack<double> sumNumManualTech_K2; //Суммарное кол-во операций, выполняемых на оборудовании с ручным управлением 
        private Stack<double> sumNumTotalTech_Kop_total; //Суммарное число операций общее

        //б) с учетом изготовления номенклатуры деталей в других подразделениях по кооперации
        private Stack<double> sumNumOtherFacil_K3;
        private Stack<double> sumNumTotalOtherFacil_Kop_total;


        //properties
        public float MFWT_Tmc
        {
            get
            {
                return machFreeWorkTime_Tmc;
            }
            set
            {
                machFreeWorkTime_Tmc = value;
            }
        }
        public float ET_Tz
        {
            get
            {
                return employmentTime_Tz;
            }
            set
            {
                if (value <= 0)
                {
                    employmentTime_Tz = 1;
                }
            }
        }
        public float EC_Kd
        {
            get
            {
                return employmentCoefficient_Kd;
            }
            set
            {
                if (value < 0.7f) employmentCoefficient_Kd = 0.7f;
                else if (value > 0.9f) employmentCoefficient_Kd = 0.9f;
                else employmentCoefficient_Kd = value;
            }
        }
        public float TL_Ti
        {
            get
            {
                return totalLabor_Ti;
            }
            set
            {
                totalLabor_Ti = value;
            }
        }
        public int AR_N
        {
            get
            {
                return annualRelease_N;
            }
            set
            {
                annualRelease_N = value;
            }
        }
        public int AWF_Feq
        {
            get
            {
                return annualWorkFund_Feq;
            }
            set
            {
                annualWorkFund_Feq = value;
            }
        }
        public float ELC_Kz
        {
            get
            {
                return eqLoadCoefficient_Kz;
            }
            set
            {
                eqLoadCoefficient_Kz = value;
            }
        }

        public Stack<double> TN_CNC_P
        {
            get
            {
                return totalNumCNCParts;
            }
        }

        public Stack<double> TNP
        {
            get
            {
                return totalNumParts;
            }
        }

        public Stack<double> SW_CNC_INTENCITY_Tcnc
        {
            get
            {
                return spWeightCNCIntencity_Tcnc;
            }
        }

        public Stack<double> SWT_INTENCITY_To
        {
            get
            {
                return spTotalIntencityWeight_To;
            }
        }

        public Stack<double> SW_CNC_DIF_Tcncj
        {
            get
            {
                return spWeightCNCDifIntence_Tcncj;
            }
        }

        public Stack<double> ST_DIF_Tj
        {
            get
            {
                return spTotalIntencityWeight_To;
            }
        }

        public Stack<double> SN_M_K2
        {
            get
            {
                return sumNumManualTech_K2;
            }
        }

        public Stack<double> SN_TOTAL_Kop_total
        {
            get
            {
                return sumNumTotalTech_Kop_total;
            }
        }

        public Stack<double> SNOF_K3
        {
            get
            {
                return sumNumOtherFacil_K3;
            }
        }

        public Stack<double> SNOF_Kop_total
        {
            get
            {
                return sumNumTotalOtherFacil_Kop_total;
            }
        }

        //constructor
        public CNC_BaseSites()
        {
            machFreeWorkTime_Tmc = 0f;
            employmentTime_Tz = 1f;
            employmentCoefficient_Kd = 0;
            totalLabor_Ti = 0;
            annualRelease_N = 0;
            annualWorkFund_Feq = 1;
            eqLoadCoefficient_Kz = 0.85f;
            totalNumCNCParts = new Stack<double>();
            totalNumParts = new Stack<double>();
            spWeightCNCIntencity_Tcnc = new Stack<double>();
            spTotalIntencityWeight_To = new Stack<double>();
            spWeightCNCDifIntence_Tcncj = new Stack<double>();
            spTotalDifIntence_Tj = new Stack<double>();
            sumNumManualTech_K2 = new Stack<double>();
            sumNumTotalTech_Kop_total = new Stack<double>();
            sumNumOtherFacil_K3 = new Stack<double>();
            sumNumTotalOtherFacil_Kop_total = new Stack<double>();
        }
        public CNC_BaseSites(float Tmc, float Tz, float Kd, float Ti, int N, int Feq, float Kz)
        {
            machFreeWorkTime_Tmc = Tmc;
            employmentTime_Tz = Tz;
            employmentCoefficient_Kd = Kd;
            totalLabor_Ti = Ti;
            annualWorkFund_Feq = Feq;
            annualRelease_N = N;
            eqLoadCoefficient_Kz = Kz;
            totalNumCNCParts = new Stack<double>();
            totalNumParts = new Stack<double>();
            spWeightCNCIntencity_Tcnc = new Stack<double>();
            spTotalIntencityWeight_To = new Stack<double>();
            spWeightCNCDifIntence_Tcncj = new Stack<double>();
            spTotalDifIntence_Tj = new Stack<double>();
            sumNumManualTech_K2 = new Stack<double>();
            sumNumTotalTech_Kop_total = new Stack<double>();
            sumNumOtherFacil_K3 = new Stack<double>();
            sumNumTotalOtherFacil_Kop_total = new Stack<double>();
        }

        //Methods
        public double MachMaintRateForOneOperator(float Tmc, float Tz, float Kd)
        {
            double n = ((Tmc * Kd) / Tz) + 1;
            return n;
        }
        public double requireCNCEquipment(float Ti, int N, int Feq, float Kz)
        {
            double ni = ((Ti * N) / (Feq * Kz));
            return ni;
        }

        public void addTN_CNC_P(double element)
        {
            totalNumCNCParts.Push(element);
        }
        public void delTN_CNC_P()
        {
            totalNumCNCParts.Pop();
        }

        public void addTNP(double element)
        {
            totalNumParts.Push(element);
        }
        public void delTNP()
        {
            totalNumParts.Pop();
        }

        public double spOperationWeight(Stack<double> CNC_Parts, Stack<double> TotalParts)
        {
            double Kop = 0;
            double sumCNC = 0;
            double sumTotal = 0;

            foreach(double CNC_el in CNC_Parts)
            {
                sumCNC += (double)CNC_Parts.Peek();
            }
            foreach(double Total_el in TotalParts)
            {
                sumTotal += (double)TotalParts.Peek();
            }
            Kop = sumCNC / sumTotal;
            return Kop;
        }

        public double spWeightOperationIntencity_KtpCNC(Stack<double> CNC_Intencity, Stack<double> Total_Intencity)
        {
            double KtpCNC = 0;
            double summ_Tcnc = 0;
            double summ_To = 0;

            foreach(double cnc_el in CNC_Intencity)
            {
                summ_Tcnc += CNC_Intencity.Peek();
            }
            foreach(double tot_el in Total_Intencity)
            {
                summ_To += Total_Intencity.Peek();
            }

            KtpCNC = summ_Tcnc / summ_To;

            return KtpCNC;
        }

        public double spWeight_CNC_DIF_KtpCNCj(Stack<double> CNC, Stack<double> Total)
        {
            double KtpCNCj = 0;
            double summ_Tcnc = 0;
            double summ_To = 0;

            foreach(double cnc_el in CNC)
            {
                summ_Tcnc += cnc_el;
            }
            foreach(double total_el in Total)
            {
                summ_To += total_el;
            }

            KtpCNCj = summ_Tcnc / summ_To;
            return KtpCNCj;
        }

        public double spWeight_Manual_Nomenclature_Kop_un(Stack<double> Manual, Stack<double> Total)
        {
            double Kop_un = 0;
            double summ_K2 = 0;
            double summ_Kop_tot = 0;

            foreach(double man_el in Manual)
            {
                summ_K2 += man_el;
            }
            foreach(double tot_el in Total)
            {
                summ_Kop_tot += tot_el;
            }

            Kop_un = summ_K2 / summ_Kop_tot;
            return Kop_un;
        }

        public double spWeight_Manual_OtherFacil_Kop_un(Stack<double> K3, Stack<double> Kop_tot)
        {
            double Kop_un = 0;
            double summ_K3 = 0;
            double summ_Kop_tot = 0;

            foreach (double man_el in K3)
            {
                summ_K3 += man_el;
            }
            foreach (double tot_el in Kop_tot)
            {
                summ_Kop_tot += tot_el;
            }

            Kop_un = summ_K3 / summ_Kop_tot;
            return Kop_un;
        }
    }

    //Наверное разобью 3.4 раздел на подклассы а то со стэками вообще...
    //3.4 Определение состава и количества станков на ГПС - гибкую произваодственную линию
    public class NumOfMachines_AF_Line
    {
        //Private fields
        private Stack<double> pieceProcessTime_Tp; //Штучное время обработки i-ой детали на данной операции
        private int annualPartRelease_Di; //Годовой выпуск i-ой детали
        private double Fc; //Пока не знаю :-)
        private double adjustmentTimeFactor_Kn; //Коэффициент, учитывающий время на переналадку линии с одного наименования на другое

        //Properties
        public Stack<double> PPT_Tp
        {
            get
            {
                return pieceProcessTime_Tp;
            }
        }
        public int APR_Di
        {
            get
            {
                return annualPartRelease_Di;
            }
            set
            {
                if(value < 0)
                {
                    annualPartRelease_Di = -value;
                }
                else
                {
                    annualPartRelease_Di = value;
                }
            }
        }
        public double FC
        {
            get
            {
                return Fc;
            }
            set
            {
                if (value == 0) Fc = 1;
                else Fc = value;
            }
        }
        public double ATF_Kn
        {
            get
            {
                return adjustmentTimeFactor_Kn;
            }
            set
            {
                if (value == 0) adjustmentTimeFactor_Kn = 0.95;
                else adjustmentTimeFactor_Kn = value;
            }
        }
        //Constructor
        public NumOfMachines_AF_Line()
        {
            pieceProcessTime_Tp = new Stack<double>();
            annualPartRelease_Di = 1;
            Fc = 1;
            adjustmentTimeFactor_Kn = 0.95;
        }
        public NumOfMachines_AF_Line(int Di, double fc, double Kn)
        {
            pieceProcessTime_Tp = new Stack<double>();
            annualPartRelease_Di = Di;
            Fc = fc;
            adjustmentTimeFactor_Kn = Kn;
        }

        //Methods
        //Задать стэк
        public void add_Tp(double el)
        {
            pieceProcessTime_Tp.Push(el);
        }
        //Очистить стэк
        public void delete_Tp()
        {
            pieceProcessTime_Tp.Clear();
        }

        //Вычислить количество станков на ППЛ
        public double NOM_AF_Line_Cp(Stack<double> Tp, int Di, float fc, float Kn)
        {
            double Cp = 0;
            double summTp = 0;
            foreach(double el in Tp)
            {
                summTp += el;
            }

            Cp = (summTp * Di) / (60 * fc * Kn);
            return Cp;
        }

    }
    //Единый такт обработки деталей
    public class singleStepMacineTime
    {
        //Private fields
        private double fc; //См выше
        private double adjustmentTimeFactor_Kn;
        private Stack<int> annualPartRelease_Di;

        //Properties
        public double Fc
        {
            get
            {
                return fc;
            }
            set
            {
                fc = value;
            }
        }

        public double ATF_Kn
        {
            get
            {
                return adjustmentTimeFactor_Kn;
            }
            set
            {
                adjustmentTimeFactor_Kn = value;
            }
        }

        public Stack<int> APR_Di
        {
            get
            {
                return annualPartRelease_Di;
            }
        }
        //Constructor
        public singleStepMacineTime()
        {
            fc = 1;
            adjustmentTimeFactor_Kn = 0.95;
            annualPartRelease_Di = new Stack<int>();
        }
        public singleStepMacineTime(double f_c, double kn)
        {
            fc = f_c;
            adjustmentTimeFactor_Kn = kn;
            annualPartRelease_Di = new Stack<int>();
        }

        //Methods
        //Добавить в стэк
        public void addTo_Di(int el)
        {
            annualPartRelease_Di.Push(el);
        }
        //Очистить стэк
        public void delet_Di()
        {
            annualPartRelease_Di.Clear();
        }
        //Рассчитать единый такт производства
        public double calculateStepTime_t(double f_c, double kn, Stack<int> Di)
        {
            double tau = 0;
            int summDi = 0;

            foreach(int el in Di)
            {
                summDi += el;
            }

            tau = (double)((60 * f_c * kn) / summDi);
            return tau;
        }
    }
    //Число станков для каждой операции в линии
    public class numOfMachines
    {
        //Private fields
        private double processTime_Tp; //Штучное время обработки
        private double stepTime_tau; //Единый так времени производства
        private Stack<int> annualRelease_Di; //Годовой выпуск деталей (стэк-структура)
        private double fc_value;
        private double adjTimeFactor_Kn; //Коэффициент времени переналадки линии

        //Properties
        public double PT_Tp
        {
            get
            {
                return processTime_Tp;
            }
            set
            {
                processTime_Tp = value;
            }
        }
        //Лучше вычислять по методу описанному выше классом
        public double ST_Tau
        {
            get
            {
                return stepTime_tau;
            }
            set
            {
                if (value == 0) stepTime_tau = 1;
                else stepTime_tau = value;
            }
        }
        public Stack<int> AR_Di
        {
            get
            {
                return annualRelease_Di;
            }
        }
        public double FC
        {
            get
            {
                return fc_value;
            }
            set
            {
                if (value == 0) fc_value = 1;
                else fc_value = value;
            }
        }
        public double ATF_Kn
        {
            get
            {
                return adjTimeFactor_Kn;
            }
            set
            {
                if (value == 0) adjTimeFactor_Kn = 0.95;
                else adjTimeFactor_Kn = value;
            }
        }  
        //Constructor
        public numOfMachines()
        {
            fc_value = 1;
            processTime_Tp = 1;
            stepTime_tau = 1;
            annualRelease_Di = new Stack<int>();
            adjTimeFactor_Kn = 0.95;
        }
        public numOfMachines(double fc, double Tp, double Tau, double Kn)
        {
            fc_value = fc;
            processTime_Tp = Tp;
            stepTime_tau = Tau;
            adjTimeFactor_Kn = Kn;
            annualRelease_Di = new Stack<int>();
        }
        //Methods
        public void addTo_Di(int el)
        {
            annualRelease_Di.Push(el);
        }
        public void delete_Tp()
        {
            annualRelease_Di.Clear();
        }
        //Число станков для каждой операции в линии так:
        public double numOfMachinesWith_Tau_Cp(double Tp, double Tau)
        {
            double Cp = 0;
            Cp = Tp / Tau;
            return Cp;
        }
        // ... или так:
        public double numOfMachinesWith_Di_Cp(double Tp, Stack<int> Di, double fc, double kn)
        {
            double Cp = 0;
            double summDi = 0;

            foreach(int el in Di)
            {
                summDi += el;
            }
            Cp = (double)((Tp*summDi)/(60*fc*kn));
            return Cp;
        }
    }
    //Число станков на автоматической линии
    public class numMachines_auto
    {
        //Private fields
        private double operativeTime_Top; //Оперативное время для данной операции
        private double timeStep_Tau; //Такт работы линии

        //Properties
        public double OT_Top
        {
            get
            {
                return operativeTime_Top;
            }
            set
            {
                operativeTime_Top = value;
            }
        }

        public double TS_Tau
        {
            get
            {
                return timeStep_Tau;
            }
            set
            {
                if (value == 0) timeStep_Tau = 1;
                else timeStep_Tau = value;
            }
        }
        public double TM { get; set; }
        public double TS { get; set; }
        public double TR { get; set; }
        //Constructor
        public numMachines_auto()
        {
            operativeTime_Top = 1;
            timeStep_Tau = 1;
        }
        public numMachines_auto(double Top, double Tau)
        {
            operativeTime_Top = Top;
            timeStep_Tau = Tau;
        }
        //Methods
        //Число станков для каждой операции, выполняемой на автоматической линии
        public double NM_Auto_Cp(double Top, double Tau)
        {
            return Top / Tau;
        }
        //Расчет оперативного времени
        public double Top(double Tmach, double Tsub, double Ttr)
        {
            return Tmach + Tsub + Ttr;
        }
    }
    //Число станков для лимитирующей операции
    public class numLimitOPMachines
    {
        //Private fields
        private double autoStep_top; //Такт автоматической линии
        private int annualRelease_D; //Ежегодный выпуск
        private double fc_value; //
        private double effectiveFactor_nu; //КПД такта работы линии (0.85 - 0.95)

        //Properties
        public double AS_Top
        {
            get
            {
                return autoStep_top;
            }
            set
            {
                autoStep_top = value;
            }
        }
        public int AR_D
        {
            get
            {
                return annualRelease_D;
            }
            set
            {
                annualRelease_D = value;
            }
        }
        public double FC
        {
            get
            {
                return fc_value;
            }
            set
            {
                fc_value = value;
            }
        }
        public double EFFECTIVE_NU
        {
            get
            {
                return effectiveFactor_nu;
            }
            set
            {
                if (value < 0.85) effectiveFactor_nu = 0.85;
                else if (value > 0.95) effectiveFactor_nu = 0.95;
                else effectiveFactor_nu = value;
            }
        }

        //Constructor
        public numLimitOPMachines()
        {
            autoStep_top = 1;
            annualRelease_D = 1;
            fc_value = 1;
            effectiveFactor_nu = 0.85;
        }
        //Methods
        //Расчет числа станков для лимитирующей операции
        public double numLimit_Cp_lim(double top, int D, double Fc, double nu)
        {
            double Cp_lim = 0;
            Cp_lim = (top * D) / (60 * Fc * nu);
            return Cp_lim;
        }
    }
    //Определение коэффициента загрузки линии
    public class busLineFactor
    {
        //Private fields
        private double Cp_auto; //На каждую операцию 
        private double Cn_auto; //На все операции

        //Properties
        public double CP_AUTO
        {
            get
            {
                return Cp_auto;
            }
            set
            {
                Cp_auto = value;
            }
        }
        public double CN_AUTO
        {
            get
            {
                return Cn_auto;
            }
            set
            {
                if (value <= 0) Cn_auto = 1;
                else Cn_auto = value;
            }
        }

        //Constructor
        public busLineFactor()
        {
            Cp_auto = 1;
            Cn_auto = 1;
        }
        //Methods
        //Коэффициент загрузки линии
        public double blFactor_Kbl(double Cp, double Cn)
        {
            return Cp / Cn;
        }
    }
    // Основное машинное время на операцию
    public class mainOperationTime
    {
        //Private fields
        private Stack<double> t_oj;// Основное время j-Го перехода обработки

        //Properties
        public Stack<double> T_OJ
        {
            get
            {
                return t_oj;
            }
        }
        //Constructor
        public mainOperationTime()
        {
            t_oj = new Stack<double>();
        }
        //Methods
        public double mainTime_To(Stack<double> Toj)
        {
            double To = 0;

            foreach(double el in Toj)
            {
                To += el;
            }

            return To;
        }
    }
    // Штучно-калькуляционное время
    public class pieceCostingTime
    {
        //Private fields
        private double pieceTime_Tpi; //Штучное время
        private double preFinTime_Tpf; //Подготовительно-заключительное время на партию деталей. Обычно = 12 мин
        private int batchSize_Nn; //Размер партии деталей

        //Properties
        public double PT_Tpi
        {
            get
            {
                return pieceTime_Tpi;
            }
            set
            {
                pieceTime_Tpi = value;
            }
        }
        public double PFT_Tpf
        {
            get
            {
                return preFinTime_Tpf;
            }
            set
            {
                if (value < 0) preFinTime_Tpf = -value;
                else preFinTime_Tpf = value;
            }
        }
        public int BS_Nn
        {
            get
            {
                return batchSize_Nn;
            }
            set
            {
                if (value == 0) batchSize_Nn = 1;
                else if (value < 0) batchSize_Nn = -value;
                else batchSize_Nn = value;
            }
        }
        //Constructor
        public pieceCostingTime()
        {
            pieceTime_Tpi = 1;
            preFinTime_Tpf = 1;
            batchSize_Nn = 1;
        }
        //Methods
        //Расчет штучно-калькуляционного времени
        public double pc_Time_Tpct(double Tpc, double Tpf, int Nn)
        {
            double Tpct = 0;
            Tpc = (double)(Tpc + (Tpf / Nn));
            return Tpc;
        }
    }

    //Коэффициент загрузки станков с ЧПУ
    public class CNCLoadFactor
    {
        //Private fields
        private double spRecoveryTime_Bn; //Удельная длительность восстановления работоспособности станка
        private double spWorkWeightFactor_Ksp; //Коэффициент удельного веса работы станка по ПУ в Фс. Используют Куд = 0.4 - 0.5

        //Properties
        public double SRT_Bn
        {
            get
            {
                return spRecoveryTime_Bn;
            }
            set
            {
                spRecoveryTime_Bn = value;
            }
        }
        public double SWWF_Ksp
        {
            get
            {
                return spWorkWeightFactor_Ksp;
            }
            set
            {
                if (value < 0.4) spWorkWeightFactor_Ksp = 0.4;
                else if (value > 0.5) spWorkWeightFactor_Ksp = 0.5;
                else spWorkWeightFactor_Ksp = value;
            }
        }

        //Constructor
        public CNCLoadFactor()
        {
            spRecoveryTime_Bn = 0.5;
            spWorkWeightFactor_Ksp = 0.4;
        }
        //Methods
        //Расчет коэффициента загрузки
        public double loadFactor_SIGMA(double Bn, double Ksp)
        {
            double SIGMA = 0;
            SIGMA = 0.85 * (1 - Bn * Ksp);
            return SIGMA;
        }
    }

    //Колличество оборудования укрупненным способом
    public class amountEq_by_EM
    {
        //Private fields
        private int annualLabIntenc_T; //Трудоемкость годового выпуска всех изделий в цехе, отделении (станков/час)
        private int fc_value; //
        private double midFactor_Kfo; //Средний коэффициент Кз.о.

        //Properties
        public int ALI_T
        {
            get
            {
                return annualLabIntenc_T;
            }
            set
            {
                if (value < 0) annualLabIntenc_T = -value;
                else annualLabIntenc_T = value;
            }
        }
        public int FC
        {
            get
            {
                return fc_value;
            }
            set
            {
                if (value == 0) fc_value = 1;
                else fc_value = value;
            }
        }
        public double MF_Kfo
        {
            get
            {
                return midFactor_Kfo;
            }
            set
            {
                if (value == 0) midFactor_Kfo = 0.7;
                else midFactor_Kfo = value;
            }
        }
        //Constructor
        public amountEq_by_EM()
        {
            annualLabIntenc_T = 1;
            fc_value = 1;
            midFactor_Kfo = 0.7; //По уиолчанию сделано для массового производства
        }
        //Methods
        public double amEQ_Cn_mid(int T, double Fc, double K)
        {
            double Cn_mid = 0;
            Cn_mid = (double)(T / (Fc * K));
            return Cn_mid;
        }
    }
    //Количество мест для сборки изделия в условии непоточного производства
    public class nonExact_PC_numWorkPlaces
    {
        //Private fields
        private int prodLabAssembly_Tla; //Трудоемкость сборки одного изделия (чел/час)
        private double fc_value; //Годовой фонд
        private double avWorkDensity_Pav; //Средняя плотность работы
        private int annualRelease_D;

        //Propertise
        public int PLA_Tla
        {
            get
            {
                return prodLabAssembly_Tla;
            }
            set
            {
                prodLabAssembly_Tla = value;
            }
        }
        public double FC
        {
            get
            {
                return fc_value;
            }
            set
            {
                if (value == 0) fc_value = 1;
                else fc_value = value;
            }
        }
        public double AWD_Pav
        {
            get
            {
                return avWorkDensity_Pav;
            }
            set
            {
                if (value == 0) avWorkDensity_Pav = 1;
                else avWorkDensity_Pav = value;
            }
        }
        public int AR_D
        {
            get
            {
                return annualRelease_D;
            }
            set
            {
                annualRelease_D = value;
            }
        }
        //Constructor
        public nonExact_PC_numWorkPlaces()
        {
            prodLabAssembly_Tla = 1;
            fc_value = 1;
            avWorkDensity_Pav = 1;
            annualRelease_D = 1;
        }
        //Methods
        //Расчет кол-ва раб. мест для сборки изделия 
        public double nwp_pc_nonexact_Mw(int Tsb, int Fc, double Pav, int D)
        {
            double Mw = 0;
            Mw = (double)((Tsb * D) / (Fc * Pav));
            return Mw;
        }
    }
    //Коэффициент загрузки рабочих мест
    public class workBusyFactor
    {
        //Private fields
        private double nwp_pc_Mw; //Число расчета рабочих мест

        //Preperties
        public double MW
        {
            get
            {
                return nwp_pc_Mw;
            }
            set
            {
                nwp_pc_Mw = value;
            }
        }

        //Constructor
        public workBusyFactor()
        {
            nwp_pc_Mw = 1;
        }
        //Methods
        //Коэффициент загрузки рабочих мест
        public double wbFactor_Kb(double Mw)
        {
            int app_Ma = (int)(Math.Floor(Mw));
            double Kb = 0;
            Kb = (double)(Mw / app_Ma);
            return Kb;
        }
    }
    //Число единиц оборудования
    public class numOfEquipment
    {
        //Private fields
        private double opLabIntencity_T; //Трудоемкость данной операции
        private int annualRelease_D; //Годовой выпуск
        private int fc_value; //Годовой фонд

        //Properties
        public double OLI_T
        {
            get
            {
                return opLabIntencity_T;
            }
            set
            {
                if (value < 0) opLabIntencity_T = -value;
                else opLabIntencity_T = value;
            }
        }
        public int AR_D
        {
            get
            {
                return annualRelease_D;
            }
            set
            {
                if (value < 0) annualRelease_D = -value;
                else annualRelease_D = value;
            }
        }
        public int FC
        {
            get
            {
                return fc_value;
            }
            set
            {
                if (value == 0) fc_value = 1;
                else if (value < 0) fc_value = -value;
                else fc_value = value;
            }
        }
        //Constructor
        public numOfEquipment()
        {
            opLabIntencity_T = 1;
            annualRelease_D = 1;
            fc_value = 1;
        }
        //Methods
        //Рассчет числа единиц оборудования
        public double NOEq_Cp(double T, int D, int Fc)
        {
            double Cp = 0;
            Cp = (double)((T * D) / Fc);
            return Cp;
        }
    }
    //Число рабочих мест поточной обработки
    public class WP_FlowAssembly
    {
        //Private fields
        private int prLabIntencity_Ta; //Трудоемкость сборки изделия (чел/час)
        private double stepTime_Tau; //Единый такт времени
        private double avWorkIntencity_Pav; //Средняя трудоемкость работы

        //Proprties
        public int PLI_Ta
        {
            get
            {
                return prLabIntencity_Ta;
            }
            set
            {
                if (value < 0) prLabIntencity_Ta = -value;
                else prLabIntencity_Ta = value;
            }
        }
        public double ST_Tau
        {
            get
            {
                return stepTime_Tau;
            }
            set
            {
                if (value == 0) stepTime_Tau = 1;
                else if (value < 0) stepTime_Tau = -value;
                else stepTime_Tau = value;
            }
        }
        public double AWI_Pav
        {
            get
            {
                return avWorkIntencity_Pav;
            }
            set
            {
                if (value == 0) avWorkIntencity_Pav = 1;
                else if (value < 0) avWorkIntencity_Pav = -value;
                else avWorkIntencity_Pav = value;
            }
        }
        //Constructor
        public WP_FlowAssembly()
        {
            prLabIntencity_Ta = 1;
            stepTime_Tau = 1;
            avWorkIntencity_Pav = 1;
        }

        //Methods
        //Определение числа рабочих мест поточной сборки
        public double num_WP_FA_Mw(int Ta, double Tau, double Pav)
        {
            double Mw = 0;
            Mw = (double)((Ta * 60) / (Tau * Pav));
            return Mw;
        }
    }
    //Число раб. мест для поточной сборки при укрупненном проектировании
    public class WP_FlowAssembly_Extended
    {
        //Private fields
        private Stack<int> prLabIntencity_Ta; //Суммарная трудоемкость сборки годового выпуска
        private int fc_value; //Годовой фонд
        private double avWorkIntencity_Pav; //См выше
        private double avBusyFactor_Kb_av; //Средний коэффициент загрузки

        //Properties
        public Stack<int> PLI_Ta
        {
            get
            {
                return prLabIntencity_Ta;
            }
        }
        public int FC
        {
            get
            {
                return fc_value;
            }
            set
            {
                if (value == 0) fc_value = 1;
                else if (value < 0) fc_value = -value;
                else fc_value = value;
            }
        }
        public double AWI_Pav
        {
            get
            {
                return avWorkIntencity_Pav;
            }
            set
            {
                if (value == 0) avWorkIntencity_Pav = 1;
                else if (value < 0) avWorkIntencity_Pav = -value;
                else avWorkIntencity_Pav = value;
            }
        }
        public double ABF_Kb_av
        {
            get
            {
                return avBusyFactor_Kb_av;
            }
            set
            {
                if (value == 0) avBusyFactor_Kb_av = 1;
                else if (value < 0.75) avBusyFactor_Kb_av = 0.75;
                else avBusyFactor_Kb_av = value;
            }
        }
        //Constructor
        public WP_FlowAssembly_Extended()
        {
            prLabIntencity_Ta = new Stack<int>();
            fc_value = 1;
            avWorkIntencity_Pav = 1;
            avBusyFactor_Kb_av = 1;
        }
        //Methods
        //Число раб. мест сборочного отделения при укрупненном проектировании
        public double num_WP_FA_Ex_Mas(Stack<int> Tsb, int Fc, double Pav, double K)
        {
            double summTsb = 0;
            double Mas = 0;

            foreach(double el in Tsb)
            {
                summTsb += el;
            }
            Mas = (double)(summTsb / (Fc * Pav * K));
            return Mas;
        }
    }
    //Число раб. мест на пульсирующих конвейерах
    public class WP_Pulsating_Conveyrous
    {
        //Private fields
        private int nodLabIntencity_Tp; //Трудоемкость сборки изделия (узла) (чел/час)
        private double stepTime_Tau; //Единый такт времени сборки
        private double posTpos_conv_time_t; //Время перемещения конвейера с позиции на позицию
        private double avWorkIntencity_Pav; //См выше

        //Properties
        public int NLI_Tp
        {
            get
            {
                return nodLabIntencity_Tp;
            }
            set
            {
                if (value < 0) nodLabIntencity_Tp = -value;
                else nodLabIntencity_Tp = value;
            }
        }
        public double ST_Tau
        {
            get
            {
                return stepTime_Tau;
            }
            set
            {
                if (value == 0) stepTime_Tau = 1;
                else if (value < 0) stepTime_Tau = -value;
                else stepTime_Tau = value;
            } 
        }
        public double PPCT_t
        {
            get
            {
                return posTpos_conv_time_t;
            }
            set
            {
                if (value == 0) posTpos_conv_time_t = 1;
                else if (value < 0) posTpos_conv_time_t = -value;
                else posTpos_conv_time_t = value;
            }
        }
        public double AWI_Pav
        {
            get
            {
                return avWorkIntencity_Pav;
            }
            set
            {
                if (value == 0) avWorkIntencity_Pav = 1;
                else if (value < 0) avWorkIntencity_Pav = -value;
                else avWorkIntencity_Pav = value;
            }
        }
        //Constructor
        public WP_Pulsating_Conveyrous()
        {
            nodLabIntencity_Tp = 1;
            stepTime_Tau = 1;
            posTpos_conv_time_t = 1;
            avWorkIntencity_Pav = 1;
        }
        //Methods
        //Расчет числа раб. мест на пульсир. конвейерах
        public double num_WP_PC_Mas(int T, double Tau, double t, double Pav)
        {
            double Mas = 0;
            Mas = (double)(T / (Pav * (Tau * t)));
            return Mas;
        }
    }
    //Параметры конвейера
    public class conveyerProperties
    {
        //Private fields
        private double fullLength_L; //Общая длина конвейера
        private double workPart_l1; // Длина рабочей части конвейера
        private double drivePart_l2; // Длина приводной станции конвейера
        private double conveyerStep_l; //Шаг конвейера
        private double wp_assembly_Mas; //Число рабочих мест для сборки
        private double asemblPart_l1_; //Длина собираемого изделия
        private double clearensPart_l2_; //Расстояние между сборочными позициями
        private double stepTime_Tau; //Единый такт времени
        private double conveyerSpeed_V; //Скорость конвейера

        //Properties
        public double FL_L
        {
            get { return fullLength_L; }
            set { fullLength_L = value; }
        }
        public double WP_L1
        {
            get { return workPart_l1; }
            set { workPart_l1 = value; }
        }
        public double DP_L2
        {
            get
            {
                return drivePart_l2;
            }
            set
            {
                drivePart_l2 = value;
            }
        }
        public double CS_l
        {
            get { return conveyerStep_l; }
            set { conveyerStep_l = value; }
        }
        public double WP_A_Mas
        {
            get { return wp_assembly_Mas; }
            set
            {
                if (value == 0) wp_assembly_Mas = 1;
                else if (value < 0) wp_assembly_Mas = -value;
                else wp_assembly_Mas = value;
            }
        }
        public double AP_L1_
        {
            get { return asemblPart_l1_; }
            set
            {
                if (value == 0) asemblPart_l1_ = 1;
                else if (value < 0) asemblPart_l1_ = -value;
                else asemblPart_l1_ = value;
            }


        }
        public double CP_L2_
        {
            get { return clearensPart_l2_; }
            set
            {
                if (value == 0) clearensPart_l2_ = 1;
                else if (value < 0) clearensPart_l2_ = -value;
                else clearensPart_l2_ = value;
            }
        }
        public double ST_Tau
        {
            get { return stepTime_Tau; }
            set
            {
                if (value == 0) stepTime_Tau = 1;
                else if (value < 0) stepTime_Tau = -value;
                else stepTime_Tau = value;
            }
        }
        public double CS_V
        {
            get { return conveyerSpeed_V; }
            set { conveyerSpeed_V = value; }
        }
        //Constructor
        public conveyerProperties()
        {
            wp_assembly_Mas = 1;
            asemblPart_l1_ = 1;
            clearensPart_l2_ = 1;
            stepTime_Tau = 1;
        }
        //Methods
        //Полная длина конвейера L
        public void FL_Conveyer_L()
        {
            fullLength_L = workPart_l1 + drivePart_l2;
        }
        //Длина рабочей части
        public void WP_Conveyer_L1()
        {
            workPart_l1 = conveyerStep_l + wp_assembly_Mas;
        }
        //Длина шага конвейера
        public void SC_Conveyer_l()
        {
            conveyerStep_l = asemblPart_l1_ + clearensPart_l2_;
        }
        //Скорость конвейера
        public void Con_Speed_V()
        {
            if (stepTime_Tau == 0) throw new System.DivideByZeroException();
            conveyerSpeed_V = conveyerStep_l / stepTime_Tau;
        }
    }
    //3.5 Транспонно-накопительные системы ТНС гибких автоматизированных систем
    //Число кранов-штабелеров для механических участков
    public class numOfStackerCranes
    {
        //Private fields
        private int numTransporingParts_n; //Число деталей, подлежащих транспортированию за смену
        private int avNumTransOperation_i; //Среднее число операций на одну деталь
        private double craneRunTime_Tcr; //Время пробега крана в мин.
        private int numSyncTransParts_m; //Число одновременно транспортируемых деталей
        private double shiftWorktime_Tsh; //Время работы в смену в мин.
        private double numStackerCranes_k; //Число кранов-штабелеров

        //Properties
        public int NTP_n
        {
            get { return numTransporingParts_n; }
            set
            {
                if (value < 0) numTransporingParts_n = -value;
                else numTransporingParts_n = value;
            }
        }
        public int ANTO_i
        {
            get { return avNumTransOperation_i; }
            set
            {
                if (value < 0) avNumTransOperation_i = -value;
                else avNumTransOperation_i = value;
            }
        }
        public double CRT_Tcr
        {
            get { return craneRunTime_Tcr; }
            set
            {
                if (value < 0) craneRunTime_Tcr = -value;
                else craneRunTime_Tcr = value;
            }
        }
        public int NSTP_m
        {
            get { return numSyncTransParts_m; }
            set
            {
                if (value == 0) numSyncTransParts_m = 1;
                else if (value < 0) numSyncTransParts_m = -value;
                else numSyncTransParts_m = value;
            }
        }
        public double SW_Tsh
        {
            get { return shiftWorktime_Tsh; }
            set
            {
                if (value == 0) shiftWorktime_Tsh = 1;
                else if (value < 0) shiftWorktime_Tsh = -value;
                else shiftWorktime_Tsh = value;
            }
        }
        public double NSC_k
        {
            get { return numStackerCranes_k; }
            set { numStackerCranes_k = value; }
        }
        //Constructor
        public numOfStackerCranes()
        {
            numTransporingParts_n = 1;
            avNumTransOperation_i = 1;
            craneRunTime_Tcr = 1;
            numSyncTransParts_m = 1;
            shiftWorktime_Tsh = 1;
        }
        //Methods
        //Расчет числа кранов-штабелеров
        public void nsc_k()
        {
            numStackerCranes_k = (double)((numTransporingParts_n * avNumTransOperation_i * craneRunTime_Tcr) / (numSyncTransParts_m * shiftWorktime_Tsh));
        }
    }
    //Кол-во единиц транспорта колесно-тележечного транспорта для...
    public class NumOf_TF_units
    {
        //Private fields
        private double annualProductTurnover_Q; //Годовой грузооборот, тонн
        private double unevenessFactor_K1; //Коэффициент неравномерности
        private double unevCargoUseFactor_K2; //Коэффициент неравномерности использования грузоподъемности
        private double totalMachineRunTime_Te; //Общее время пробега электрокара
        private double carCarryng_Qe; //Грузоподъемность электрокара
        private double annualTimeFund_Fo; //Действительный годовой фонд времени работы оборудования
        private double DSPS_Carryng_E1; //...двустрононней маятноковой системы
        private double SSPS_Carryng_E2; //...односторонней маятноковой системы

        //Properties
        public double APT_Q
        {
            get { return annualProductTurnover_Q; }
            set
            {
                if (value < 0) annualProductTurnover_Q = -value;
                else annualProductTurnover_Q = value;
            }
        }
        public double UF_K1
        {
            get { return unevenessFactor_K1; }
            set
            {
                if (value < 0) unevenessFactor_K1 = -value;
                else unevenessFactor_K1 = value;
            }
        }
        public double UCUF_K2
        {
            get { return unevCargoUseFactor_K2; }
            set
            {
                if (value < 0) unevCargoUseFactor_K2 = -value;
                else unevCargoUseFactor_K2 = value;
            }
        }
        public double TMRT_Te
        {
            get { return totalMachineRunTime_Te; }
            set
            {
                if (value < 0) totalMachineRunTime_Te = -value;
                else totalMachineRunTime_Te = value;
            }
        }
        public double CC_Qe
        {
            get { return carCarryng_Qe; }
            set
            {
                if (value == 0) carCarryng_Qe = 1;
                else if (value < 0) carCarryng_Qe = -value;
                else carCarryng_Qe = value;
            }
        }
        public double ATF_Fo
        {
            get { return annualTimeFund_Fo; }
            set
            {
                if (value == 0) annualTimeFund_Fo = 1;
                else if (value < 0) annualTimeFund_Fo = -value;
                else annualTimeFund_Fo = value;
            }
        }
        public double E1
        {
            get { return DSPS_Carryng_E1; }
            set { DSPS_Carryng_E1 = value; }
        }
        public double E2
        {
            get { return SSPS_Carryng_E2; }
            set { SSPS_Carryng_E2 = value; }
        }
        //Constructor
        public NumOf_TF_units()
        {
            annualProductTurnover_Q = 1;
            annualTimeFund_Fo = 1;
            unevenessFactor_K1 = 1;
            unevCargoUseFactor_K2 = 1;
            totalMachineRunTime_Te = 1;
            carCarryng_Qe = 1;
        }
        //Methods
        //...для двусторонней маятн. сис.
        public void ds_carryndMethod_E1()
        {
            DSPS_Carryng_E1 = (annualProductTurnover_Q*unevenessFactor_K1*totalMachineRunTime_Te) / (2*carCarryng_Qe*unevCargoUseFactor_K2*annualTimeFund_Fo*60);
        }
        //...для односторонней маятн. сис.
        public void ss_carryngMethod_E2()
        {
            SSPS_Carryng_E2 = (annualProductTurnover_Q * unevenessFactor_K1 * totalMachineRunTime_Te) / (carCarryng_Qe * unevCargoUseFactor_K2 * annualTimeFund_Fo * 60);
        }
    }
    //Скорость и производительность конвейера
    public class SpeedAndPerfomance
    {
        //Private fields
        //Задаваемые данные
        private double partStepDistance_l; //Шаг перемещаемых изделий, м
        private double stepTime_Tau; //Такт работы, мин
        private int numSyncParts_n; //Кол-во. синхронно перемещенных изделий
        //Расчитываемые данные
        private double convSpeed_V; //Скорость конвейера
        private int convPerfomance_Q; //Производительность конвейера
            
        //Properties
        public double PSD_l
        {
            get { return partStepDistance_l; }
            set
            {
                if (value < 0) partStepDistance_l = -value;
                else partStepDistance_l = value;
            }
        }
        public double ST_Tau
        {
            get { return stepTime_Tau; }
            set
            {
                if (value == 0) stepTime_Tau = 1;
                else if (value < 0) stepTime_Tau = -value;
                else stepTime_Tau = value;
            }
        }
        public int NSP_n
        {
            get { return NSP_n; }
            set
            {
                if (value == 0) numSyncParts_n = 1;
                else if (value < 0) numSyncParts_n = -value;
                else numSyncParts_n = value;
            }
        }
        //Constructor
        public SpeedAndPerfomance()
        {
            partStepDistance_l = 1;
            stepTime_Tau = 1;
            numSyncParts_n = 1;
        }
        //Methods
        //Расчет скорости через производительность
        public void SpeedViaPerf_V()
        {
            convSpeed_V = (double)(convPerfomance_Q * partStepDistance_l) / (60 *numSyncParts_n);
        }
        //Расчет скорости через введенные параметры n tau l
        public void SpeedViaKnown_V()
        {
            convSpeed_V = partStepDistance_l / (stepTime_Tau * numSyncParts_n);
        }
        //Расчет производительности
        public void Perfomance_Q()
        {
            convPerfomance_Q = (int)(Math.Floor((convSpeed_V * numSyncParts_n * 60) / (partStepDistance_l)));
        }
    }

    //3.6 Компановка автоматизированных складов
    //Продолжительность цикла кранов-штабелеров
    public class StacerCrane_CycleTime
    {
        //Private fields
        //Задаваемые параметры
        private double avCargoCraneDistance_Lk; //Средний путь перемещения крана с грузом, м
        private double avTrolleyCraneDistance_Lt; //Средний путь перемещения тележки-крана, м
        private double avCargoLiftingHeight_D; //Средняя высота подъема груза, м
        private double craneMovLift_Factor_fi; //Коэффициент, учитывающий совмещение движения штабелера с подъемом грууза от 0 до 0.3
        private double cargoWidth_b; //Ширина груза, м
        private double craneSpeed_Vc; //Скорость крана
        private double carriageLiftSpeed_Vcl; //Скорость подъема каретки
        private double extTelescCargoLoaderSpeed_Vet; //Скорость выдвижения телескопического грузозахвата
        private double overtime_to; //Дополнительное время - от 0.1 - 0.4 мин
        private double colRotatAngle_alpha; //Угол поворота колонны за время перемещения груза, град
        private double colRotatFriquency_omega; //Частота вращения колонны об/мин
        //Вычисляемые параметры
        private double RST_crane_cycleTime_Trst; //Продолжительность цикла стелажного крана-штабелера
        private double PST_crane_cycleTime_Tpst; //Продолжительность цикла мостового крана-штабелера

        //Properties
        public double ACCD_Lk
        {
            get { return avCargoCraneDistance_Lk; }
            set
            {
                if (value < 0) avCargoCraneDistance_Lk = -value;
                else avCargoCraneDistance_Lk = value;
            }
        }
        public double ATCD_Lt
        {
            get { return avTrolleyCraneDistance_Lt; }
            set
            {
                if (value < 0) avTrolleyCraneDistance_Lt = -value;
                else avTrolleyCraneDistance_Lt = value;
            }
        }
        public double ACLH_D
        {
            get { return avCargoLiftingHeight_D; }
            set
            {
                if (value < 0) avCargoLiftingHeight_D = -value;
                else avCargoLiftingHeight_D = value;
            }
        }
        public double CMLF_fi
        {
            get { return craneMovLift_Factor_fi; }
            set
            {
                if (value < 0) craneMovLift_Factor_fi = -value;
                else craneMovLift_Factor_fi = value;
            }
        }
        public double CW_b
        {
            get { return cargoWidth_b; }
            set
            {
                if (value < 0) cargoWidth_b = -value;
                else cargoWidth_b = value;
            }
        }
        public double CS_Vc
        {
            get { return craneSpeed_Vc; }
            set
            {
                if (value < 0) craneSpeed_Vc = -value;
                else craneSpeed_Vc = value;
            }
        }
        public double CLS_Vcl
        {
            get { return carriageLiftSpeed_Vcl; }
            set
            {
                if (value < 0) carriageLiftSpeed_Vcl = -value;
                else carriageLiftSpeed_Vcl = value;
            }
        }
        public double ETCLS_Vet
        {
            get { return extTelescCargoLoaderSpeed_Vet; }
            set
            {
                if (value < 0) extTelescCargoLoaderSpeed_Vet = -value;
                else extTelescCargoLoaderSpeed_Vet = value;
            }
        }
        public double OVERTIME_to
        {
            get { return overtime_to; }
            set
            {
                if (value < 0) overtime_to = -value;
                else overtime_to = value;
            }
        }
        public double CRA_ALPHA
        {
            get { return colRotatAngle_alpha; }
            set
            {
                if (value < 0) colRotatAngle_alpha = -value;
                else colRotatAngle_alpha = value;
            }
        }
        public double CRF_OMEGA
        {
            get { return colRotatFriquency_omega; }
            set
            {
                if (value < 0) colRotatFriquency_omega = -value;
                else colRotatFriquency_omega = value;
            }
        }
        //Calculated properties
        public double RST_Trst
        {
            get { return RST_crane_cycleTime_Trst; }
            set { RST_crane_cycleTime_Trst = value; }
        }
        public double PST_Tpst
        {
            get { return PST_crane_cycleTime_Tpst; }
            set { PST_crane_cycleTime_Tpst = value; }
        }
        //Constructor
        public StacerCrane_CycleTime()
        {
            avCargoCraneDistance_Lk = 1;
            avCargoLiftingHeight_D = 1;
            avTrolleyCraneDistance_Lt = 1;
            craneMovLift_Factor_fi = 1;
            cargoWidth_b = 1;
            craneSpeed_Vc = 1;
            carriageLiftSpeed_Vcl = 1;
            extTelescCargoLoaderSpeed_Vet = 1;
            overtime_to = 1;
            colRotatAngle_alpha = 1;
            colRotatFriquency_omega = 1;
        }
        //Methods
        //Расчет продолжительности цикла стеллажного крана-штабелера
        public void RST_CycleTime_Trst()
        {
            RST_crane_cycleTime_Trst = 2 * ((avCargoCraneDistance_Lk / craneSpeed_Vc) + ((avCargoLiftingHeight_D + 0.2) / carriageLiftSpeed_Vcl) * craneMovLift_Factor_fi + ((cargoWidth_b + 0.1) / extTelescCargoLoaderSpeed_Vet)) + overtime_to;
        }
        //Расчет продолжительности цикла мостового крана-штабелера
        public void PST_CycleTime_Tpst()
        {
            PST_crane_cycleTime_Tpst = 2 * ((avCargoCraneDistance_Lk / craneSpeed_Vc) + (avTrolleyCraneDistance_Lt / extTelescCargoLoaderSpeed_Vet) + (colRotatAngle_alpha / (360 * colRotatFriquency_omega))) + 2 * ((avCargoLiftingHeight_D + 0.2) / carriageLiftSpeed_Vcl) * craneMovLift_Factor_fi + overtime_to;
        }
    }
    //3.8 Расчет потребного количества основных производственных рабочих мест с учетом многопоточного обслуживания станков с ЧПУ
    //Количество операторов по трудоемкости
    public class NumOfOperators_viaLabInt
    {
        //Private fields
        //Задаваемые данные
        private double actualAnnualFund_Fa; //Действительный годовой фонд работы оператора
        private int annualRelease_D; //Годовая норма выпуска
        private double numMachinesPerOperator_d; //Число станков, обслуживаемых одним оператором
        private double num_Intence_Factor_Knum; //Коэффициент, зависящий от числа обслуживаемых станков и учитывающий интенсификацию труда при многопоточном обслуживании
        private double unitTimeRate_one_Tut_; //Штучное время при работе оператора на одном станке
        //Вычисляемые данные
        private Stack<double> summaryIntencity_Tsumm; //Суммарная трудоемкость обработки годового кол-ва деталей
        private double unitTimeRate_Tut; //Норма штучного времени на i-ом станке
        private double numOfOperators_Pop; //Количество операторов
        

        //Properties
        public double ANF_Fa
        {
            get { return actualAnnualFund_Fa; }
            set
            {
                if (value == 0) actualAnnualFund_Fa = 1;
                else if (value < 0) actualAnnualFund_Fa = -value;
                else actualAnnualFund_Fa = value;
            }
        }
        public int AR_D
        {
            get { return annualRelease_D; }
            set
            {
                if (value < 0) annualRelease_D = -value;
                else annualRelease_D = value;
            }
        }
        public double NMPO_d
        {
            get { return numMachinesPerOperator_d; }
            set
            {
                if (value < 0) numMachinesPerOperator_d = -value;
                else numMachinesPerOperator_d = value;
            }
        }
        public double NIF_Knum
        {
            get { return num_Intence_Factor_Knum; }
            set
            {
                if (value < 0) num_Intence_Factor_Knum = -value;
                else num_Intence_Factor_Knum = value;
            }
        }
        public double UTRO_Tut_
        {
            get { return unitTimeRate_one_Tut_; }
            set
            {
                if (value < 0) unitTimeRate_one_Tut_ = -value;
                else unitTimeRate_one_Tut_ = value;
            }
        }
        public Stack<double> SI_Tsumm
        {
            get { return summaryIntencity_Tsumm; }
        }
        public double UTR_Tut
        {
            get { return unitTimeRate_Tut; }
            set { unitTimeRate_Tut = value; }
        }
        public double NO_Pop
        {
            get { return numOfOperators_Pop; }
            set { numOfOperators_Pop = value; }
        }
        
        //Constructor
        public NumOfOperators_viaLabInt()
        {
            actualAnnualFund_Fa = 1;
            annualRelease_D = 1;
            numMachinesPerOperator_d = 1;
            num_Intence_Factor_Knum = 1;
        }
        //Methods
        //Расчет штучного времени  на одном станке
        public void UTR_Method_Tut()
        {
            unitTimeRate_Tut = (unitTimeRate_one_Tut_ / numMachinesPerOperator_d) * num_Intence_Factor_Knum;
        }
        //Расчет суммарной трудоемкости
        public void SI_Method_Tsumm()
        {
            summaryIntencity_Tsumm.Push(unitTimeRate_one_Tut_);
        }
        //Очистить стэк
        public void SI_DEL_Tsumm()
        {
            summaryIntencity_Tsumm.Clear();
        }
        //Расчет количества оператров по трудоемкости
        public void NO_Method_Pop()
        {
            double summT = 0;
            foreach(double el in summaryIntencity_Tsumm)
            {
                summT += el;
            }
            numOfOperators_Pop = summT / actualAnnualFund_Fa;
        }
    }
    //Кол-во операторов по числу станков на линии
    public class NumOfOperators_viaMachNum
    {
        //Private fields
        //Задаваемые значения
        private double realFund_Fr; //Действительный годовой фонд работы оборудования
        private int aprMachNum_Ca; //Принятое количество станков
        private double facilBusyFactor_Kb; //Коэффициент загрузки оборудования
        private double actualAnnualFund_Fa; //Действительный годовой фонд работы оператора
        private double numMachinesPerOperator_d; //Число станков, обслуживаемых одним оператором
        //Вычисляемые значения
        private double numOfOperators_Pop; //Количество операторов

        //Properties
        public double RF_Fr
        {
            get { return realFund_Fr; }
            set
            {
                if (value < 0) realFund_Fr = -value;
                else realFund_Fr = value;
            }
        }
        public int AMN_Ca
        {
            get { return aprMachNum_Ca; }
            set
            {
                if (value < 0) aprMachNum_Ca = -value;
                else aprMachNum_Ca = value;
            }
        }
        public double FBF_Kb
        {
            get { return facilBusyFactor_Kb; }
            set
            {
                if (value < 0) facilBusyFactor_Kb = -value;
                else facilBusyFactor_Kb = value;
            }
        }
        public double AAF_Fa
        {
            get { return actualAnnualFund_Fa; }
            set
            {
                if (value == 0) actualAnnualFund_Fa = 1;
                else if (value < 0) actualAnnualFund_Fa = -value;
                else actualAnnualFund_Fa = value;
            }
        }
        public double NMPO_d
        {
            get { return numMachinesPerOperator_d; }
            set
            {
                if (value == 0) numMachinesPerOperator_d = 1;
                else if (value < 0) numMachinesPerOperator_d = -value;
                else numMachinesPerOperator_d = value;
            }
        }
        public double NOO_Pop
        {
            get { return numOfOperators_Pop; }
            set { numOfOperators_Pop = value; }
        }
        //Constructor
        public NumOfOperators_viaMachNum()
        {
            realFund_Fr = 1;
            aprMachNum_Ca = 1;
            facilBusyFactor_Kb = 1;
            actualAnnualFund_Fa = 1;
            numMachinesPerOperator_d = 1;
        }
        //Methods
        //Расчет кол-вы операторов по кол-ву станков на линии
        public void NO_Method_Pop()
        {
            numOfOperators_Pop = (double)((realFund_Fr*aprMachNum_Ca*facilBusyFactor_Kb)/(actualAnnualFund_Fa*numMachinesPerOperator_d));
        }
    }
    //Кол-во производственных рабочих сборочного участка
    public class NumOfWorkers_AssembSite
    {
        //Private fields
        //Задаваемые значения
        private Stack<double> assemblLabIntence_Tas; //Трудоемкость сборочных работ для сборки i-го узла или изделия, час
        private int annualRelease_Di; //Годовой выпуск i-го узла или изделия
        private double actualAnnualFund_Fa; //Действительный годовой фонд времени работы оператора
        //Вычисляемые значения
        private double numOfWorkers_Pw; //Кол-во рабочих сборочного участка

        //Properties
        public Stack<double> ALI_Tas
        {
            get { return assemblLabIntence_Tas; }
        }
        public int AR_Di
        {
            get { return annualRelease_Di; }
            set
            {
                if (value < 0) annualRelease_Di = -value;
                else annualRelease_Di = value;
            }
        }
        public double AAF_Fa
        {
            get { return actualAnnualFund_Fa; }
            set
            {
                if (value == 0) actualAnnualFund_Fa = 1;
                else if (value < 0) actualAnnualFund_Fa = -value;
                else actualAnnualFund_Fa = value;
            }
        }
        public double NOW_Pw
        {
            get { return numOfWorkers_Pw; }
            set { numOfWorkers_Pw = value; }
        }
        //Constructor
        public NumOfWorkers_AssembSite()
        {
            assemblLabIntence_Tas = new Stack<double>();
            annualRelease_Di = 1;
            actualAnnualFund_Fa = 1;
        }
        //Methods
        //Добавить в стэк трудоемкости
        public void AddTas(double el)
        {
            assemblLabIntence_Tas.Push(el);
        }
        //Очистить стэк
        public void DeleteTas()
        {
            assemblLabIntence_Tas.Clear();
        }
        //Вычислить число рабочих
        public void NOW_Method_Pw()
        {
            double summTas = 0;

            foreach(double el in assemblLabIntence_Tas)
            {
                summTas += el;
            }
            numOfWorkers_Pw = (double)((summTas * annualRelease_Di) / actualAnnualFund_Fa);
        }
    }
    //Число сборщиков на сборочной операции при поточной сборке
    public class NumOfWorkers_FlowAssembly
    {
        //Private fields
        //Задаваемые значения
        private double partTime_Tp; //Штучное время на выполнение i-той сборочной операции
        private double stepTime_Tau; //Такт рабочей линии
        //Вычисляемые значения
        private Stack<double>  numOfWorkers_Pw; //Число сборщиков на одной сборке
        private double allNumWorkers_P; //Общее число сборщиков


        //Properties
        public double PT_Tp
        {
            get { return partTime_Tp; }
            set
            {
                if (value < 0) partTime_Tp = -value;
                else partTime_Tp = value;
            }
        }
        public double ST_Tau
        {
            get { return stepTime_Tau; }
            set
            {
                if (value == 0) stepTime_Tau = 1;
                else if (value < 0) stepTime_Tau = -value;
                else stepTime_Tau = value;
            }
        }
        public Stack<double> NOW_Pw
        {
            get { return numOfWorkers_Pw; }
            
        }
        public double ANOW_P
        {
            get { return allNumWorkers_P; }
            set { allNumWorkers_P = value; }
        }
        //Constructor
        public NumOfWorkers_FlowAssembly()
        {
            partTime_Tp = 1;
            stepTime_Tau = 1;
            numOfWorkers_Pw = new Stack<double>();
            allNumWorkers_P = 0;
        }
        //Methods
        //Число работников на сборку
        public void NOW_Method_Pw()
        {
            numOfWorkers_Pw.Push(partTime_Tp / stepTime_Tau);
        }
        public void NOW_SUMM_METHOD_P()
        {
            foreach(double el in numOfWorkers_Pw)
            {
                allNumWorkers_P += el;
            }
        }

    }
    //Расчет экономической эффективности от внедрения автоматизированных участков
    //Условие экномической целесообразности разработки проекта
    public class EconomicResponsebility
    {
        //Private fields
        private double mainCost_K0base; //Капитальные затраты в базовом варианте
        private double costAllowableFactor_Kal; //Коэффициент допустимого увеличения стоимости ГАП
        private double facilCost_C0; //Стоимость оборудования ГАП

        //Properties
        public double MC_K0base
        {
            get { return mainCost_K0base; }
            set
            {
                if (value < 0) mainCost_K0base = -value;
                else mainCost_K0base = value;
            }
        }
        public double CAF_Kal
        {
            get { return costAllowableFactor_Kal; }
            set
            {
                if (value < 0) costAllowableFactor_Kal = -value;
                else costAllowableFactor_Kal = value;
            }
        }
        public double FC_C0
        {
            get { return facilCost_C0; }
            set
            {
                if (value < 0) facilCost_C0 = -value;
                else facilCost_C0 = value;
            }
        }
        //Constructor
        public EconomicResponsebility()
        {
            mainCost_K0base = 1;
            costAllowableFactor_Kal = 1;
            facilCost_C0 = 1;
        }
        //Method
        //Условие экономической целесообразности
        public bool IsResponse()
        {
            //Испльзуется тернарный оператор для краткости
           return  (facilCost_C0 <= costAllowableFactor_Kal*mainCost_K0base) ?  true: false;
        }
    }
    //Целесообразность перевода деталей на обработку в ГАП
    public class ExpediencyOfTranslation
    {
        //Private fields
        //Задаваемые значения
        private double mechAssemblCost_Casc; //Стоимость механической сборки
        private double shiftIncreaseFactor_Ksi; //Коэффициент увеличения сменности
        private double costDecreaseFactor_Kcd; //Коэффициент уменьшения стоимости оборотных средств
        private double hourlyRate_Hr; //Часовая ставка наладчика, руб
        private double bonusFactor_Kb; //Коэффициент размера премии
        private double adjustCostFactor_Kac; //Коэффициент наладочных расходов
        private int yearRelease_N; //Годовая программа выпуска деталей
        private int numOfOperations_m; //Число операций
        private double interoperationTime_n; //Время межоперационного пролеживания
        private Stack<double> mechLabIntence_Tmec; //Трудоемкость механообработки детали, час
        private int numOfParts_np; //Количество деталей в партии
        private double partSelfCost_Cpc; //Себестоимость детали
        private int numWorkDays_Fwd; //Число рабочих дней
        private double costIncreaseFactor_Kif; //Коэффициент нарастания стоимости
        private double lostPercentage_Klp; //Коэффициент потерь от связывания оборотных средств в незавершенном производстве
        //?? Задать или вычислить - по графику
        private double minTotalLoses_Pmin; //Минимальные суммарные потери, руб
        //...
        private double numReqAdjOperations_Nop; //Число операций, требующих наладки
        private double adjustmentIntencity_Tadj; //Трудоемкость одной наладки
        private double numOfRun_Nr; //Число запусков
        //Вычисляемые значения
        private double costIncreaseFactor_Kinc; //Коэфиц. увеличения стоимости в ГАП
        private double labIntencionYear_Tly; //Трудоемкость годового объема наладки
        private double adjustCost_Zac; //Затраты на наладку 1 детали
        private double interopTimeGlobal_Tit; //Время межоперационного пролеживания
        private double industrialCycle_Tif; //Производственный цикл, час
        private double onetimeCosts_H; //Единовременные затраты в оборотные дни, руб
        private double onetimeCostsPerPart_Hp; //Единовременные затраты на 1 деталь
        private double lostCosts_Hlc; //Потери от связывания оборотных средств

        //Properties
        public double MAC_Casc
        {
            get { return mechAssemblCost_Casc; }
            set
            {
                if (value == 0) mechAssemblCost_Casc = 1;
                else if (value < 0) mechAssemblCost_Casc = -value;
                else mechAssemblCost_Casc = value;
            }
        }
        public double SIF_Ksi
        {
            get { return shiftIncreaseFactor_Ksi; }
            set
            {
                if (value < 0) shiftIncreaseFactor_Ksi = -value;
                else shiftIncreaseFactor_Ksi = value;
            }
        }
        public double CDF_Kcd
        {
            get { return costDecreaseFactor_Kcd; }
            set
            {
                if (value == 0) costDecreaseFactor_Kcd = 1;
                else if (value < 0) costDecreaseFactor_Kcd = -value;
                else costDecreaseFactor_Kcd = value;
            }
        }
        public double NRAO_Nop
        {
            get { return numReqAdjOperations_Nop; }
            set
            {
                if (value < 0) numReqAdjOperations_Nop = -value;
                else numReqAdjOperations_Nop = value;
            }
        }
        public double NI_Tadj
        {
            get { return adjustmentIntencity_Tadj; }
            set
            {
                if (value < 0) adjustmentIntencity_Tadj = -value;
                else adjustmentIntencity_Tadj = value;
            }
        }
        public double NOR_Nr
        {
            get { return numOfRun_Nr; }
            set
            {
                if (value < 0) numOfRun_Nr = -value;
                else numOfRun_Nr = value;
            }
        }
        public double HR_Hr
        {
            get { return hourlyRate_Hr; }
            set
            {
                if (value < 0) hourlyRate_Hr = -value;
                else hourlyRate_Hr = value;
            }
        }
        public double BF_Kb
        {
            get { return bonusFactor_Kb; }
            set
            {
                if (value < 0) bonusFactor_Kb = -value;
                else bonusFactor_Kb = value;
            }
        }
        public double ACF_Kac
        {
            get { return adjustCostFactor_Kac; }
            set
            {
                if (value < 0) adjustCostFactor_Kac = -value;
                else adjustCostFactor_Kac = value;
            }
        }
        public int YR_N
        {
            get { return yearRelease_N; }
            set
            {
                if (value == 0) yearRelease_N = 1;
                else if (value < 0) yearRelease_N = -value;
                else yearRelease_N = value;
            }
        }
        public int NOO_m
        {
            get { return numOfOperations_m; }
            set
            {
                if (value < 0) numOfOperations_m = -value;
                else numOfOperations_m = value;
            }
        }
        public double IT_n
        {
            get { return interoperationTime_n; }
            set
            {
                if (value < 0) interoperationTime_n = -value;
                else interoperationTime_n = value;
            }
        }
        public Stack<double> MLI_Tmec
        {
            get { return mechLabIntence_Tmec; }
        }
        public int NOP_np
        {
            get { return numOfParts_np; }
            set
            {
                if (value == 0) numOfParts_np = 1;
                else if (value < 0) numOfParts_np = -value;
                else numOfParts_np = value;
            }
        }
        public double PSC_Cpc
        {
            get { return partSelfCost_Cpc; }
            set
            {
                if (value < 0) partSelfCost_Cpc = -value;
                else partSelfCost_Cpc = value;
            }
        }
        public int NWD_Fwd
        {
            get { return numWorkDays_Fwd; }
            set
            {
                if (value == 0) numWorkDays_Fwd = 1;
                else if (value < 0) numWorkDays_Fwd = -value;
                else numWorkDays_Fwd = value;
            }
        }
        public double CIF_Kif
        {
            get { return costIncreaseFactor_Kif; }
            set
            {
                if (value < 0) costIncreaseFactor_Kif = -value;
                else costIncreaseFactor_Kif = value;
            }
        }
        public double LP_Klp
        {
            get { return lostPercentage_Klp; }
            set
            {
                if (value < 0) lostPercentage_Klp = -value;
                else lostPercentage_Klp = value;
            }
        }
        public double MTL_Pmin
        {
            get { return minTotalLoses_Pmin; }
            set { minTotalLoses_Pmin = value; }
        }
        public double CIF_Kinc
        {
            get { return costIncreaseFactor_Kinc; }
            set { costIncreaseFactor_Kinc = value; }
        }
        public double LIY_Tly
        {
            get { return labIntencionYear_Tly; }
            set { labIntencionYear_Tly = value; }
        }
        public double AC_Zac
        {
            get { return adjustCost_Zac; }
            set { adjustCost_Zac = value; }
        }
        public double ITG_Tit
        {
            get { return interopTimeGlobal_Tit; }
            set { interopTimeGlobal_Tit = value; }
        }
        public double IC_Tif
        {
            get { return industrialCycle_Tif; }
            set { industrialCycle_Tif = value; }
        }
        public double OC_H
        {
            get { return onetimeCosts_H; }
            set { onetimeCosts_H = value; }
        }
        public double OCPP_Hp
        {
            get { return onetimeCostsPerPart_Hp; }
            set { onetimeCostsPerPart_Hp = value; }
        }
        public double LC_Hlc
        {
            get { return lostCosts_Hlc; }
            set { lostCosts_Hlc = value; }
        }
        //Constructor
        public ExpediencyOfTranslation()
        {
             mechAssemblCost_Casc = 1; 
             shiftIncreaseFactor_Ksi = 1; 
             costDecreaseFactor_Kcd = 1; 
             hourlyRate_Hr = 1; 
             bonusFactor_Kb = 1;
             adjustCostFactor_Kac = 1; 
             yearRelease_N = 1; 
             numOfOperations_m = 1;
             interoperationTime_n = 1; 
             mechLabIntence_Tmec = new Stack<double>();
             numOfParts_np = 1; 
             partSelfCost_Cpc = 1;
             numWorkDays_Fwd = 1; 
             costIncreaseFactor_Kif = 1; 
             lostPercentage_Klp = 1; 
            
             minTotalLoses_Pmin = 1; 
            
             numReqAdjOperations_Nop = 0; 
             adjustmentIntencity_Tadj = 0; 
             numOfRun_Nr = 0;
        
             costIncreaseFactor_Kinc = 0; 
             labIntencionYear_Tly = 0; 
             adjustCost_Zac = 0; 
             interopTimeGlobal_Tit = 0;
             industrialCycle_Tif = 0; 
             onetimeCosts_H = 0; 
             onetimeCostsPerPart_Hp = 0; 
             lostCosts_Hlc = 0; 
    }
        //Methods
        //Добавить в стэк
        public void AddTmec(double el)
        {
            mechLabIntence_Tmec.Push(el);
        }
        //Очистить стэк
        public void DeleteTmec()
        {
            mechLabIntence_Tmec.Clear();
        }
        //Целесообразность перевода деталей на ГАП
        public bool IsAdvisability()
        {
            return (costIncreaseFactor_Kinc <= (1 + (minTotalLoses_Pmin / (2 * mechAssemblCost_Casc))) * (shiftIncreaseFactor_Ksi / costDecreaseFactor_Kcd)) ? true : false;
        }
        //Трудоемкость годового объема наладки
        public void LIY_Method_Tly()
        {
            labIntencionYear_Tly = numReqAdjOperations_Nop * adjustmentIntencity_Tadj * numOfRun_Nr;
        }
        //Затраты на наладку 1 детали
        public void AC_Method_Zac()
        {
            adjustCost_Zac = (double)((labIntencionYear_Tly*hourlyRate_Hr*bonusFactor_Kb*adjustCostFactor_Kac) / yearRelease_N);
        }
        //Время межоперационного пролеживания
        public void ITG_Method_Tit()
        {
            interopTimeGlobal_Tit = numOfOperations_m * (interoperationTime_n - 1);
        }
        //Производственный цикл
        public void IC_Method_Tif()
        {
            double summ = 0;
            foreach(double el in mechLabIntence_Tmec)
            {
                summ += el;
            }
            industrialCycle_Tif = summ * numOfParts_np + numOfOperations_m * (interoperationTime_n - 1);
        }
        //Единовременные затраты в оборотные фонды
        public void OC_Method_H()
        {
            onetimeCosts_H = industrialCycle_Tif * partSelfCost_Cpc * (yearRelease_N / numWorkDays_Fwd) * costIncreaseFactor_Kif;
        }
        //Единовременные затраты на 1 деталь
        public void OCPP_Method_Hp()
        {
            onetimeCostsPerPart_Hp = onetimeCosts_H / numOfParts_np;
        }
        //Потери от оборотных средств
        public void LC_Method_Hlc()
        {
            lostCosts_Hlc = (onetimeCosts_H / numOfParts_np) * lostPercentage_Klp;
        }
    }
    //Экономия за счет сокращения доделочных и подгоночных работ
    public class ShorteringFinWorks
    {
        //Private fields
        //Задаваемые данные
        private double averageIntenceFW_tfin; //Средняя трудоемкость доделочных работ
        private double tarifHourlyRate_Hr; //Тарифная часовая ставка
        private int yearRelease_N; //Годовая программа выпуска
        private double averageIntenceAW_tadj; //Средняя трудоемкость подгоночных работ
        //Вычисляемые данные
        private double FW_Economy_Zfw; //Экономия за счет сокращения доделочных работ
        private double AW_Economy_Zaw; //Экономия за счет сокращения подгоночных работ

        //Properties
        public double AI_FW_tfin
        {
            get { return averageIntenceFW_tfin; }
            set
            {
                if (value < 0) averageIntenceFW_tfin = -value;
                else averageIntenceFW_tfin = value;
            }
        }
        public double THR_Hr
        {
            get { return tarifHourlyRate_Hr; }
            set
            {
                if (value < 0) tarifHourlyRate_Hr = -value;
                else tarifHourlyRate_Hr = value;
            }
        }
        public int YR_N
        {
            get { return yearRelease_N; }
            set
            {
                if (value < 0) yearRelease_N = -value;
                else yearRelease_N = value;
            }
        }
        public double AI_AW_tadj
        {
            get { return averageIntenceAW_tadj; }
            set
            {
                if (value < 0) averageIntenceAW_tadj = -value;
                else averageIntenceAW_tadj = value;
            }
        }
        public double FWE_Zfw
        {
            get { return FW_Economy_Zfw; }
            set { FW_Economy_Zfw = value; }
        }
        public double AWE_Zaw
        {
            get { return AW_Economy_Zaw; }
            set { AW_Economy_Zaw = value; }
        }
        //Constructor
        public ShorteringFinWorks()
        {
            averageIntenceFW_tfin = 1;
            tarifHourlyRate_Hr = 1;
            yearRelease_N = 1;
            averageIntenceAW_tadj = 1;

            FW_Economy_Zfw = 0;
            AW_Economy_Zaw = 0;
        }
        //Methods
        //Экономия сокращения доделочных работ
        public void FWE_Method_Zfw()
        {
            FW_Economy_Zfw = (double)(1.57 * averageIntenceFW_tfin * tarifHourlyRate_Hr * yearRelease_N);
        }
        //Экономия сокращения подгоночных работ
        public void AWE_Method_Zaw()
        {
            AW_Economy_Zaw = (double)(1.57 * averageIntenceAW_tadj * tarifHourlyRate_Hr * yearRelease_N);
        }
    }
    //Экономия единовременных затрат, вложенных в оборотные фонды
    public class EconomyOnetimeCosts
    {
        //Private fields
        //Задаваемые величины
        private double averageCycleReduce_Tc; //Среднее сокращение длительности цикла
        private int yearRelease_N; //Годовая программа выпуска
        private int numOfWorkdays_Fw; //Число рабочих дней
        private double fullSelfcost_Cf; //Полная себестоимость детали
        private double costIncreaseFactor_Kci; //Коэффициент возрастания затрат
        //Задаваемые величины
        private double RF_Economy_H; //Экономия единовременных затрат, вложенных в оборотные фонды

        //Propertise
        public double ACR_Tc
        {
            get { return averageCycleReduce_Tc; }
            set
            {
                if (value < 0) averageCycleReduce_Tc = -value;
                else averageCycleReduce_Tc = value;
            }
        }
        public int YR_N
        {
            get { return yearRelease_N; }
            set
            {
                if (value < 0) yearRelease_N = -value;
                else yearRelease_N = value;
            }
        }
        public int NOW_Fw
        {
            get { return numOfWorkdays_Fw; }
            set
            {
                if (value == 0) numOfWorkdays_Fw = 1;
                else if (value < 0) numOfWorkdays_Fw = -value;
                else numOfWorkdays_Fw = value;
            }
        }
        public double FS_Cf
        {
            get { return fullSelfcost_Cf; }
            set
            {
                if (value < 0) fullSelfcost_Cf = -value;
                else fullSelfcost_Cf = value;
            }
        }
        public double CIF_Kci
        {
            get { return costIncreaseFactor_Kci; }
            set
            {
                if (value < 0) costIncreaseFactor_Kci = -value;
                else costIncreaseFactor_Kci = value;
            }
        }
        public double RFE_H
        {
            get { return RF_Economy_H; }
            set { RF_Economy_H = value; }
        }
        //Constructor
        public EconomyOnetimeCosts()
        {
            averageCycleReduce_Tc = 1;
            yearRelease_N = 1;
            numOfWorkdays_Fw = 1;
            fullSelfcost_Cf = 1;
            costIncreaseFactor_Kci = 1;

            RF_Economy_H = 0;
        }
        //Method
        //Экономия единовременных затрат
        public void RFE_Method_H()
        {
            RF_Economy_H = (double)(averageCycleReduce_Tc * (yearRelease_N / numOfWorkdays_Fw) * fullSelfcost_Cf * costIncreaseFactor_Kci);
        }
    }
    //Экономия за счет высвобождения сборочной оснастки
    public class EconomyAssemblyRigging
    {
        //Private fields
        //Задаваемые значения
        private double riggingSelfcost_Cs; //Себестоимость оснастки
        private double decreaseIntence_ta; //Снижение трудоемкости сборочных работ
        private int numRiggAssemblNodes_Na; //Количество узлов, собираемых в оснастке данного наименования
        private double compilStandartFactor_Kcs; //Коэффициент выполнения норм
        private double actualRiggYearFund_Far; //Действительный годовой фонд работы оснастки
        private int avNumOnetimeWorkers_x; //Среднее кол-во одновременно работающих
        //Вычисляемые значения
        private int amountReleasedRigg_Narr; //Количество высвобожденной сборочной оснастки данного наименования
        private double AR_Economy_Zar; //Экономия за счет высвобождения сборочной оснастки
        
        //Properties
        public double RS_Cs
        {
            get { return riggingSelfcost_Cs; }
            set
            {
                if (value < 0) riggingSelfcost_Cs = -value;
                else riggingSelfcost_Cs = value;
            }
        }
        public double DI_ta
        {
            get { return decreaseIntence_ta; }
            set
            {
                if (value < 0) decreaseIntence_ta = -value;
                else decreaseIntence_ta = value;
            }
        }
        public int NRAN_Na
        {
            get { return numRiggAssemblNodes_Na; }
            set
            {
                if (value < 0) numRiggAssemblNodes_Na = -value;
                else numRiggAssemblNodes_Na = value;
            }
        }
        public double CSF_Kcs
        {
            get { return compilStandartFactor_Kcs; }
            set
            {
                if (value == 0) compilStandartFactor_Kcs = 1;
                else if (value < 0) compilStandartFactor_Kcs = -value;
                else compilStandartFactor_Kcs = value;
            }
        }
        public double ARYF_Far
        {
            get { return actualRiggYearFund_Far; }
            set
            {
                if (value == 0) actualRiggYearFund_Far = 1;
                else if (value < 0) actualRiggYearFund_Far = -value;
                else actualRiggYearFund_Far = value;
            }
        }
        public int ANOTW_x
        {
            get { return avNumOnetimeWorkers_x; }
            set
            {
                if (value == 0) avNumOnetimeWorkers_x = 1;
                else if (value < 0) avNumOnetimeWorkers_x = -value;
                else avNumOnetimeWorkers_x = value;
            }
        }
        public int ARR_Narr
        {
            get { return amountReleasedRigg_Narr; }
            set { amountReleasedRigg_Narr = value; }
        }
        public double ARE_Zar
        {
            get { return AR_Economy_Zar; }
            set { AR_Economy_Zar = value; }
        }
        //Constructor
        public EconomyAssemblyRigging()
        {
            riggingSelfcost_Cs = 1;
            decreaseIntence_ta = 1;
            numRiggAssemblNodes_Na = 1;
            compilStandartFactor_Kcs = 1;
            actualRiggYearFund_Far = 1;
            avNumOnetimeWorkers_x = 1;

            amountReleasedRigg_Narr = 0;
            AR_Economy_Zar = 0;
        }
        //Methods
        //Количество высвобожденной оснастки
        public void ARR_Method_Narr()
        {
            amountReleasedRigg_Narr = (int)(Math.Floor((decreaseIntence_ta*numRiggAssemblNodes_Na)/(compilStandartFactor_Kcs*actualRiggYearFund_Far*avNumOnetimeWorkers_x)));
        }
        //Экономия высвобождения
        public void ARE_Method_Zar()
        {
            AR_Economy_Zar = (double)(amountReleasedRigg_Narr * riggingSelfcost_Cs);
        }
    }
    //Экономия  по снижению трудоемкости пеиода освоения
    public class EconomyDevelopmentPeriod
    {
        //Private fields
        //Задаваемые значения
        private Stack<double> labIntence_t; //Трудоемкость изготовления детали
        private double tarrifHourRate_Hr; //Часовая тарифная ставка рабочего 
        private double developmentFactor_Kd; //Коэффициент освоения
        private int manufactureProgramm_N; //Производственная программа
        //Вычисляемые значения
        private double DP_Economy_Zd; //Экономия по сниж. трудоемкости

        //Properties
        public Stack<double> LI_t
        {
            get { return labIntence_t; }
        }
        public double THR_Hr
        {
            get { return tarrifHourRate_Hr; }
            set
            {
                if (value < 0) tarrifHourRate_Hr = -value;
                else tarrifHourRate_Hr = value;
            }
        }
        public double DF_Kd
        {
            get { return developmentFactor_Kd; }
            set
            {
                if (value < 0) developmentFactor_Kd = -value;
                else developmentFactor_Kd = value;
            }
        }
        public int MP_N
        {
            get { return manufactureProgramm_N; }
            set
            {
                if (value < 0) manufactureProgramm_N = -value;
                else manufactureProgramm_N = value;
            }
        }
        public double DPE_Zd
        {
            get { return DP_Economy_Zd; }
            set { DP_Economy_Zd = value; }
        }
        //Constructor
        public EconomyDevelopmentPeriod()
        {
            labIntence_t = new Stack<double>();
            tarrifHourRate_Hr = 1;
            developmentFactor_Kd = 1;
            manufactureProgramm_N = 1;

            DP_Economy_Zd = 0;
        }
        //Methods
        //Добавить в стэк
        public void Add_t(double el)
        {
            labIntence_t.Push(el);
        }
        //Очистить стэк
        public void Delete_t()
        {
            labIntence_t.Clear();
        }
        //Экономия ... 
        public void DPE_Method_Zd()
        {
            double summ = 0;
            foreach(double el in labIntence_t)
            {
                summ += el;
            }
            DP_Economy_Zd = (double)(1.57 * summ * tarrifHourRate_Hr * developmentFactor_Kd * manufactureProgramm_N); 
        }
    }
    //Эффект от сокращения срока ввода в действие основных и оборотных фондов
    public class ShorteringPeriodEffect
    {
        //Private fields
        //Задаваемые данные
        private double shorteringPeriod_Tsp; //Сокращение срокоа ввода в действие основн и оборотн фондов, в долях года
        private double standarEffectFactor_Ese; //Нормативные коэффициент эффективности
        private double capitalInvestment_Ko; //Капиталовложение в оборудование
        private double onetimeTechInvestment_Kr; //Единовременные вложения в технологическую оснастку
        private double onetimeCircleFundInvestment_H; //Единовременные вложения в оборотные фонды
        //Вычисляемые данные
        private double costFixedAndCirculFund_Kcf; //Стоимость основных и оборотных фондов
        private double SP_Effect_Esp; //Эффект сокращ срока в действие

        //Properties
        public double SP_Tsp
        {
            get { return shorteringPeriod_Tsp; }
            set
            {
                if (value < 0) shorteringPeriod_Tsp = -value;
                else shorteringPeriod_Tsp = value;
            }
        }
        public double SEF_Ese
        {
            get { return standarEffectFactor_Ese; }
            set
            {
                if (value < 0) standarEffectFactor_Ese = -value;
                else standarEffectFactor_Ese = value;
            }
        }
        public double CI_Ko
        {
            get { return capitalInvestment_Ko; }
            set
            {
                if (value < 0) capitalInvestment_Ko = -value;
                else capitalInvestment_Ko = value;
            }
        }
        public double OTI_Kr
        {
            get { return onetimeTechInvestment_Kr; }
            set
            {
                if (value < 0) onetimeTechInvestment_Kr = -value;
                else onetimeTechInvestment_Kr = value;
            }
        }
        public double OCFI_H
        {
            get { return onetimeCircleFundInvestment_H; }
            set
            {
                if (value < 0) onetimeCircleFundInvestment_H = -value;
                else onetimeCircleFundInvestment_H = value;
            }
        }
        public double CFACF_Kcf
        {
            get { return costFixedAndCirculFund_Kcf; }
            set { costFixedAndCirculFund_Kcf = value; }
        }
        public double SPE_Esp
        {
            get { return SP_Effect_Esp; }
            set { SP_Effect_Esp = value; }
        }
        //Constructor
        public ShorteringPeriodEffect()
        {
            shorteringPeriod_Tsp = 1;
            capitalInvestment_Ko = 1;
            standarEffectFactor_Ese = 1;
            onetimeTechInvestment_Kr = 1;
            onetimeCircleFundInvestment_H = 1;

            costFixedAndCirculFund_Kcf = 0;
            SP_Effect_Esp = 0;
        }
        //Methods
        //Стоимость основных и оборотных фондов
        public void CFACF_Method_Kcf()
        {
            costFixedAndCirculFund_Kcf = capitalInvestment_Ko + onetimeTechInvestment_Kr + onetimeCircleFundInvestment_H;
        }
        //Эффект сокращения
        public void SPE_Method_Esp()
        {
            SP_Effect_Esp = costFixedAndCirculFund_Kcf * shorteringPeriod_Tsp * standarEffectFactor_Ese;
        }
    }
    //Экономия от роста дополнительной прибыли
    public class EconomyAdditionalProfit
    {
        //Private fields
        //Задаваемые данные
        private int numRelProdWorkers_Nrel; //Кол-во высвобожденных рабочих
        private double yearTimeFund_Fy; //Годовой фонд времени рабочего
        private double tarrifHourRate_Hr; //Часовая тарифная ставка
        private double salaryOverheads_Hso; //Накладные расходы на зар. плату
        private double matirealCostFactor_Kmc; //Коэффициент затрат на материалы
        private double profitabilityLevel_R; //Уровень рентабельности предприятия
        //Вычисляемые данные
        private double AP_Economy_Eap; //Экономия от роста прибыли

        //Properties
        public int NRPW_Nrel
        {
            get { return numRelProdWorkers_Nrel; }
            set
            {
                if (value < 0) numRelProdWorkers_Nrel = -value;
                else numRelProdWorkers_Nrel = value;
            }
        }
        public double YTF_Fy
        {
            get { return yearTimeFund_Fy; }
            set
            {
                if (value < 0) yearTimeFund_Fy = -value;
                else yearTimeFund_Fy = value;
            }
        }
        public double THR_Hr
        {
            get { return tarrifHourRate_Hr; }
            set
            {
                if (value < 0) tarrifHourRate_Hr = -value;
                else tarrifHourRate_Hr = value;
            }
        }
        public double SO_Hso
        {
            get { return salaryOverheads_Hso; }
            set
            {
                if (value < 0) salaryOverheads_Hso = -value;
                else salaryOverheads_Hso = value;
            }
        }
        public double MCF_Kmc
        {
            get { return matirealCostFactor_Kmc; }
            set
            {
                if (value < 0) matirealCostFactor_Kmc = -value;
                else matirealCostFactor_Kmc = value;
            }
        }
        public double PL_R
        {
            get { return profitabilityLevel_R; }
            set
            {
                if (value < 0) profitabilityLevel_R = -value;
                else profitabilityLevel_R = value;
            }
        }
        public double APE_Eap
        {
            get { return AP_Economy_Eap; }
            set { AP_Economy_Eap = value; }
        }
        //Constructor
        public EconomyAdditionalProfit()
        {
            numRelProdWorkers_Nrel = 1;
            yearTimeFund_Fy = 1;
            tarrifHourRate_Hr = 1;
            salaryOverheads_Hso = 1;
            matirealCostFactor_Kmc = 1;
            profitabilityLevel_R = 1;

            AP_Economy_Eap = 0;
        }
        //Methods
        //Экономия по прибыли
        public void APE_Method_Eap()
        {
            AP_Economy_Eap = (double)(1.57*numRelProdWorkers_Nrel*yearTimeFund_Fy*tarrifHourRate_Hr*(1+ ((salaryOverheads_Hso*matirealCostFactor_Kmc)/100))*profitabilityLevel_R);
        }
    }
}
