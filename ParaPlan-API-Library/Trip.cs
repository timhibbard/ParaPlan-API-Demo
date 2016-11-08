using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;
using System.Linq;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace ParaPlan.Entities
{
    /// <summary>
    /// The base class for objects that have the 'trips' structure
    /// </summary>
    /// <history>
    ///     [Tim Hibbard]   02/26/2007  Created
    ///     [Tim Hibbard]   03/19/2007  Abstracted dummy fields to DummyField Object
    ///     [Tim Hibbard]   05/23/2007  Implemented INotifiyPropertyChanged
    ///     [Tim Hibbard]   06/08/2007  Added WirePropertyChanged to bubble PropertyChanged events
    ///     [Tim Hibbard]   07/09/2008  Added Dispatch, PickUp, and DropOff methods
    ///     [Tim Hibbard]   09/04/2008  Added xmlinclude for subscription
    /// </history>
    [Serializable]
    [DataContract]
    public class Trip
    {
        #region --Fields--
        
        private int? _id;
        private int? _tripID;
        private string _tripType;
        private int? _appointmentID;
        private int? _subscriptionID;
        private Client _client;
        private User _user;
        private int? _age;
        private DateTime _tripDate = DateTime.MinValue;
        private DateTime? _tripTime = DateTime.MinValue;
        private Place _pickUpPlace;
        private Place _dropOffPlace;
        private string _returnType;
        private DateTime? _returnTime;
        private string _appointmentType;
        private double? _mileage;
        private Driver _driver;
        private DateTime _entryDate = DateTime.Now;
        private SubscriptionInformation _subscriptionInformation;
        private string _clientPrograms;
        private ClientProgram _tripProgram;
        private DateTime? _pickUpTime;
        private DateTime? _dropOffTime;
        private string _tripComment;
        private string _toPick;
        private string _toDrop;
        private DateTime? _TWa;
        private DateTime? _TWb;
        private DateTime? _TWc;
        private DateTime? _TWd;
        private bool? _excludeFromAutoSchedule;
        private bool? _scheduledByAutoSchedule;
        private bool? _went;
        private bool? _cancelled;
        private int? _cancelCode;
        private string _cancelReason;
        private DateTime? _cancelDate;
        private bool? _noGo;
        private int? _noGoCode;
        private string _noGoReason;
        private int? _groupID;
        private double? _travelDistance;
        private double? _travelTime;
        //private decimal? _cost1;
        //private decimal? _cost2;
        private decimal? _cost3;
        private string _cost4;
        private bool? _ap;
        private bool? _wp;
        private DummyFields _dummyFields;
        private bool? _billed;
        private string _billTo;
        private DateTime? _billDate;
        private string _invoiceID;
        private decimal? _billAmount;
        private bool? _paid;
        private string _payee;
        private DateTime? _payDate;
        private int? _checkNumber;
        private decimal? _checkAmount;
        private double? _donations;
        private double? _tickets;
        private DateTime? _pickUpStamp;
        private DateTime? _dropOffStamp;
        private FleetManager _fleetManager;
        private string _timeType;
        private Riders _riders;
        private List<Stop> _stops;
        private List<FleetManager> _availableRoutes;
        private int? _fleetManagerID;
        private TripBase _returnTrip;
        private TripBase _outgoingTrip;
        private Schedule _schedule;
        private MDTTripStatus _mdtTripStatus;
        public event EventHandler<System.ComponentModel.ProgressChangedEventArgs> AutoScheduleProgressChanged;
        private bool? _isDateSavedToHistory = null;
        private double? _pickUpOdometer;
        private double? _dropOffOdometer;
        private TripBilling _tripBilling;
        private bool _manuallySetDuration;
        private bool _manuallySetFees;
        private TripStreamEvent _pickUpArrive;
        private TripStreamEvent _pickUpPerform;
        private TripStreamEvent _dropOffArrive;
        private TripStreamEvent _dropOffPerform;
        private TripStreamEvent _signature;

        private TripStreamEvent _downloaded;





        #endregion

        #region CallBack Info
        //[XmlIgnore]
        //[RuleIgnore]
        //public bool CallbackNeedsToBeGenerated
        //{
        //    get
        //    {
        //        if (this.DummyFields.LI_D_1 == 0 && this.TripType.EndsWith("c",StringComparison.OrdinalIgnoreCase))
        //        {
        //            return true;
        //        }
        //        return false;
        //    }
        //}

        [XmlIgnore]
        [RuleIgnore]
        public bool IsCallBackTrip
        {
            get
            {
                if (this.TripType == "RT")
                {
                    return true;
                }
                return false;
            }
        }
        #endregion

        #region Broker Specific Properties
        private string _brokerTripID;
        private BrokerTripStatus _brokerTripStatus;
        private string _brokerClientID;
        private bool _brokerIsSelected;
        private string _brokerCallType;

        /// <summary>
        /// Gets or sets BrokerCallType
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 01/05/2012  Created
        /// </history>
        public string BrokerCallType
        {
            get { return _brokerCallType; }

            set
            {
                base.Setter(ref _brokerCallType, value, "BrokerCallType");
            }

        }


        /// <summary>
        /// Gets or sets BrokerIsSelected
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 12/19/2011  Created
        /// </history>
        public bool BrokerIsSelected
        {
            get { return _brokerIsSelected; }

            set
            {
                base.Setter(ref _brokerIsSelected, value, "BrokerIsSelected");
            }

        }


        /// <summary>
        /// Gets or sets BrokerClientID
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 12/19/2011  Created
        /// </history>
        public string BrokerClientID
        {
            get { return _brokerClientID; }

            set
            {
                base.Setter(ref _brokerClientID, value, "BrokerClientID");
            }

        }


        /// <summary>
        /// Gets or sets BrokerTripStatus
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 12/19/2011  Created
        /// </history>
        public BrokerTripStatus BrokerTripStatus
        {
            get { return _brokerTripStatus; }

            set
            {
                base.Setter(ref _brokerTripStatus, value, "BrokerTripStatus");
            }

        }


        /// <summary>
        /// Gets or sets BrokerTripID
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 12/19/2011  Created
        /// </history>
        public string BrokerTripID
        {
            get { return _brokerTripID; }

            set
            {
                base.Setter(ref _brokerTripID, value, "BrokerTripID");
            }

        }

        public bool BrokerIDIsValid
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.BrokerTripID))
                {
                    return false;
                }
                if (this.BrokerTripID.StartsWith("Job"))
                {
                    return false;
                }
                return true;
            }

        }

        #endregion

        #region FlexRoutes
        private FlexRouteResult _flexOptions = null;
        private FlexRouteStopResult _outgoingPUFlexRoute;
        private FlexRouteStopResult _outgoingDOFlexRoute;
        private FlexRouteStopResult _returnPUFlexRoute;
        private FlexRouteStopResult _returnDOFlexRoute;

        public bool OutgoingPUFlexSetManually { get; set; }
        public bool OutgoingDOFlexSetManually { get; set; }
        public bool ReturnPUFlexSetManually { get; set; }
        public bool ReturnDOFlexSetManually { get; set; }

        public void ParseDOFlexString(string db)
        {
            if (string.IsNullOrWhiteSpace(db))
            {
                return;
            }
            if (!IsFlex)
            {
                return;
            }

            var split = db.Split('|');
            if (split == null || split.Count() == 0)
            {
                return;
            }

            var outgoingLeg = split[0];
            var returnLeg = "";
            if (split.Count() > 1)
            {
                returnLeg = split[1];
            }

            if (outgoingLeg.StartsWith("M"))
            {
                this.OutgoingDOFlexSetManually = true;
                outgoingLeg = outgoingLeg.Replace("M", "");
            }
            this.OutgoingDOFlexRoute = Hub.FlexStopFromID(outgoingLeg);

            if (string.IsNullOrWhiteSpace(returnLeg))
            {
                return;
            }

            if (returnLeg.StartsWith("M"))
            {
                this.ReturnPUFlexSetManually = true;
                returnLeg = returnLeg.Replace("M", "");
            }

            this.ReturnPUFlexRoute = Hub.FlexStopFromID(returnLeg);

        }
        public void ParsePUFlexString(string db)
        {
            if (string.IsNullOrWhiteSpace(db))
            {
                return;
            }
            if (!IsFlex)
            {
                return;
            }
            var split = db.Split('|');
            if (split == null || split.Count() == 0)
            {
                return;
            }

            var outgoingLeg = split[0];
            var returnLeg = "";
            if (split.Count() > 1)
            {
                returnLeg = split[1];
            }

            if (outgoingLeg.StartsWith("M"))
            {
                this.OutgoingPUFlexSetManually = true;
                outgoingLeg = outgoingLeg.Replace("M", "");
            }
            this.OutgoingPUFlexRoute = Hub.FlexStopFromID(outgoingLeg);

            if (string.IsNullOrWhiteSpace(returnLeg))
            {
                return;
            }

            if (returnLeg.StartsWith("M"))
            {
                this.ReturnDOFlexSetManually = true;
                returnLeg = returnLeg.Replace("M", "");
            }

            this.ReturnDOFlexRoute = Hub.FlexStopFromID(returnLeg);
        }

        public string PUFlexStringForDB
        {
            get
            {
                if (!IsFlex)
                {
                    return "";
                }
                StringBuilder sb = new StringBuilder();
                sb.Append(DBStringFromFlexRoute(OutgoingPUFlexSetManually, OutgoingPUFlexRoute));
                sb.Append("|");
                sb.Append(DBStringFromFlexRoute(ReturnDOFlexSetManually, ReturnDOFlexRoute));
                return sb.ToString();


            }
        }

        public string DOFlexStringForDB
        {
            get
            {
                if (!IsFlex)
                {
                    return "";
                }
                var sb = new StringBuilder();
                sb.Append(DBStringFromFlexRoute(OutgoingDOFlexSetManually, OutgoingDOFlexRoute));
                sb.Append("|");
                sb.Append(DBStringFromFlexRoute(ReturnPUFlexSetManually, ReturnPUFlexRoute));
                return sb.ToString();

            }
        }

        private string DBStringFromFlexRoute(bool ManuallySet, FlexRouteStopResult stop)
        {
            var rv = "";
            if (stop != null)
            {
                if (ManuallySet)
                {
                    rv = rv + "M";
                }
                rv = rv + stop.Stop.ID.ToString() + "*" + stop.StopTime.ToString("HHmm");
            }
            return rv;
        }

        public string OutgoingPUFlexString
        {
            get
            {
                if (!this.IsFlex)
                {
                    return "";
                }
                if (this.OutgoingPUFlexRoute == null)
                {
                    return "";
                }
                return string.Format("{0} @ {1}", this.OutgoingPUFlexRoute.Route.Name, this.OutgoingPUFlexRoute.StopTime.ToShortTimeString());
            }
        }

        public string OutgoingDOFlexString
        {

            get
            {
                if (!this.IsFlex)
                {
                    return "";
                }
                if (this.OutgoingDOFlexRoute == null)
                {
                    return "";
                }

                return string.Format("{0} @ {1}", this.OutgoingDOFlexRoute.Route.Name, this.OutgoingDOFlexRoute.StopTime.ToShortTimeString());
            }

        }

        public string ReturnPUFlexString
        {

            get
            {
                if (!this.IsFlex)
                {
                    return "";
                }

                if (this.IsTrip)
                {
                    return "";
                }

                if (this.ReturnPUFlexRoute == null)
                {
                    return "";
                }

                if (this.OutgoingDOFlexRoute == null)
                {
                    return string.Format("{0} @ {1}", this.ReturnPUFlexRoute.Route.Name, this.ReturnPUFlexRoute.StopTime.ToShortTimeString());
                }

                if (this.OutgoingDOFlexRoute.Route.Name != this.ReturnPUFlexRoute.Route.Name)
                {
                    return string.Format("& {0} @ {1}", this.ReturnPUFlexRoute.Route.Name, this.ReturnPUFlexRoute.StopTime.ToShortTimeString());
                }

                return string.Format("& {0}", this.ReturnPUFlexRoute.StopTime.ToShortTimeString());
            }

        }

        public string ReturnDOFlexString
        {

            get
            {
                if (!this.IsFlex)
                {
                    return "";
                }

                if (this.IsTrip) { return ""; }

                if (this.ReturnDOFlexRoute == null)
                {
                    return "";
                }

                if (this.OutgoingPUFlexRoute == null)
                {
                    return string.Format("{0} @ {1}", this.ReturnDOFlexRoute.Route.Name, this.ReturnDOFlexRoute.StopTime.ToShortTimeString());
                }

                if (this.OutgoingPUFlexRoute.Route.Name != this.ReturnDOFlexRoute.Route.Name)
                {
                    return string.Format("& {0} @ {1}", this.ReturnDOFlexRoute.Route.Name, this.ReturnDOFlexRoute.StopTime.ToShortTimeString());
                }

                return string.Format("& {0}", this.ReturnDOFlexRoute.StopTime.ToShortTimeString());

            }

        }

        public bool IsFlex
        {
            get
            {
                return FileSpecsManager.Instance.DummyFields.TXT_D_2 == "K.A.R.T.";
            }
        }

        private DateTime GetFlexTimeForPickUp(FlexRouteStopResult Pick, FlexRouteStopResult Drop, bool isReturnLeg)
        {
            var rv = DateTime.MinValue;
            var time = this.TripTime;
            if (isReturnLeg) time = this.ReturnTime;
            if (this.TimeType == "P" || isReturnLeg)
            {
                int offset = 0;
                if (time.Minute > Pick.Stop.Time)
                {
                    //we are too early. Let's get them next loop
                    offset = 1;
                }
                return DateTime.Parse(time.AddHours(offset).ToString("MM/dd/yyyy HH:") + Pick.Stop.Time.ToFixedLength(2, '0'));
            }

            //it is drop off request
            if (Drop == null) return DateTime.MinValue;

            int dropOffset = -1;
            if (Drop.Route.Name == Pick.Route.Name && Drop.Stop.Time > Pick.Stop.Time)
            {
                //we are on the same route and we can be dropped off before going back to transfer
                dropOffset = 0;
            }

            return DateTime.Parse(Drop.StopTime.AddHours(dropOffset).ToString("MM/dd/yyyy HH:") + Pick.Stop.Time.ToFixedLength(2, '0'));

        }

        private DateTime GetFlexTimeForDropOff(FlexRouteStopResult Drop, FlexRouteStopResult Pick, bool isReturnLeg)
        {

            if (Pick == null) return DateTime.MinValue;

            var time = this.TripTime;
            if (isReturnLeg) time = this.ReturnTime;

            if (this.TimeType == "P" || isReturnLeg)
            {
                int pickOffset = 1;
                if (Pick.Route.Name == Drop.Route.Name && Drop.Stop.Time > Pick.Stop.Time)
                {
                    pickOffset = 0;
                }
                return DateTime.Parse(Pick.StopTime.AddHours(pickOffset).ToString("MM/dd/yyyy HH:") + Drop.Stop.Time.ToFixedLength(2, '0'));
            }

            int offset = 0;
            if (Drop.Stop.Time > time.Minute)
            {
                offset = -1;
            }
            return DateTime.Parse(time.AddHours(offset).ToString("MM/dd/yyyy HH:") + Drop.Stop.Time.ToFixedLength(2, '0'));

        }

        public List<FlexRouteStopResult> FlexAltOptions(bool isPick, bool isReturn)
        {
            if (!IsFlex) return null;

            var options = this.FlexOptions.PickUpRoutes;
            if ((!isReturn && !isPick) || (isReturn && isPick)) options = this.FlexOptions.DropOffRoutes;

            if (options == null || options.Count == 0) return null;

            var rv = new List<FlexRouteStopResult>();

            foreach (var item in options)
            {

                var option = item.Clone<FlexRouteStopResult>();
                var stopTime = DateTime.MinValue;

                if (!isReturn)
                {
                    if (isPick)
                    {
                        stopTime = GetFlexTimeForPickUp(item, this.OutgoingDOFlexRoute, false);
                    }
                    else
                    {
                        stopTime = GetFlexTimeForDropOff(item, this.OutgoingPUFlexRoute, false);
                    }
                }
                else
                {
                    if (isPick)
                    {
                        stopTime = GetFlexTimeForPickUp(item, this.ReturnDOFlexRoute, true);
                    }
                    else
                    {
                        stopTime = GetFlexTimeForDropOff(item, this.ReturnPUFlexRoute, true);
                    }
                }



                if (!stopTime.IsMinValue())
                {

                    option.StopTime = stopTime;

                    rv.Add(option);

                }

            }

            return rv;
        }



        public FlexRouteResult FlexOptions
        {
            get
            {
                if (!IsFlex)
                {
                    return null;
                }
                return FlexRoute.RouteOptions(Hub.Kart, this);
                //return _flexOptions ?? (_flexOptions = FlexRoute.RouteOptions(Hub.Kart, this));
            }
        }

        #region Assigned Routes

        private bool ShouldCalculateFlexRoute()
        {

            if (this.TripDate.Date < DateTime.Now.Date)
            {
                return false;
            }

            if (!IsFlex)
            {
                return false;
            }

            if (this.TripID == 0)
            {
                return true;
            }

            if (this.PickUpPlace.Address.GeoResult.Contains("|") || this.DropOffPlace.Address.GeoResult.Contains("|"))
            {
                return true;
            }

            return false;

        }

        /// <summary>
        /// Gets or sets ReturnDOFlexRoute
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/27/2014  Created
        /// </history>
        [RuleIgnore]
        [XmlIgnore]
        public FlexRouteStopResult ReturnDOFlexRoute
        {
            get
            {
                if (!ShouldCalculateFlexRoute())
                {
                    return _returnDOFlexRoute;
                }
                if (_returnDOFlexRoute != null && this.ReturnDOFlexSetManually)
                {
                    return _returnDOFlexRoute;
                }
                if (this.ReturnType == "One-Way: No return" || this.ReturnType == "Roundtrip: Call for return")
                {
                    return null;
                }
                if (this.ReturnTime == DateTime.MinValue)
                {
                    return null;
                }

                if (!this.PickUpLocation.IsGeocoded)
                {
                    return null;
                }

                if (this.FlexOptions.PickUpRoutes == null || this.FlexOptions.PickUpRoutes.Count == 0)
                {
                    return null;
                }

                _returnDOFlexRoute = this.FlexOptions.PickUpRoutes.OrderBy(r => r.Distance).First();

                _returnDOFlexRoute.StopTime = this.GetFlexTimeForDropOff(_returnDOFlexRoute, _returnPUFlexRoute, true);

                //if (_returnPUFlexRoute != null)
                //{

                //    if (_returnDOFlexRoute.Route.Name == _returnPUFlexRoute.Route.Name)
                //    {
                //        //on the same route

                //        if (_returnDOFlexRoute.Stop.Time > _returnPUFlexRoute.Stop.Time)
                //        {
                //            //we can do off on this loop
                //            _returnDOFlexRoute.StopTime = DateTime.Parse(_returnPUFlexRoute.StopTime.ToString("MM/dd/yyyy HH:") + _returnDOFlexRoute.Stop.Time.ToFixedLength(2, '0'));
                //        }
                //        else
                //        {
                //            //we have to wait until the next loop
                //            _returnDOFlexRoute.StopTime = DateTime.Parse(_returnPUFlexRoute.StopTime.AddHours(1).ToString("MM/dd/yyyy HH:") + _returnDOFlexRoute.Stop.Time.ToFixedLength(2, '0'));
                //        }
                //    }
                //    else
                //    {
                //        //add an hour to transfer
                //        _returnDOFlexRoute.StopTime = DateTime.Parse(_returnPUFlexRoute.StopTime.AddHours(1).ToString("MM/dd/yyyy HH:") + _returnDOFlexRoute.Stop.Time.ToFixedLength(2, '0'));
                //    }
                //}

                return _returnDOFlexRoute;
            }

            set
            {
                PropertyChangedEventHandler noChange = null;
                base.Setter(ref _returnDOFlexRoute, value, noChange, "ReturnDOFlexRoute", "ReturnDOFlexString");
            }

        }


        /// <summary>
        /// Gets or sets ReturnPUFlexRoute
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/27/2014  Created
        /// </history>
        [RuleIgnore]
        [XmlIgnore]
        public FlexRouteStopResult ReturnPUFlexRoute
        {
            get
            {
                if (!ShouldCalculateFlexRoute())
                {
                    return _returnPUFlexRoute;
                }
                if (_returnPUFlexRoute != null && this.ReturnPUFlexSetManually)
                {
                    return _returnPUFlexRoute;
                }

                if (this.ReturnType == "One-Way: No return" || this.ReturnType == "Roundtrip: Call for return")
                {
                    return null;
                }

                if (this.ReturnTime == DateTime.MinValue)
                {
                    return null;
                }

                if (!this.DropOffLocation.IsGeocoded)
                {
                    return null;
                }



                if (this.FlexOptions.DropOffRoutes == null || this.FlexOptions.DropOffRoutes.Count == 0)
                {
                    return null;
                }

                _returnPUFlexRoute = this.FlexOptions.DropOffRoutes.OrderBy(r => r.Distance).First();

                _returnPUFlexRoute.StopTime = this.GetFlexTimeForPickUp(_returnPUFlexRoute, _returnDOFlexRoute, true);

                return _returnPUFlexRoute;
            }

            set
            {
                PropertyChangedEventHandler noChange = null;
                base.Setter(ref _returnPUFlexRoute, value, noChange, "ReturnPUFlexRoute", "ReturnPUFlexString");
            }

        }



        /// <summary>
        /// Gets or sets OutgoingDOFlexRoute
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/27/2014  Created
        /// </history>
        [RuleIgnore]
        [XmlIgnore]
        public FlexRouteStopResult OutgoingDOFlexRoute
        {
            get
            {

                if (!ShouldCalculateFlexRoute())
                {
                    return _outgoingDOFlexRoute;
                }

                if (_outgoingDOFlexRoute != null && this.OutgoingDOFlexSetManually)
                {
                    this.DropOffTime = _outgoingDOFlexRoute.StopTime;
                    return _outgoingDOFlexRoute;
                }

                if (this.TripTime.IsMinValue())
                {
                    return null;
                }
                if (!this.DropOffLocation.IsGeocoded)
                {
                    return null;
                }
                if (this.FlexOptions.DropOffRoutes == null || this.FlexOptions.DropOffRoutes.Count == 0)
                {
                    return null;
                }
                _outgoingDOFlexRoute = this.FlexOptions.DropOffRoutes.OrderBy(r => r.Distance).First();

                //try to calculate the time
                _outgoingDOFlexRoute.StopTime = this.GetFlexTimeForDropOff(_outgoingDOFlexRoute, _outgoingPUFlexRoute, false);
                this.DropOffTime = _outgoingDOFlexRoute.StopTime;

                return _outgoingDOFlexRoute;
            }

            set
            {
                PropertyChangedEventHandler noChange = null;
                base.Setter(ref _outgoingDOFlexRoute, value, noChange, "OutgoingDOFlexRoute", "OutgoingDOFlexString", "ReturnPUFlexString");
                if (_outgoingDOFlexRoute != null)
                {
                    this.DropOffTime = _outgoingDOFlexRoute.StopTime;
                }

            }

        }


        /// <summary>
        /// Gets or sets OutgoingPUFlexRoute
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/27/2014  Created
        /// </history>
        [RuleIgnore]
        [XmlIgnore]
        public FlexRouteStopResult OutgoingPUFlexRoute
        {
            get
            {

                if (!ShouldCalculateFlexRoute())
                {
                    return _outgoingPUFlexRoute;
                }
                if (_outgoingPUFlexRoute != null && this.OutgoingPUFlexSetManually)
                {
                    this.PickUpTime = _outgoingPUFlexRoute.StopTime;
                    return _outgoingPUFlexRoute;
                }

                if (this.TripTime.IsMinValue())
                {
                    return null;
                }
                if (!this.PickUpLocation.IsGeocoded)
                {
                    return null;
                }
                if (this.FlexOptions.PickUpRoutes == null || this.FlexOptions.PickUpRoutes.Count == 0)
                {
                    return null;
                }

                _outgoingPUFlexRoute = this.FlexOptions.PickUpRoutes.OrderBy(r => r.Distance).First();
                _outgoingPUFlexRoute.StopTime = this.GetFlexTimeForPickUp(_outgoingPUFlexRoute, _outgoingDOFlexRoute, false);
                this.PickUpTime = _outgoingPUFlexRoute.StopTime;
                return _outgoingPUFlexRoute;
            }

            set
            {
                PropertyChangedEventHandler noChange = null;
                base.Setter(ref _outgoingPUFlexRoute, value, noChange, "OutgoingPUFlexRoute", "OutgoingPUFlexString", "ReturnDOFlexString");
                if (_outgoingPUFlexRoute != null)
                {
                    this.PickUpTime = _outgoingPUFlexRoute.StopTime;
                }

            }

        }
        #endregion

        #endregion

        #region --Properties--
        public bool IsTrip
        {
            get
            {
                if (this is Trip || this is TripHistory)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// Gets or sets Downloaded
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 11/12/2013  Created
        /// </history>
        public TripStreamEvent Downloaded
        {
            get { return _downloaded ?? (_downloaded = GetNewEntity<TripStreamEvent>(Downloaded_PropertyChanged)); }

            set
            {
                base.Setter(ref _downloaded, value, Downloaded_PropertyChanged, "Downloaded", "BackgroundColor");
            }

        }

        /// <summary>
        /// Gets or sets Signature
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/25/2013  Created
        /// </history>
        public TripStreamEvent Signature
        {
            get { return _signature ?? (_signature = GetNewEntity<TripStreamEvent>(Signature_PropertyChanged)); }

            set
            {
                base.Setter(ref _signature, value, Signature_PropertyChanged, "Signature");
            }

        }


        /// <summary>
        /// Gets or sets DropOffPerform
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/25/2013  Created
        /// </history>
        public TripStreamEvent DropOffPerform
        {
            get { return _dropOffPerform ?? (_dropOffPerform = GetNewEntity<TripStreamEvent>(DropOffPerform_PropertyChanged)); }

            set
            {
                base.Setter(ref _dropOffPerform, value, DropOffPerform_PropertyChanged, "DropOffPerform");
            }

        }


        /// <summary>
        /// Gets or sets DropOffArrive
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 01/25/2013  Created
        /// </history>
        public TripStreamEvent DropOffArrive
        {
            get { return _dropOffArrive ?? (_dropOffArrive = GetNewEntity<TripStreamEvent>(DropOffArrive_PropertyChanged)); }

            set
            {
                base.Setter(ref _dropOffArrive, value, DropOffArrive_PropertyChanged, "DropOffArrive");
            }

        }

        /// <summary>
        /// Gets or sets PickUpPerform
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/25/2013  Created
        /// </history>
        public TripStreamEvent PickUpPerform
        {
            get { return _pickUpPerform ?? (_pickUpPerform = GetNewEntity<TripStreamEvent>(PickUpPerform_PropertyChanged)); }

            set
            {
                base.Setter(ref _pickUpPerform, value, PickUpPerform_PropertyChanged, "PickUpPerform");
            }

        }
        /// <summary>
        /// Gets or sets PickUpArrive
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/25/2013  Created
        /// </history>
        public TripStreamEvent PickUpArrive
        {
            get { return _pickUpArrive ?? (_pickUpArrive = GetNewEntity<TripStreamEvent>(PickUpArrive_PropertyChanged)); }

            set
            {
                base.Setter(ref _pickUpArrive, value, PickUpArrive_PropertyChanged, "PickUpArrive");
            }

        }




        public string OldGPSID { get; set; }

        /// <summary>
        /// Gets or sets ManuallySetFees
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 01/24/2012  Created
        /// </history>
        public bool ManuallySetFees
        {
            get { return _manuallySetFees; }

            set
            {
                base.Setter(ref _manuallySetFees, value, FN.ManuallySetFees);
            }

        }

        /// <summary>
        /// Gets or sets ManuallySetDuration
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 11/24/2011  Created
        /// </history>
        public bool ManuallySetDuration
        {
            get { return _manuallySetDuration; }

            set
            {
                base.Setter(ref _manuallySetDuration, value, FN.ManuallySetDuration, FN.UICalculateDriveTimeText);
            }

        }

        public string UICalculateDriveTimeText
        {
            get
            {
                string rv = "Calculate drivetime and distance";
                if (ManuallySetDuration)
                {
                    rv += "*";
                }
                return rv;
            }
        }


        public string ClientProgramID
        {
            get
            {
                if (this.TripProgram == null || string.IsNullOrWhiteSpace(this.TripProgram.ProgramName))
                {
                    return string.Empty;
                }
                if (this.Client.ClientProgramSet == null)
                {
                    return string.Empty;
                }
                return this.Client.ClientProgramSet.ProgramID(this.TripProgram.ProgramName);
            }
        }

        public string TripStatus
        {
            get
            {
                if (this.Client.ClientID == 0)
                {
                    return "";
                }
                if (this.Cancelled)
                {
                    return "Cancelled";
                }
                if (this.NoGo)
                {
                    return "No Show";
                }
                if (this.TripDateTime > DateTime.Now)
                {
                    if (this.FleetManager.ID == 0)
                    {
                        return "Unscheduled";
                    }
                    return "Scheduled";
                }
                if (this.FleetManager.ID == 0)
                {
                    return "Not executed";
                }
                return "Completed";
            }
        }

        /// <summary>
        /// Gets or sets TripBilling
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 10/11/2011  Created
        /// </history>
        public TripBilling TripBilling
        {
            get { return _tripBilling ?? (_tripBilling = GetNewTripBilling()); }

            set
            {
                base.Setter(ref _tripBilling, value, TripBilling_PropertyChanged, FN.TripBilling);
            }

        }

        /// <summary>
        /// Gets or sets DropOffOdometer
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/15/2011  Created
        /// </history>
        public double DropOffOdometer
        {
            get { return Math.Round(_dropOffOdometer.GetValueOrDefault(), 1); }

            set
            {
                base.Setter(ref _dropOffOdometer, value, FN.DropOffOdometer);
                OnPropertyChanged("CalculatedActualDistanceOfTrip");
                OnPropertyChanged("CalculatedMPHOfTrip");
                OnPropertyChanged("CalculatedActualsToString");
                OnPropertyChanged("FleetManager.Statistics.Deadhead");
                OnPropertyChanged("FleetManager.Statistics.Revenue");
                OnPropertyChanged("FleetManager.Statistics.Service");
                base.OnPropertyChanged("BackgroundColor");
            }

        }


        /// <summary>
        /// Gets or sets PickUpOdometer
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 06/15/2011  Created
        /// </history>
        public double PickUpOdometer
        {
            get { return Math.Round(_pickUpOdometer.GetValueOrDefault(), 1); }

            set
            {
                base.Setter(ref _pickUpOdometer, value, FN.PickUpOdometer);
                OnPropertyChanged("CalculatedActualDistanceOfTrip");
                OnPropertyChanged("CalculatedMPHOfTrip");
                OnPropertyChanged("CalculatedActualsToString");
                OnPropertyChanged("FleetManager.Statistics.Deadhead");
                OnPropertyChanged("FleetManager.Statistics.Revenue");
                OnPropertyChanged("FleetManager.Statistics.Service");
            }

        }

        public System.Windows.Media.Color ValidColor
        {
            get
            {
                if (this.IsValid)
                {
                    return System.Windows.Media.Colors.Transparent;
                }
                return System.Windows.Media.Colors.IndianRed;
            }
        }

        public System.Windows.Media.Color BackgroundColor
        {
            get
            {
                if (this.NoGo)
                {
                    return FileSpecsManager.Instance.DispatchViewNoShowColor;
                }
                if (this.Cancelled)
                {
                    if (this.CancelBackgroundColor != System.Windows.Media.Colors.Transparent)
                    {
                        return this.CancelBackgroundColor;
                    }
                    return FileSpecsManager.Instance.DispatchViewCancelledColor;
                }

                if (this.TripDate.Date >= DateTime.Now.Date)
                {
                    //assign program color if it is in the future
                    if (this.TripProgram.BackgroundColor != System.Windows.Media.Colors.Transparent)
                    {
                        return this.TripProgram.BackgroundColor;
                    }
                }

                if (!this.DropOffStamp.IsMinValue())
                {
                    return FileSpecsManager.Instance.DispatchViewDroppedOffColor;
                }
                if (!this.PickUpStamp.IsMinValue())
                {
                    return FileSpecsManager.Instance.DispatchViewPickedUpColor;
                }
                if (this.Downloaded != null && !this.Downloaded.Timestamp.IsMinValue() && !this.FleetManagerIsNullOrUnassigned)
                {
                    return FileSpecsManager.Instance.DispatchViewDriverDownloadedColor;
                }
                if (!this.DummyFields.MT_D_1.IsMinValue())
                {
                    return FileSpecsManager.Instance.DispatchViewDispatchedColor;
                }
                if (this.IsScheduled)
                {
                    return FileSpecsManager.Instance.DispatchViewScheduledColor;
                }
                if (!this.IsScheduled)
                {
                    return FileSpecsManager.Instance.DispatchViewUnscheduledColor;
                }
                return System.Windows.Media.Colors.Transparent;
            }
        }

        /// <summary>
        /// If a trip isn't cancelled or no-showed
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   04/05/2010  Created
        /// </history>
        public bool IsActive
        {
            get
            {
                return !Cancelled && !NoGo;
            }
        }

        public double CalculatedMinutesOfTrip
        {
            get
            {
                var rv = (this.DropOffStamp - this.PickUpStamp).TotalMinutes;
                if (rv < 0)
                {
                    return 0;
                }
                return rv;
            }
        }

        public double CalculatedActualDistanceOfTrip
        {
            get
            {
                var rv = this.DropOffOdometer - this.PickUpOdometer;
                if (rv < 0)
                {
                    return 0;
                }
                return rv;
            }
        }

        public double CalculatedMPHOfTrip
        {
            get
            {
                var rv = 0D;
                if (CalculatedMinutesOfTrip > 0 && CalculatedActualDistanceOfTrip > 0)
                {
                    rv = (this.CalculatedActualDistanceOfTrip / this.CalculatedMinutesOfTrip) * 60;
                }
                return rv;
            }
        }

        public string CalculatedActualsToString
        {
            get
            {
                var sb = new StringBuilder();
                if (this.CalculatedActualDistanceOfTrip > 0)
                {
                    sb.Append("Distance: ");
                    sb.Append(Math.Round(this.CalculatedActualDistanceOfTrip, 1).ToString());
                    sb.Append(" miles ");

                }
                if (this.CalculatedMinutesOfTrip > 0)
                {
                    sb.Append("Duration: ");
                    sb.Append(Math.Round(this.CalculatedMinutesOfTrip, 1).ToString());
                    sb.Append(" minutes ");
                }

                if (this.CalculatedMPHOfTrip > 0)
                {
                    sb.Append(Math.Round(this.CalculatedMPHOfTrip, 1).ToString());
                    sb.Append(" MPH");
                }

                return sb.ToString();
            }
        }

        ///// <summary>
        ///// The return trip (if applicable)
        ///// </summary>
        ///// <history>
        /////     [Tim Hibbard]   05/20/2009  Created
        ///// </history>
        //[ReportAttribute]
        //[RuleIgnore]
        //public TripBase ReturnTrip
        //{
        //    get
        //    {
        //        if (_returnTrip == null)
        //        {
        //            if (this.TripType.ToUpper() == "A2" || this.TripType.ToUpper() == "S2")
        //            {
        //                _returnTrip = new TripBaseManager().GetReturnTrip(this) as TripBase;
        //            }
        //            else
        //            {
        //                _returnTrip = new Trip();
        //            }
        //        }
        //        return _returnTrip;
        //    }
        //}

        /// <summary>
        /// The return trip (if applicable)
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   05/20/2009  Created
        /// </history>
        [RuleIgnore]
        public TripBase ReturnTrip
        {
            get
            {
                if (_returnTrip == null)
                {
                    if (this.TripType.ToUpper() == "A2" || this.TripType.ToUpper() == "S2")
                    {
                        _returnTrip = new TripBaseManager().GetReturnTrip(this);
                    }
                    else
                    {
                        _returnTrip = new Trip();
                    }
                }
                return _returnTrip;
            }
        }


        [RuleIgnore]
        [XmlIgnore]
        public TripBase OutgoingTrip
        {
            get
            {
                if (_outgoingTrip != null)
                {
                    return _outgoingTrip;
                }
                if (this.TripType.ToUpper() != "AR"
                    && this.TripType.ToUpper() != "SR"
                    && this.TripType.ToUpper() != "RT")
                {
                    return new Trip();
                }
                var legs = new TripBaseManager().GetAssociatedLegs(this);

                foreach (var t in legs)
                {
                    if (t.TripType == "A2"
                    || t.TripType == "S2"
                    || t.TripType == "AC"
                    || t.TripType == "SC")
                    {
                        _outgoingTrip = t;
                        return _outgoingTrip;
                    }
                }

                _outgoingTrip = new Trip();
                return _outgoingTrip;
            }
        }

        [RuleIgnore]
        public MDTTripStatus MDTTripStatus
        {
            get
            {
                //if (_mdtTripStatus == null)
                //{
                //    if (this.TripID != 0)
                //    {
                //        this._mdtTripStatus = (MDTTripStatus)new Managers.MDTTripStatusManager().GetByTripID(this.TripID);
                //    }
                //    if (_mdtTripStatus == null)
                //    {
                //        this._mdtTripStatus = new MDTTripStatus(this);
                //    }
                //}
                var rv = new Managers.MDTTripStatusManager().GetByTripID(this.TripID);
                if (rv == null)
                {
                    rv = new MDTTripStatus(this);
                }
                return rv;
            }
        }

        [RuleIgnore]
        [XmlIgnore]
        public Schedule AssignedSchedule
        {
            get
            {
                return _schedule;
            }
            set
            {
                this._schedule = value;
            }
        }

        [ReportAttribute]
        [RuleIgnore]
        public FleetManager ReturnFleetManager
        {
            get
            {
                var rt = this.ReturnTrip;
                if (rt != null)
                {
                    return rt.FleetManager;
                }
                else
                {
                    return new FleetManager();
                }
            }
        }

        /// <summary>
        /// If this trip is a one way
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   11/13/2008  Created
        /// </history>
        [ReportAttribute]
        public bool IsOneWay
        {
            get
            {
                return this.TripType.ToUpper() == "A1" || this.TripType.ToUpper() == "S1" || this.TripType.ToUpper() == "FI";
            }
        }

        public bool IsElibleForNextLeg
        {
            get
            {
                if (this.TripType.ToUpper() == "FI")
                {
                    return true;
                }
                if (this is Trip || this is TripHistory)
                {
                    return false;
                }
                if (this.TripType.ToUpper() == "A1" || this.TripType.ToUpper() == "S1")
                {
                    return true;
                }
                return false;
            }
        }

        [ReportAttribute]
        public bool IsOutgoingLeg
        {
            get
            {
                return this.TripType.ToUpper() == "A1"
                    || this.TripType.ToUpper() == "A2"
                    || this.TripType.ToUpper() == "AC"
                    || this.TripType.ToUpper() == "S1"
                    || this.TripType.ToUpper() == "S2"
                    || this.TripType.ToUpper() == "SC"
                    || this.TripType.ToUpper() == "FI";
            }
        }


        /// <summary>
        /// If this trip will generate a callback trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   11/13/2008  Created
        /// </history>
        [ReportAttribute]
        public bool WillGenerateCallback
        {
            get
            {
                return this.TripType.ToUpper() == "AC" || this.TripType.ToUpper() == "SC";
            }
        }

        ///<summary>
        /// If this trip is a fill in
        ///</summary>
        ///<history>
        ///		[Tim Hibbard]	11/03/2008	Created
        ///</history> 
        [XmlIgnore]
        [RuleIgnore]
        public bool IsFillIn
        {
            get
            {
                return this.TripType == "FI";
            }
        }

        /// <summary>
        /// Returns if this reservation is a subscription
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   08/14/2008  Created
        ///     [Tim Hibbard]   09/03/2008  Moved to TripBase
        /// </history>
        public bool IsSubscription
        {
            get
            {
                if (this.SubscriptionInformation.WMonday)
                {
                    return true;
                }
                if (this.SubscriptionInformation.WTuesday)
                {
                    return true;
                }
                if (this.SubscriptionInformation.WWednesday)
                {
                    return true;
                }
                if (this.SubscriptionInformation.WThursday)
                {
                    return true;
                }
                if (this.SubscriptionInformation.WFriday)
                {
                    return true;
                }
                if (this.SubscriptionInformation.WSaturday)
                {
                    return true;
                }
                if (this.SubscriptionInformation.WSunday)
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(this.SubscriptionInformation.MultiWeeklyDay))
                {
                    return true;
                }
                if (this.SubscriptionInformation.MultiWeeklyInterval != 0)
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(this.SubscriptionInformation.MWeek))
                {
                    return true;
                }
                if (!string.IsNullOrEmpty(this.SubscriptionInformation.MonthDay))
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// returns information about the city of this trip.  For binding purposes only
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   05/12/2008  Created
        /// </history>
        [ContainsAttribute, Report]
        public string City
        {
            get
            {
                var rv = "";
                if (!string.IsNullOrEmpty(this.DropOffLocation.City) && !string.IsNullOrEmpty(this.PickUpLocation.City))
                {
                    if (this.DropOffLocation.City.ToUpper() == this.PickUpLocation.City.ToUpper())
                    {
                        rv = this.PickUpLocation.City;
                    }
                    else
                    {
                        rv = this.PickUpLocation.City + " | " + this.DropOffLocation.City;
                    }
                }
                return rv;
            }
        }

        /// <summary>
        /// Gets the physical stops for this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   04/11/2008  Commented
        /// </history>
        [RuleIgnore]
        [XmlIgnore]
        public List<Stop> Stops
        {
            get
            {
                //if (_stops != null)
                //{
                //    _stops.ForEach(s =>
                //        {
                //            s.Trip = null;
                //            s.PropertyChanged -= stop_PropertyChanged;
                //        });
                //}
                //_stops = null;

                if (_isCalculatingTripTimes)
                {
                    return new List<Stop>();
                }

                if (_stops == null)
                {
                    _stops = new List<Stop>();
                    var pickUp = new Stop(this, this.PickUpTime, this.PickUpLocation, StopActionType.PickUp);
                    var dropOff = new Stop(this, this.DropOffTime, this.DropOffLocation, StopActionType.DropOff);
                    pickUp.PropertyChanged += stop_PropertyChanged;
                    dropOff.PropertyChanged += stop_PropertyChanged;

                    _stops.Add(pickUp);
                    _stops.Add(dropOff);
                }
                return _stops;
            }
        }

        protected internal void ClearStops()
        {
            if (this._stops != null)
            {
                _stops.ForEach(s =>
                {
                    s.Trip = null;
                    s.PropertyChanged -= stop_PropertyChanged;

                });
                _stops = null;
            }
        }

        /// <summary>
        /// Provides list of available routes for this trip
        /// </summary>
        /// <returns></returns>
        /// <history>
        ///     [Tim Hibbard]   04/15/2008  Created
        ///     [Tim Hibbard]   06/19/2008  Changed to consider tripdate and time and need for wheelchair
        ///     [Tim Hibbard]   06/19/2008  Changed to return FleetManager
        /// </history>
        [XmlIgnore]
        [RuleIgnore]
        public List<FleetManager> AvailableRoutes
        {
            get
            {
                if (_availableRoutes == null)
                {
                    _availableRoutes = new TripBaseManager().GetAvailableRoutes(this).OrderBy(r => r.Route.Name).ToList();
                }
                return _availableRoutes;
            }
        }

        /// <summary>
        /// What the request time is (p or d)
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/08/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public string TimeType
        {
            get
            {
                return _timeType ?? string.Empty;
            }
            set
            {
                if (base.IsDifferent(_timeType, value))
                {
                    _timeType = value;
                    OnPropertyChanged("TimeType");
                    OnPropertyChanged("TimeTypeLong");
                    OnPropertyChanged("FlexOptions");
                    OnPropertyChanged("OutgoingPUFlexString");
                    OnPropertyChanged("OutgoingDOFlexString");
                    OnPropertyChanged("ReturnPUFlexString");
                    OnPropertyChanged("ReturnDOFlexString");
                    this.CalculatePUDOTimeAndWindows();
                }
            }
        }

        public string TimeTypeLong
        {
            get
            {
                if (TimeType == "P")
                {
                    return "Pick up";
                }
                return "Drop off";
            }
        }

        ///<summary>
        /// --insert comment--
        ///</summary>
        ///<history>
        ///     [Tim Hibbard]     11/26/2008     Created
        ///</history>     
        [XmlAttribute]
        [DataMember]
        public int FleetManagerID
        {
            get
            {
                return _fleetManagerID ?? 0;
            }

            set
            {
                if (base.IsDifferent(_fleetManagerID, value))
                {
                    _fleetManagerID = value;
                    if (_fleetManager == null)
                    {
                        _fleetManager = GetNewFleetManager();
                    }
                    this._fleetManager._isPopulated = false;
                }
            }
        }

        public bool FleetManagerIsNullOrUnassigned
        {
            get
            {
                if (_fleetManagerID.GetValueOrDefault() != 0)
                {
                    return false;
                }
                if (_fleetManager == null)
                {
                    return true;
                }
                else
                {
                    return string.IsNullOrWhiteSpace(_fleetManager.Route.Name);
                    //return _fleetManager.Equals(new FleetManager());
                }
            }
        }

        /// <summary>
        /// The fleetmanager associated with this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/07/2007  Changed to FleetManager object
        ///     [Tim Hibbard]   08/13/2007  Modified setter to re-subscribe to events
        /// </history>
        [ReportAttribute]
        [ContainsAttribute]
        [RuleIgnore]
        public FleetManager FleetManager
        {
            get
            {
                if (_fleetManager == null)
                {
                    _fleetManager = GetNewFleetManager();
                }
                if (this.AssignedSchedule != null && this.FleetManagerID != 0 && this.AssignedSchedule.FleetManagersAreInitialized)
                {
                    FleetManager tmp = this.AssignedSchedule.FleetManagers.FirstOrDefault(f => f.ID == this.FleetManagerID);
                    if (tmp != null)
                    {
                        _fleetManager = tmp;
                        _fleetManager._isPopulated = true;
                    }
                }
                if (!_fleetManager._isPopulated && !this.IsBuildingGet())
                {
                    var hubStatus = Hub.RaisePropertyChangedEvents;
                    if (hubStatus)
                    {
                        Hub.RaisePropertyChangedEvents = false;
                    }
                    _fleetManager.ID = this.FleetManagerID;
                    _fleetManager.Populate();
                    _fleetManager._isPopulated = true;
                    if (hubStatus)
                    {
                        Hub.RaisePropertyChangedEvents = true;
                    }
                }
                return _fleetManager;
            }
            set
            {
                if (!(_fleetManager ?? (_fleetManager = GetNewFleetManager())).Equals(value))
                {
                    if (value == null)
                    {
                        value = new FleetManager();
                    }
                    Logging.Logger.Log(Hub.scMouse);
                    string uniqueID = "";
                    string oldGPSID = _fleetManager.Vehicle.GPSUnitID;
                    int oldID = 0;
                    Schedule schedule = null;
                    if (!_fleetManager.Equals(new FleetManager()))
                    {
                        uniqueID = _fleetManager.UniqueID;
                        oldID = _fleetManager.ID;
                        schedule = _fleetManager.Schedule;
                    }

                    //try to get it out of the fleetmanager
                    if (schedule == null)
                    {
                        schedule = value.Schedule;
                    }

                    //try to get it out of the trip
                    if (schedule == null)
                    {
                        schedule = this.AssignedSchedule;
                    }

                    var hubStatus = Hub.RaisePropertyChangedEvents;
                    if (hubStatus)
                    {
                        Hub.RaisePropertyChangedEvents = false;
                    }

                    //sent went status
                    if (!FleetManager.IsNullOrEmpty(value))
                    {
                        this._went = true;
                    }
                    else
                    {
                        this._went = false;
                    }

                    _fleetManager.PropertyChanged -= FleetManager_PropertyChanged;
                    var fm = value;
                    //try to pull fleetmanager out of schedule
                    if (schedule != null)
                    {
                        var tmp = schedule.FleetManagers.FirstOrDefault(inner => inner.UniqueID == value.UniqueID || (inner.ID == value.ID && value.ID != 0));
                        if (tmp != null)
                        {
                            fm = tmp;
                        }
                    }

                    _fleetManager = fm;
                    this.FleetManagerID = fm.ID;


                    if (hubStatus)
                    {
                        Hub.RaisePropertyChangedEvents = true;
                    }

                    //_route = value.Route;
                    //if it is a new fleetmanager, then we are just unscheduling it
                    //so remove dispatch time
                    if (value.Equals(new FleetManager()))
                    {
                        this.Dispatch(DateTime.MinValue);
                    }
                    //otherwise set the dispatch time to now
                    else
                    {
                        //unless they don't want that
                        if (FileSpecsManager.Instance.AutoDispatch && this.TripDate.Date == DateTime.Now.Date)
                        {
                            this.Dispatch();
                        }
                    }

                    if (schedule == null)
                    {
                        schedule = _fleetManager.Schedule;
                    }

                    this.UpdateOldAssignedTrips(schedule, uniqueID, oldID);
                    //try
                    //{
                    //    Application.DoEvents();
                    //}
                    //catch (Exception ex)
                    //{

                    //    throw ex;
                    //}


                    _fleetManager.PropertyChanged += FleetManager_PropertyChanged;
                    _fleetManager._isPopulated = true;
                    OnPropertyChanged("FleetManager");
                    OnPropertyChanged("BackgroundColor");
                    OnPropertyChanged("IsValid");
                    OnPropertyChanged("ValidColor");
                    OnPropertyChanged("IsScheduled");
                    #region Raise assignedtrips prop on new fleetmanager - refresh SC
                    //retain existing dirty status.  avoid trip to db
                    var wasDirty = _fleetManager.IsDirty;
                    _fleetManager.ResetAssignedTrips();
                    _fleetManager.OnPropertyChanged(FN.AssignedTrips);
                    _fleetManager.OnPropertyChanged("AssignedStops");
                    _fleetManager.IsDirty = wasDirty;
                    #endregion

                    OnPropertyChanged("DummyFields.MT_D_1");
                    //a change to get the expected version number
                }
            }
        }

        /// <summary>
        /// See if we can find the old fleetmanager and raise an event on it
        /// that will cause the scheduling canvas to refresh quickly
        /// </summary>
        /// <param name="fm">New fleetmanager</param>
        /// <param name="uniqueID">unique key of old fm</param>
        /// <param name="oldID">database id of old fm</param>
        private void UpdateOldAssignedTrips(Schedule schedule, string uniqueID, int oldID)
        {
            if (schedule == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(uniqueID) && oldID == 0)
            {
                return;
            }

            var o = schedule.FleetManagers.FirstOrDefault(innerFM => innerFM.UniqueID == uniqueID || (innerFM.ID == oldID && oldID != 0));
            if (o != null)
            {
                //retain existing dirty status.  save a db transaction
                var oldOdirtyStatus = o.IsDirty;
                o.ResetAssignedTrips();
                o.OnPropertyChanged(FN.AssignedTrips);
                o.OnPropertyChanged("AssignedStops");
                o.IsDirty = oldOdirtyStatus;
            }
        }

        /// <summary>
        /// Additional riders on this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   08/20/2007  Created
        /// </history>
        [Report]
        public Riders Riders
        {
            get
            {
                return _riders ?? (_riders = GetNewRiders());
            }
            set
            {
                base.Setter(ref _riders, value, Riders_PropertyChanged, FN.Riders, "ValidColor");
            }
        }

        public bool ClientOrTripHasPCA
        {
            get
            {
                return this.Riders.PCA || this.Client.SpecialNeeds.PersonalCareAttendant;
            }
        }

        /// <summary>
        /// When the client was actually dropped off
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime DropOffStamp
        {
            get
            {
                var rv = _dropOffStamp ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _dropOffStamp, value, FN.DropOffStamp,
                            FN.CalculatedActualsToString,
                            FN.CalculatedMinutesOfTrip,
                            FN.CalculatedMPHOfTrip,
                            FN.FleetManager_Statistics_Deadhead,
                            FN.FleetManager_Statistics_Revenue,
                            FN.FleetManager_Statistics_Service,
                            FN.BackgroundColor);
            }
        }

        /// <summary>
        /// When this client was actually picked up
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime PickUpStamp
        {
            get
            {
                var rv = _pickUpStamp ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _pickUpStamp, value, FN.PickUpStamp,
                            FN.CalculatedActualsToString,
                            FN.CalculatedMinutesOfTrip,
                            FN.CalculatedMPHOfTrip,
                            FN.FleetManager_Statistics_Deadhead,
                            FN.FleetManager_Statistics_Revenue,
                            FN.FleetManager_Statistics_Service,
                            FN.BackgroundColor);
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/04/2007  Changed to double
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public double Tickets
        {
            get
            {
                return _tickets ?? 0;
            }
            set
            {
                base.Setter(ref _tickets, value, FN.Tickets);
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/04/2007  Changed to double
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public double Donations
        {
            get
            {
                return _donations ?? 0;
            }
            set
            {
                base.Setter(ref _donations, value, FN.Donations);
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public decimal CheckAmount
        {
            get
            {
                return _checkAmount ?? 0M;
            }
            set
            {
                base.Setter(ref _checkAmount, value, FN.CheckAmount);
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public int CheckNumber
        {
            get
            {
                return _checkNumber ?? 0;
            }
            set
            {
                base.Setter(ref _checkNumber, value, FN.CheckNumber);
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public DateTime PayDate
        {
            get
            {
                return _payDate ?? DateTime.MinValue;
            }
            set
            {
                if (!(_payDate ?? DateTime.MinValue).Equals(value))
                {
                    _payDate = value;
                    OnPropertyChanged("PayDate");
                }
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public string Payee
        {
            get
            {
                return _payee ?? string.Empty;
            }
            set
            {
                if (!(_payee ?? string.Empty).Equals(value))
                {
                    _payee = value;
                    OnPropertyChanged("Payee");
                }
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public bool Paid
        {
            get
            {
                return _paid ?? false;
            }
            set
            {
                if (!(_paid ?? false).Equals(value))
                {
                    _paid = value;
                    OnPropertyChanged("Paid");
                }
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public decimal BillAmount
        {
            get
            {
                return _billAmount ?? 0M;
            }
            set
            {
                if (!(_billAmount ?? 0M).Equals(value))
                {
                    _billAmount = value;
                    OnPropertyChanged("BillAmount");
                }
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public string InvoiceID
        {
            get
            {
                return _invoiceID ?? string.Empty;
            }
            set
            {
                if (!(_invoiceID ?? string.Empty).Equals(value))
                {
                    _invoiceID = value;
                    OnPropertyChanged("InvoiceID");
                }
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public DateTime BillDate
        {
            get
            {
                return _billDate ?? DateTime.MinValue;
            }
            set
            {
                if (!(_billDate ?? DateTime.MinValue).Equals(value))
                {
                    _billDate = value;
                    OnPropertyChanged("BillDate");
                }
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public string BillTo
        {
            get
            {
                return _billTo ?? string.Empty;
            }
            set
            {
                if (!(_billTo ?? string.Empty).Equals(value))
                {
                    _billTo = value;
                    OnPropertyChanged("BillTo");
                }
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public bool Billed
        {
            get
            {
                return _billed ?? false;
            }
            set
            {
                if (!(_billed ?? false).Equals(value))
                {
                    _billed = value;
                    OnPropertyChanged("Billed");
                }
            }
        }

        /// <summary>
        /// Any dummy data
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/19/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   08/13/2007  Modified setter to resubscribe to events
        /// </history>
        [Report]
        [RuleIgnore]
        [DataMember]
        public DummyFields DummyFields
        {
            get
            {
                return _dummyFields ?? (_dummyFields = GetNewDummyFields());
            }
            set
            {
                base.Setter(ref _dummyFields, value, DummyFields_PropertyChanged, FN.DummyFields);
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public bool WP
        {
            get
            {
                return _wp ?? false;
            }
            set
            {
                if (!(_wp ?? false).Equals(value))
                {
                    _wp = value;
                    OnPropertyChanged("WP");
                }
            }
        }

        /// <summary>
        /// Kyle Edit.
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public bool AP
        {
            get
            {
                return _ap ?? false;
            }
            set
            {
                if (!(_ap ?? false).Equals(value))
                {
                    _ap = value;
                    OnPropertyChanged("AP");
                }
            }
        }

        /// <summary>
        /// Description of fare
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public string Cost4
        {
            get
            {
                return _cost4 ?? string.Empty;
            }
            set
            {
                if (!(_cost4 ?? string.Empty).Equals(value))
                {
                    _cost4 = value;
                    OnPropertyChanged("Cost4");
                }
            }
        }

        /// <summary>
        /// Dummy field.  Rarely used according to Kyle
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public decimal Cost3
        {
            get
            {
                return _cost3 ?? 0M;
            }
            set
            {
                if (!(_cost3 ?? 0M).Equals(value))
                {
                    _cost3 = value;
                    OnPropertyChanged("Cost3");
                }
            }
        }

        /// <summary>
        /// Fixed fee for this trip.  Comes from ClientPrograms table
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   12/21/2011  Redirected to TripBilling obj
        /// </history>
        //[Report]
        //[XmlAttribute]
        //[DataMember]
        //public decimal Cost2
        //{
        //    get
        //    {
        //        return TripBilling.ProgramBilled;
        //    }
        //}

        /// <summary>
        /// The co-pay that the client is required to pay.  Comes from ClientPrograms table
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   12/21/2011  Redirected to TripBilling obj
        /// </history>/
        //[Report]
        //[XmlAttribute]
        //[DataMember]
        //public decimal Cost1
        //{
        //    get
        //    {
        //        return TripBilling.ClientCoPay;
        //    }
        //}

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/04/2007  Changed to double
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   11/05/2007  Changed to Decimal
        ///     [Tim Hibbard]   11/05/2007  Changed to calculate drivetime if not already calculated
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public double TravelTime
        {
            get
            {
                return _travelTime ?? 0D;
            }
            set
            {
                base.Setter(ref _travelTime, value, () => this.CalculatePUDOTimeAndWindows(), FN.TravelTime);

            }
        }

        public double TravelHours
        {
            get
            {
                return TravelTime / 60;
            }
        }

        /// <summary>
        /// Kyle Edit.  Isn't this the same as .Miles?
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/04/2007  Changed to double
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   11/05/2007  Changed to Decimal
        ///     [Tim Hibbard]   11/05/2007  Changed to calculate drivetime if not already calculated
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public double TravelDistance
        {
            get
            {
                return _travelDistance ?? 0D;
            }
            set
            {

                base.Setter(ref _travelDistance, value, FN.TravelDistance, "TripBilling.BillableMiles");
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public int GroupID
        {
            get
            {
                return _groupID ?? 0;
            }
            set
            {
                if (!(_groupID ?? 0).Equals(value))
                {
                    _groupID = value;
                    OnPropertyChanged("GroupID");
                }
            }
        }

        /// <summary>
        /// Why the bastard didn't go, but wasn't polite enough to cancel
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public string NoGoReason
        {
            get
            {
                return _noGoReason ?? string.Empty;
            }
            set
            {
                if (!(_noGoReason ?? string.Empty).Equals(value))
                {
                    _noGoReason = value;
                    OnPropertyChanged("NoGoReason");
                }
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public int NoGoCode
        {
            get
            {
                return _noGoCode ?? 0;
            }
            set
            {
                if (!(_noGoCode ?? 0).Equals(value))
                {
                    _noGoCode = value;
                    OnPropertyChanged("NoGoCode");
                }
            }
        }

        /// <summary>
        /// If the client did not go on the trip, but did not cancel
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   03/01/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public bool NoGo
        {
            get
            {
                return _noGo ?? false;
            }
            set
            {
                if (!(_noGo ?? false).Equals(value))
                {
                    _noGo = value;
                    OnPropertyChanged("NoGo");
                    OnPropertyChanged("IsActive");
                    base.OnPropertyChanged("BackgroundColor");
                }
            }
        }

        /// <summary>
        /// When the trip was cancelled
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public DateTime CancelDate
        {
            get
            {
                return _cancelDate ?? DateTime.MinValue;
            }
            set
            {
                if (!(_cancelDate ?? DateTime.MinValue).Equals(value))
                {
                    _cancelDate = value;
                    OnPropertyChanged("CancelDate");
                }
            }
        }

        /// <summary>
        /// Why the trip was cancelled
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [ContainsAttribute, Report]
        [XmlAttribute]
        [DataMember]
        public string CancelReason
        {
            get
            {
                return _cancelReason ?? string.Empty;
            }
            set
            {
                if (!(_cancelReason ?? string.Empty).Equals(value))
                {
                    _cancelReason = value;
                    OnPropertyChanged("CancelReason");
                }
            }
        }

        private System.Windows.Media.Color _cancelBackgroundColor = System.Windows.Media.Colors.Transparent;

        /// <summary>
        /// Gets or sets CancelBackgroundColor
        /// </summary>
        /// <history>
        ///   [Tim Hibbard] 05/13/2015  Created
        /// </history>
        public System.Windows.Media.Color CancelBackgroundColor
        {
            get { return _cancelBackgroundColor; }

            set
            {
                base.Setter(ref _cancelBackgroundColor, value, "CancelBackgroundColor");
            }

        }


        /// <summary>
        /// stores the id of the cancelreason
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public int CancelCode
        {
            get
            {
                return _cancelCode ?? 0;
            }
            set
            {
                if (!(_cancelCode ?? 0).Equals(value))
                {
                    _cancelCode = value;
                    OnPropertyChanged("CancelCode");
                }
            }
        }

        /// <summary>
        /// If the trip was cancelled
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public bool Cancelled
        {
            get
            {
                return _cancelled ?? false;
            }
            set
            {
                if (!(_cancelled ?? false).Equals(value))
                {
                    _cancelled = value;
                    OnPropertyChanged("Cancelled");
                    OnPropertyChanged("IsActive");
                    base.OnPropertyChanged("BackgroundColor");
                }
            }
        }

        /// <summary>
        /// If the trip was completed
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public bool Went
        {
            get
            {
                return _went ?? false;
            }
            set
            {
                if (!(_went ?? false).Equals(value))
                {
                    _went = value;
                    OnPropertyChanged("Went");
                }
            }
        }

        /// <summary>
        /// If the trip was originally scheduled by the PRSE
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public bool ScheduledByAutoSchedule
        {
            get
            {
                return _scheduledByAutoSchedule ?? false;
            }
            set
            {
                if (!(_scheduledByAutoSchedule ?? false).Equals(value))
                {
                    _scheduledByAutoSchedule = value;
                    OnPropertyChanged("ScheduledByAutoSchedule");
                }
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public bool ExcludeFromAutoschedule
        {
            get
            {
                return _excludeFromAutoSchedule ?? false;
            }
            set
            {
                if (!(_excludeFromAutoSchedule ?? false).Equals(value))
                {
                    _excludeFromAutoSchedule = value;
                    OnPropertyChanged("ExcludeFromAutoschedule");
                }
            }
        }

        public void ClearTimeWindows()
        {
            this.TWa = DateTime.MinValue;
            this.TWb = DateTime.MinValue;
            this.TWc = DateTime.MinValue;
            this.TWd = DateTime.MinValue;
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        public DateTime TWa
        {
            get
            {
                var rv = _TWa ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _TWa, value, "TWa", "ValidColor");
                //if (!(_TWa ?? DateTime.MinValue).Equals(value))
                //{
                //    _TWa = value;
                //    OnPropertyChanged("TWa");
                //}
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        public DateTime TWb
        {
            get
            {
                var rv = _TWb ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _TWb, value, "TWb", "ValidColor");
                //if (!(_TWb ?? DateTime.MinValue).Equals(value))
                //{
                //    _TWb = value;
                //    OnPropertyChanged("TWb");
                //}
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        public DateTime TWc
        {
            get
            {
                var rv = _TWc ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _TWc, value, "TWc", "ValidColor");
                //if (!(_TWc ?? DateTime.MinValue).Equals(value))
                //{
                //    _TWc = value;
                //    OnPropertyChanged("TWc");
                //}
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        public DateTime TWd
        {
            get
            {
                var rv = _TWd ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _TWd, value, "TWd", "ValidColor");
                //if (!(_TWd ?? DateTime.MinValue).Equals(value))
                //{
                //    _TWd = value;
                //    OnPropertyChanged("TWd");
                //}
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public string ToDrop
        {
            get
            {
                //return _toDrop ?? string.Empty; 
                if (string.IsNullOrEmpty(_toDrop))
                {
                    return "0";
                }
                else
                {
                    return _toDrop;
                }
            }
            set
            {
                if (!(_toDrop ?? string.Empty).Equals(value))
                {
                    _toDrop = value;
                    OnPropertyChanged("ToDrop");
                }
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [XmlAttribute]
        [DataMember]
        public string ToPick
        {
            get
            {
                //return _toPick ?? string.Empty; 
                if (string.IsNullOrEmpty(_toPick))
                {
                    return "0";
                }
                else
                {
                    return _toPick;
                }
            }
            set
            {
                if (!(_toPick ?? string.Empty).Equals(value))
                {
                    _toPick = value;
                    OnPropertyChanged("ToPick");
                }
            }
        }

        /// <summary>
        /// Any additional information about this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [ContainsAttribute, Report]
        [XmlAttribute]
        [DataMember]
        public string TripComment
        {
            get
            {
                var rv = _tripComment ?? string.Empty;
                return rv.Replace(Environment.NewLine, " ");
            }
            set
            {
                if (!(_tripComment ?? string.Empty).Equals(value))
                {
                    _tripComment = value;
                    OnPropertyChanged("TripComment");
                }
            }
        }

        /// <summary>
        /// When this trip was scheduled to be dropped off
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/20/2008  Changed to check equality before setting
        ///     [Tim Hibbard]   06/20/2008  Changed to set availableroutes to null
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime DropOffTime
        {
            get
            {
                var rv = _dropOffTime ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _dropOffTime, value,
                    () => _availableRoutes = null,
                    "DropOffTime", "AvailableRoutes", "IsValid", "ValidColor");
            }
        }

        /// <summary>
        /// When this trip was scheduled to be picked up
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/20/2008  Changed to check equality before setting
        ///     [Tim Hibbard]   06/20/2008  Changed to set availableroutes to null
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime PickUpTime
        {
            get
            {
                var rv = _pickUpTime ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                base.Setter(ref _pickUpTime, value, () =>
                {
                    _availableRoutes = null;
                }, "PickUpTime", "AvailableRoutes", "IsValid", "ValidColor");
            }
        }

        public DateTime PickUpDateAndTime
        {
            get
            {
                //return DateTime.ParseExact(
                //    this.PickUpTime.ToString("hh:mm") + this.TripDate.ToString("MM/dd/yyyy"), 
                //    "hh:mmMM/dd/yyyy",
                //    null);
                return DateTime.Parse(this.TripDate.ToShortDateString() + " " + this.PickUpTime.ToShortTimeString());

            }
        }

        public DateTime DropOffDateAndTime
        {
            get
            {
                //return DateTime.ParseExact(
                //    this.DropOffTime.ToString("hh:mm") + this.TripDate.ToString("MM/dd/yyyy"), 
                //    "hh:mmMM/dd/yyyy",
                //    null);
                return DateTime.Parse(this.TripDate.ToShortDateString() + " " + this.DropOffTime.ToShortTimeString());
            }
        }

        public TimeSpan TimeslotSortTime
        {
            get
            {
                if (this.PickUpTime.IsMinValue())
                {
                    return TimeSpan.MaxValue;
                }
                return this.PickUpTime.TimeOfDay;
            }
        }

        public string Timeslot
        {
            get
            {
                if (this.PickUpTime.IsMinValue())
                {
                    return "No Assigned Time";
                }
                return string.Format("{0} - {1}", this.PickUpTime.ToString("h tt"), this.PickUpTime.AddHours(1).ToString("h tt"));
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   11/21/2008  Changed to ClientProgram type
        /// </history>
        [ContainsAttribute]
        [ReportAttribute]
        [RuleIgnore]
        [DataMember]
        public ClientProgram TripProgram
        {
            get
            {
                return _tripProgram ?? (_tripProgram = GetNewTripProgram());
            }
            set
            {
                base.Setter(ref _tripProgram, value, _tripProgram_PropertyChanged, () => this.CalculateZoneFee(), FN.TripProgram, "IsValid", "ValidColor");
            }
        }

        /// <summary>
        /// The programs that this client is assocated with.  Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [ContainsAttribute]
        [XmlAttribute]
        //[DataMember]
        public string ClientPrograms
        {
            get
            {
                return _clientPrograms ?? string.Empty;
            }
            set
            {
                if (!(_clientPrograms ?? string.Empty).Equals(value))
                {
                    _clientPrograms = value;
                    OnPropertyChanged("ClientPrograms");
                }
            }
        }

        /// <summary>
        /// Recurrence information about the subscription associated with this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   08/13/2007  Modified setter to resubscribe to events
        /// </history>
        [Report]
        public SubscriptionInformation SubscriptionInformation
        {
            get
            {
                return _subscriptionInformation ?? (_subscriptionInformation = GetNewSubscriptionInformation());
            }
            set
            {
                base.Setter(ref _subscriptionInformation, value, SubscriptionInformation_PropertyChanged,
                            FN.SubscriptionInformation,
                            FN.IsSubscription);
            }
        }

        /// <summary>
        /// When this trip was entered in the system
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime EntryDate
        {
            get
            {
                return _entryDate;
            }
            set
            {
                if (!_entryDate.Equals(value))
                {
                    _entryDate = value;
                    OnPropertyChanged("EntryDate");
                }
            }
        }

        /// <summary>
        /// Driver for this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/07/2007  Changed name from DriverID to Driver
        ///     [Tim Hibbard]   06/07/2007  Changed type from int to Driver
        ///     [Tim Hibbard]   08/13/2007  Modified setter to resubscribe to events
        /// </history>
        [ContainsAttribute, Report]
        [RuleIgnore]
        public Driver Driver
        {
            get
            {
                return _driver ?? (_driver = GetNewDriver());
            }
            set
            {
                base.Setter(ref _driver, value, Driver_PropertyChanged, FN.Driver);
            }
        }

        /// <summary>
        /// Typically stores the authorized or billable miles for a trip leg
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/04/2007  Changed to double
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        //[Report]
        //[XmlAttribute]
        //[DataMember]
        //public double Mileage
        //{
        //    get
        //    {
        //        return _mileage ?? 0D;
        //    }
        //    set 
        //    {
        //        if (!(_mileage ?? 0D).Equals(value))
        //        {
        //            _mileage = value;
        //            OnPropertyChanged("Mileage");
        //            OnPropertyChanged("CalculatedActualDistanceOfTrip");
        //            OnPropertyChanged("CalculatedMPHOfTrip");
        //            OnPropertyChanged("CalculatedActualsToString");
        //            OnPropertyChanged("FleetManager.Statistics.Deadhead");
        //            OnPropertyChanged("FleetManager.Statistics.Revenue");
        //            OnPropertyChanged("FleetManager.Statistics.Service");
        //        }
        //    }
        //}

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/09/2011  Moved into Riders
        /// </history>
        //[XmlAttribute]
        //[DataMember]
        //public int OtherWC
        //{
        //    get
        //    {
        //        return _otherWC ?? 0;
        //    }
        //    set 
        //    {
        //        if (!(_otherWC ?? 0).Equals(value))
        //        {
        //            _otherWC = value;
        //            OnPropertyChanged("OtherWC");
        //        }
        //    }
        //}

        /// <summary>
        /// The purpose of this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [ContainsAttribute, Report]
        [XmlAttribute]
        [DataMember]
        public string AppointmentType
        {
            get
            {
                return _appointmentType ?? string.Empty;
            }
            set
            {
                if (!(_appointmentType ?? string.Empty).Equals(value))
                {
                    _appointmentType = value;
                    OnPropertyChanged("AppointmentType");
                }
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime ReturnTime
        {
            get
            {
                return _returnTime ?? DateTime.MinValue;
            }
            set
            {
                base.Setter(ref _returnTime, value, "ReturnTime", "IsValid", "ValidColor", "FlexOptions", "ReturnPUFlexString", "ReturnDOFlexString");
            }
        }

        /// <summary>
        /// How this person is coming back (Callback, pickup @ or no return)
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/28/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   08/23/2007  Made Virtual
        /// </history>
        [ContainsAttribute, Report]
        [XmlAttribute]
        [DataMember]
        public virtual string ReturnType
        {
            get
            {
                return _returnType ?? string.Empty;
            }
            set
            {
                if (!(_returnType ?? string.Empty).Equals(value))
                {
                    _returnType = value;
                    OnPropertyChanged("ReturnType");
                    OnPropertyChanged("TripType");
                    OnPropertyChanged("FlexOptions");
                    OnPropertyChanged("ReturnPUFlexString");
                    OnPropertyChanged("ReturnDOFlexString");
                    if (this.IsBuildingGet())
                    {
                        return;
                    }
                    if (value != "Roundtrip: Pick up at ->")
                    {
                        this.ReturnTime = DateTime.MinValue;
                        return;
                    }
                    //set return time based on filespecs
                    if (FileSpecsManager.Instance.DefaultReturnTime == DefaultReturnTime.ElevenFiftyNinePM)
                    {
                        this.ReturnTime = new DateTime(DateTime.MinValue.Year, DateTime.MinValue.Month, DateTime.MinValue.Day, 23, 59, 00).ToAdjustedSQLTime();
                    }

                }
            }
        }

        [ContainsAttribute]
        [RuleIgnore]
        [DataMember]
        public Place PickUpPlace
        {
            get
            {
                return this._pickUpPlace ?? (_pickUpPlace = this.GetNewPickUpPlace());
            }
            set
            {
                if (_pickUpPlace != null)
                {
                    _pickUpPlace.StatusChanged -= StatusChangedHandler;
                }
                base.Setter(ref _pickUpPlace, value, PickUpPlace_PropertyChanged, () => this.CalculateZoneFee(), "PickUpPlace", "IsValid", "ValidColor", "FlexOptions", "OutgoingPUFlexString", "OutgoingDOFlexString", "ReturnPUFlexString", "ReturnDOFlexString");

                if (_pickUpPlace != null)
                {
                    _pickUpPlace.StatusChanged += StatusChangedHandler;
                }
            }
        }

        [ContainsAttribute]
        [RuleIgnore]
        [DataMember]
        public Place DropOffPlace
        {
            get
            {
                return this._dropOffPlace ?? (_dropOffPlace = this.GetNewDropOffPlace());
            }
            set
            {
                if (_dropOffPlace != null)
                {
                    _dropOffPlace.StatusChanged -= StatusChangedHandler;
                }
                base.Setter(ref _dropOffPlace, value, DropOffPlace_PropertyChanged,
                    () => this.CalculateZoneFee(),
                    "DropOffPlace", "IsValid", "ValidColor", "FlexOptions", "OutgoingPUFlexString", "OutgoingDOFlexString", "ReturnPUFlexString", "ReturnDOFlexString");

                if (_dropOffPlace != null)
                {
                    _dropOffPlace.StatusChanged += StatusChangedHandler;
                }
            }
        }

        /// <summary>
        /// Where the trip ended
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   08/13/2007  Modified setter to resubscribe to events
        /// </history>
        [ContainsAttribute]
        [Report]
        [RuleIgnore]
        public Location DropOffLocation
        {
            get
            {
                return this.DropOffPlace.Address;
            }
        }

        [RuleIgnore]
        public Location DatabaseDropOffLocation { get; set; }


        [RuleIgnore]
        public Location DatabasePickUpLocation { get; set; }

        /// <summary>
        /// Where the trip started
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   08/13/2007  Modified setter to resubscribe to events
        ///     [Tim Hibbard]   10/09/2007  Added null check on setter
        /// </history>
        [ContainsAttribute]
        [Report]
        [RuleIgnore]
        public Location PickUpLocation
        {
            get
            {
                return this.PickUpPlace.Address;
            }
        }

        /// <summary>
        /// Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/20/2008  Removed setter
        ///     [Tim Hibbard]   06/20/2008  Made getter dynamic based on client
        /// </history>
        public string Class
        {
            get
            {
                var rv = "AMB";
                if (this.Client.SpecialNeeds.HasWheelChair || this.Client.SpecialNeeds.NeedsWheelChairVan)
                {
                    rv = "NAM";
                }

                if (this.Client.CalculateAge(DateTime.Now).Years >= FileSpecsManager.Instance.ssAge)
                {
                    rv = rv + "ELD";
                }
                return rv;
            }
        }

        /// <summary>
        /// What time the client requested
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime TripTime
        {
            get
            {
                var rv = _tripTime ?? DateTime.MinValue;
                return rv.ToAdjustedSQLTime();
            }
            set
            {
                //if (!(_tripTime ?? DateTime.MinValue.ToAdjustedSQLTime()).Equals(value))
                //{
                //    _tripTime = value;
                //    OnPropertyChanged("TripTime");
                //    if (!Hub.IsCloning && !this.IsBuildingGet() && !value.IsMinValue())
                //    {
                //        this.CalculatePUDOTimeAndWindows();
                //    }
                //    //this.CalculatePickUpAndDropOffTime();
                //}

                base.Setter(ref _tripTime, value, () =>
                {
                    if (!Hub.IsCloning && !this.IsBuildingGet() && !value.IsMinValue())
                    {
                        this.CalculatePUDOTimeAndWindows();
                    }
                }, "TripTime", "ValidColor", "FlexOptions", "OutgoingPUFlexString", "OutgoingDOFlexString", "ReturnPUFlexString", "ReturnDOFlexString");

            }
        }

        public DateTime TripTimeWithDwellTime
        {
            get
            {
                if (this.TimeType == "P")
                {
                    return this.TripTime;
                }
                return this.TripTime.AddMinutes(0 - this.Client.DwellTime);
            }
        }

        /// <summary>
        /// The date the trip is taking place
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/20/2008  Changed to check equality before setting
        ///     [Tim Hibbard]   06/20/2008  Changed to set availableroutes to null
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public DateTime TripDate
        {
            get
            {
                return _tripDate.Date;
            }
            set
            {
                if (!(_tripDate).Equals(value))
                {
                    _tripDate = value;
                    _availableRoutes = null;
                    OnPropertyChanged("AvailableRoutes");
                    OnPropertyChanged("TripDate");
                    OnPropertyChanged("ClientAgeStringOnTripDate");
                    //#if DEBUG
                    //   this.TripComment = this.TripComment + "-DateChanged";                            
                    //#endif
                }
            }
        }

        [XmlIgnore]
        [RuleIgnore]
        public DateTime TripDateTime
        {
            get
            {
                return DateTime.Parse(string.Format("{0} {1}", this.TripDate.ToShortDateString(), this.TripTime.ToShortTimeString()));
            }
        }

        /// <summary>
        /// When this trip is going next
        /// </summary>
        /// <history>   
        ///     [Tim Hibbard]   06/07/2010  Created
        /// </history>
        public virtual DateTime NextTripDate
        {
            get
            {
                return this.TripDate;
            }
        }



        /// <summary>
        /// An awful hack for List and Labels
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   06/03/2009  Created
        /// </history>
        [ReportAttribute]
        public string TripDateStringForReport
        {
            get
            {
                return this.TripDate.ToShortDateString();
            }
        }

        /// <summary>
        /// The client's age when this trip was taken
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public int Age
        {
            get
            {
                //try to calculate the age first
                if (_client != null)
                {
                    var rv = Client.CalculateAge(TripDate).Years;
                    if (rv > 0) return rv;
                }
                return _age ?? 0;
            }
            set
            {
                if (!(_age ?? 0).Equals(value))
                {
                    _age = value;
                    OnPropertyChanged("Age");
                }
            }
        }

        /// <summary>
        /// The user that scheduled this trip Kyle Edit
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/07/2007  Changed name from Username to User
        ///     [Tim Hibbard]   06/07/2007  Changed type from string to User
        ///     [Tim Hibbard]   08/13/2007  Modified setter to resubscribe to events
        /// </history>
        [ContainsAttribute, Report]
        [RuleIgnore]
        public User User
        {
            get
            {
                return _user ?? (_user = GetNewUser());
            }
            set
            {
                base.Setter(ref _user, value, User_PropertyChanged, FN.User);
            }
        }

        /// <summary>
        /// The clients age on the date the trip took place
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   08/27/2010  Created
        /// </history>
        public int ClientAgeOnTripDate
        {
            get
            {
                return this.Client.CalculateAge(this.TripDate).Years;
            }
        }

        public virtual string ClientAgeStringOnTripDate
        {
            get
            {
                if (EntitiesBase.IsNullOrEmpty(this.Client))
                {
                    return "Client not set";
                }
                var age = this.Client.CalculateAge(this.TripDate);
                if (age.Years == 0 && age.Months == 0)
                {
                    return "";
                }
                return string.Format("{0} years, {1} months old on {2} ({3})", age.Years, age.Months, this.TripDate.ToString("MMMM d"), this.Client.BirthDate.GetValueOrDefault().ToShortDateString());
            }
        }

        /// <summary>
        /// The client taking this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   06/07/2007  Changed name from ClientID to Client
        ///     [Tim Hibbard]   06/07/2007  Changed type from int to Client
        ///     [Tim Hibbard]   08/13/2007  Modified setter to resubscribe to events
        ///     [Tim Hibbard]   09/27/2007  Wrote Location, Cost1 and TripPrograms to this
        ///     [Tim Hibbard]   09/28/2007  Wrote Location.Name to this.PickUpLocation
        ///     [Tim Hibbard]   06/26/2008  Reset the rules on setter
        ///     [Tim Hibbard]   05/11/2009  Cloned location because propchange routing was incorrect
        /// </history>
        [ContainsAttribute, Report]
        [RuleIgnore]
        [DataMember]
        public Client Client
        {
            get
            {
                return _client ?? (_client = GetNewClient());
            }
            set
            {
                if (!(_client ?? (_client = GetNewClient())).Equals(value))
                {
                    _client.PropertyChanged -= Client_PropertyChanged;

                    if (value != null && !Hub.IsCloning && !this.IsBuildingGet() && !Hub.IsUpdatingFutureTripsAfterClientSave)
                    {
                        //if (value.Location.IsCompleteForMapping)
                        //{
                        //    this.PickUpPlace.Address = value.Location.Clone<Location>();
                        //    this.PickUpPlace.StopName = "Home";
                        //    this.PickUpPlace.Save();

                        //    OnPropertyChanged("PickUpPlace.Address");
                        //    OnPropertyChanged("PickUpPlace.Address.Name");
                        //}

                        this.Riders.PCA = value.SpecialNeeds.PersonalCareAttendant;

                        if (value.ClientProgramSet.DefaultProgram != null)
                        {
                            value.ClientProgramSet.DefaultProgram.Populate();
                            //this.Cost1 = value.ClientProgramSet.DefaultProgram.Fee_CoPay;
                            //this.Cost2 = value.ClientProgramSet.DefaultProgram.Fee_Fixed;
                            this.TripProgram = value.ClientProgramSet.DefaultProgram;
                            this.TripBilling.ClientCoPay = value.ClientProgramSet.DefaultProgram.ClientCoPay;
                            this.TripBilling.ClientBilled = value.ClientProgramSet.DefaultProgram.ClientBilled;
                            this.TripBilling.ProgramBilled = value.ClientProgramSet.DefaultProgram.ProgramBilled;
                            this.TripBilling.ContractorDue = value.ClientProgramSet.DefaultProgram.ContractorDue;
                        }
                    }

                    _client = value;
                    if (_client != null)
                    {
                        _client.PropertyChanged += Client_PropertyChanged;
                    }
                    base.ResetRules();
                    OnPropertyChanged("Client");
                    OnPropertyChanged("ClientAgeStringOnTripDate");
                }
            }
        }

        public void SetPickUpLocationFromClient(Client c)
        {
            Logging.Logger.Log(string.Format("Setting pu to Home Current = {0}, {1}, {2}",
                this.PickUpPlace.ID, this.PickUpPlace.StopName, this.PickUpPlace.Address.Address1));
            this.PickUpPlace = c.Location;
            this.PickUpPlace.StopName = "Home";

            this.PickUpPlace.Save();
        }

        /// <summary>
        /// The associated subscription 
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public int SubscriptionID
        {
            get
            {
                return _subscriptionID ?? 0;
            }
            set
            {
                if (!(_subscriptionID ?? 0).Equals(value))
                {
                    _subscriptionID = value;
                    OnPropertyChanged("SubscriptionID");
                    OnPropertyChanged("ReservationID");
                }
            }
        }

        /// <summary>
        /// The associated appointment
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public int AppointmentID
        {
            get
            {
                return _appointmentID ?? 0;
            }
            set
            {
                if (!(_appointmentID ?? 0).Equals(value))
                {
                    _appointmentID = value;
                    OnPropertyChanged("AppointmentID");
                    OnPropertyChanged("ReservationID");
                    OnPropertyChanged("IsNewAppointment");
                    OnPropertyChanged("IsNotNewAppointment");
                }
            }
        }

        /// <summary>
        /// The type of trip.  Need to be an enumeration
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        ///     [Tim Hibbard]   09/29/2007  Made virtual.  appointment and subscription will override
        ///     [Tim Hibbard]   08/12/2008  Checked value on setter before setting
        /// </history>
        [ContainsAttribute, Report]
        [XmlAttribute]
        [DataMember]
        public virtual string TripType
        {
            get
            {
                return _tripType ?? string.Empty;
            }
            set
            {
                if (!(_tripType ?? string.Empty).Equals(value))
                {
                    _tripType = value;
                    OnPropertyChanged("TripType");
                    base.OnPropertyChanged("IsOneWay");
                    base.OnPropertyChanged("WillGenerateCallback");
                    base.OnPropertyChanged("IsElibleForNextLeg");
                }
            }
        }

        /// <summary>
        /// The ID of this trip
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public int TripID
        {
            get
            {
                return _tripID ?? 0;
            }
            set
            {
                if (!(_tripID ?? 0).Equals(value))
                {
                    _tripID = value;
                    OnPropertyChanged("TripID");
                }
            }
        }

        /// <summary>
        /// The ID of the table represented by this object
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   02/27/2007  Created
        ///     [Tim Hibbard]   05/23/2007  Added OnPropertyChanged on setter
        /// </history>
        [Report]
        [XmlAttribute]
        [DataMember]
        public int ID
        {
            get
            {
                return _id ?? 0;
            }
            set
            {
                if (!(_id ?? 0).Equals(value))
                {
                    _id = value;
                    OnPropertyChanged("ID");
                }
            }
        }

        /// <summary>
        /// Returns if this trip is scheduled on a route
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   09/03/2008  Created
        ///     [Tim Hibbard]   09/08/2008  Accuratized it!
        /// </history>
        public bool IsScheduled
        {
            get
            {
                return !string.IsNullOrEmpty(this.FleetManager.Route.Name);
            }
        }

        #endregion

        #region --Property Initializers--
        private TripBilling GetNewTripBilling()
        {
            var rv = new TripBilling(this);
            rv.PropertyChanged += TripBilling_PropertyChanged;
            return rv;
        }


        private FleetManager GetNewFleetManager()
        {
            var rv = new FleetManager();
            rv.PropertyChanged += FleetManager_PropertyChanged;
            return rv;
        }

        private Riders GetNewRiders()
        {
            var rv = new Riders();
            rv.PropertyChanged += Riders_PropertyChanged;
            return rv;
        }

        private DummyFields GetNewDummyFields()
        {
            var rv = new DummyFields();
            rv.PropertyChanged += DummyFields_PropertyChanged;
            return rv;
        }

        private ClientProgram GetNewTripProgram()
        {
            var rv = new ClientProgram();
            rv.PropertyChanged += _tripProgram_PropertyChanged;
            return rv;
        }

        private SubscriptionInformation GetNewSubscriptionInformation()
        {
            var rv = new SubscriptionInformation();
            rv.PropertyChanged += SubscriptionInformation_PropertyChanged;
            return rv;
        }

        private Driver GetNewDriver()
        {
            var rv = new Driver();
            rv.PropertyChanged += Driver_PropertyChanged;
            return rv;
        }

        private Place GetNewDropOffPlace()
        {
            var rv = new Place();
            rv.PropertyChanged += DropOffPlace_PropertyChanged;
            rv.StatusChanged += StatusChangedHandler;
            return rv;
        }

        private Place GetNewPickUpPlace()
        {
            var rv = new Place();
            rv.PropertyChanged += new PropertyChangedEventHandler(PickUpPlace_PropertyChanged);
            rv.StatusChanged += StatusChangedHandler;
            return rv;
        }

        private User GetNewUser()
        {
            var rv = new User();
            rv.PropertyChanged += User_PropertyChanged;
            return rv;
        }

        private Client GetNewClient()
        {
            var rv = new Client();
            rv.PropertyChanged += Client_PropertyChanged;
            return rv;
        }

        #endregion

        #region --Private Methods--
        public bool IsScheduledOn(FleetManager FM)
        {
            return this.FleetManager.UniqueID == FM.UniqueID || (this.FleetManager.ID != 0 && this.FleetManager.ID == FM.ID);
        }

        public DateTime RecalculatePickUpTime()
        {
            if (this.TimeType == "P")
            {
                return this.TripTime;
            }
            return this.TripTime.AddMinutes(0 - ((this.Client.DwellTime * 2) + this.TravelTime));
        }

        public DateTime RecalculateDropOffTime()
        {
            if (this.TimeType == "D")
            {
                return this.TripTime.AddMinutes(0 - this.Client.DwellTime);
            }
            return this.TripTime.AddMinutes((this.Client.DwellTime * 2) + this.TravelTime);
        }

        public void CalculatePUDOTimeAndWindows()
        {
            //if we are building this object from the database, we don't want to calculate this
            //because it will default to the original...which may overwrite any changes made by the user
            if (this.IsBuildingGet())
            {
                return;
            }

            if (this.TripTime.IsMinValue())
            {
                return;
            }

            _isCalculatingTripTimes = true;
            this.ClearStops();

            //if they use 11:59 as a filler and that is what the trip time is
            if (FileSpecsManager.Instance.DefaultReturnTime == DefaultReturnTime.ElevenFiftyNinePM && this.TripTime.TimeEquals(DateTime.Parse("11:59 PM")))
            {
                this.PickUpTime = this.TripTime;
                this.DropOffTime = this.TripTime;
                this.TWa = DateTime.Parse("11:59 PM");
                this.TWb = DateTime.Parse("11:59 PM");
                this.TWc = DateTime.Parse("11:59 PM");
                this.TWd = DateTime.Parse("11:59 PM");
                _isCalculatingTripTimes = false;
                return;
            }

            //calculate PU, DO Times and DriveTime and Distance

            int clientDwell = this.Client.DwellTime;
            var fs = Managers.FileSpecsManager.Instance;

            //calculate drivetime here
            int driveTime = (int)this.TravelTime;
            if (driveTime == 0)
            {
                driveTime = fs.maxRT;
            }


            //10-29-2013
            //if this trip was autoscheduled, we don't want to revert back to 
            //the original. I'm not sure when that would be the case
            //I'm setting a breakpoint to find out
            if (this.ScheduledByAutoSchedule)
            {
                _isCalculatingTripTimes = false;
                return;
            }

            int earlyPU = 0;
            int latePU = fs.DT / 60;
            int earlyDO = fs.DT / 60;
            int lateDO = 0;

            if (fs.AmountEarlyForPU > 0)
            {
                earlyPU = fs.AmountEarlyForPU;
            }

            if (fs.AmountLateForPU > 0)
            {
                latePU = fs.AmountLateForPU;
            }

            if (fs.AmountEarlyForDO > 0)
            {
                earlyDO = fs.AmountEarlyForDO;
            }

            if (fs.AmountLateForDO > 0)
            {
                lateDO = fs.AmountLateForDO;
            }



            //figure out what our starting point is
            if (this.TimeType == "P")
            {
                this.PickUpTime = this.TripTime;
                this.DropOffTime = this.TripTime.AddMinutes(clientDwell + driveTime);

                this.TWa = this.TripTime.AddMinutes(0 - earlyPU);
                this.TWb = this.TripTime.AddMinutes(latePU);
                this.TWc = this.TripTime.AddMinutes(0 - earlyPU).AddMinutes(clientDwell).AddMinutes(driveTime);
                this.TWd = this.TripTime.AddMinutes(latePU).AddMinutes(driveTime).AddSeconds(fs.RC).AddMinutes(clientDwell);
            }
            else if (this.TimeType == "D")
            {
                this.DropOffTime = this.TripTime.AddMinutes(0 - clientDwell);
                this.PickUpTime = this.TripTime.AddMinutes(0 - ((clientDwell * 2) + driveTime));

                this.TWd = this.TripTime.AddMinutes(0 - clientDwell).AddMinutes(lateDO);
                this.TWc = this.TripTime.AddMinutes(0 - clientDwell).AddMinutes(0 - earlyDO);
                this.TWb = this.TripTime.AddMinutes(0 - (clientDwell * 2)).AddMinutes(0 - driveTime).AddMinutes(lateDO);
                this.TWa = this.TripTime.AddMinutes(0 - (clientDwell * 2)).AddMinutes(0 - driveTime).AddSeconds(0 - fs.RC).AddMinutes(0 - earlyDO);
            }
            _isCalculatingTripTimes = false;
        }

        private bool _isCalculatingTripTimes = false;

        public bool IsDateSavedToHistory
        {
            get
            {
                if (_isDateSavedToHistory.HasValue)
                {
                    return _isDateSavedToHistory.Value;
                }

                if (this.AssignedSchedule == null)
                {
                    this.AssignedSchedule = new Schedule(this.TripDate);
                }

                _isDateSavedToHistory = this.AssignedSchedule.IsSavedToHistory;

                return _isDateSavedToHistory.Value;
            }
        }

        private void InitializeTrip()
        {
            //if it is building, then why would we set the user.
            if (this.IsBuildingGet())
            {
                return;
            }

            this.User = Hub.CurrentUser.Clone<User>();

        }

        private void ResetDistanceAndDuration()
        {
            this.TravelDistance = 0;
            this.TravelTime = 0;
        }

        void PickUpArrive_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged("PickUpArrive." + e.PropertyName);
        }
        void PickUpPerform_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged("PickUpPerform." + e.PropertyName);
        }
        void DropOffArrive_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged("DropOffArrive." + e.PropertyName);
        }
        void DropOffPerform_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged("DropOffPerform." + e.PropertyName);
        }
        void Signature_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged("Signature." + e.PropertyName);
        }
        void Downloaded_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged("Downloaded." + e.PropertyName);
        }

        void _tripProgram_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("TripProgram." + e.PropertyName);
            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidColor");
        }

        void StatusChangedHandler(object sender, StatusEventArgs e)
        {
            base.OnStatusChanged(e);
        }

        void Riders_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "RidersAges")
            {
                OnPropertyChanged("ValidColor");
            }
            if (e.PropertyName == "PCA")
            {
                OnPropertyChanged("ClientOrTripHasPCA");
            }
            OnPropertyChanged(FN.Riders + FN.Dot + e.PropertyName);

        }

        void TripBilling_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(FN.TripBilling + FN.Dot + e.PropertyName);
        }

        void Client_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "SpecialNeeds.NeedsWheelChairVan":
                    this._availableRoutes = null;
                    OnPropertyChanged("AvailableRoutes");
                    OnPropertyChanged("Class");
                    break;
                case "SpecialNeeds.HasWheelChair":
                    OnPropertyChanged("Class");
                    break;
                case "BirthDate":
                    OnPropertyChanged("Class");
                    OnPropertyChanged("Age");
                    break;

                default:
                    break;
            }

            OnPropertyChanged("Client." + e.PropertyName);
        }

        void User_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("User." + e.PropertyName);
        }

        void PickUpPlace_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PlacePropertyChangedHandler("PickUpPlace." + e.PropertyName);
        }

        void DropOffPlace_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.PlacePropertyChangedHandler("DropOffPlace." + e.PropertyName);
        }

        void PlacePropertyChangedHandler(string PropertyName)
        {
            //if (!this.IsBuildingGet())
            //{
            //    this.ResetDistanceAndDuration();
            //}
            OnPropertyChanged(PropertyName);
            OnPropertyChanged("IsValid");
            OnPropertyChanged("ValidColor");
        }

        void Driver_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Driver." + e.PropertyName);
        }

        void SubscriptionInformation_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("SubscriptionInformation." + e.PropertyName);
            OnPropertyChanged("IsSubscription");
        }

        void DummyFields_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("DummyFields." + e.PropertyName);
        }

        //void Route_PropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    OnPropertyChanged("Route." + e.PropertyName);
        //}

        void FleetManager_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Contains(FN.AssignedTrips))
            {
                OnPropertyChanged("FleetManager." + e.PropertyName);
                OnPropertyChanged("IsValid");
                OnPropertyChanged("ValidColor");
            }
        }

        void stop_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnPropertyChanged("Stops." + e.PropertyName);
        }

        #endregion

        #region --Public Methods--
        public TripBase Clone()
        {
            TripBase rv = null;
            if (this is Trip)
            {
                rv = this.Clone<Trip>();
            }
            else if (this is TripHistory)
            {
                rv = this.Clone<TripHistory>();
            }
            else if (this is Subscription)
            {
                rv = this.Clone<Subscription>();
            }
            else if (this is Appointment)
            {
                rv = this.Clone<Appointment>();
            }

            return rv;
        }

        internal void OnAutoScheduleProgressChanged(int Progress)
        {
            if (this.AutoScheduleProgressChanged != null)
            {
                this.AutoScheduleProgressChanged(this, new ProgressChangedEventArgs(Progress, null));
            }
        }

        public bool Autoschedule(string saveIdentifier)
        {
            var rv = false;
            this.OnAutoScheduleProgressChanged(10);

            string uniqueID = "";
            int oldID = 0;
            if (!_fleetManager.Equals(new FleetManager()))
            {
                uniqueID = _fleetManager.UniqueID;
                oldID = _fleetManager.ID;
            }

            this.Unschedule();
            this.OnAutoScheduleProgressChanged(20);
            this.Save();

            this.OnAutoScheduleProgressChanged(30);

            var a = new AutoScheduleTrip(this);
            this.OnAutoScheduleProgressChanged(40);
            if (a.BestRoute != null)
            {
                this.OnAutoScheduleProgressChanged(75);
                this.Schedule(a.BestRoute, saveIdentifier);
                this.UpdateOldAssignedTrips(a.Schedule, uniqueID, oldID);
                this.OnAutoScheduleProgressChanged(80);
                if (this.Save().SaveResult == ParaPlan.Enumerations.SaveEntityResult.Success)
                {
                    rv = true;
                }
                this.OnAutoScheduleProgressChanged(90);
            }

            a = null;

            return rv;
        }

        public bool IsActiveDuringTimeSpan(DateTime time1, DateTime time2)
        {
            var rv = false;

            return rv;
        }

        /// <summary>
        /// Returns if associated reservation is in the future
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   10/20/2008  Created
        /// </history>
        public bool IsReservationInFuture()
        {
            bool rv = false;
            if (this.SubscriptionID != 0)
            {
                var s = new SubscriptionManager().GetByID(this.SubscriptionID);
                if (s != null)
                {
                    rv = s.SubscriptionInformation.SubscriptionEnd > DateTime.Now;
                }
            }
            else if (this.AppointmentID != 0)
            {
                var a = new AppointmentManager().GetByID(this.AppointmentID);
                if (a != null)
                {
                    rv = a.TripDate > DateTime.Now;
                }
            }
            return rv;
        }

        /// <summary>
        /// Returns if this 
        /// </summary>
        public bool IsNewAppointment
        {
            get
            {
                if (!(this is Appointment))
                {
                    return false;
                }

                if (this.AppointmentID != 0)
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsNotNewAppointment
        {
            get
            {
                return !IsNewAppointment;
            }
        }

        public int ReservationID
        {
            get
            {
                if (this.SubscriptionID != 0) return this.SubscriptionID;
                if (this.AppointmentID != 0) return this.AppointmentID;
                return 0;
            }
        }

        public string ReservationIDWithType
        {
            get
            {
                if (this.SubscriptionID != 0) return "S" + this.SubscriptionID.ToString();
                if (this.AppointmentID != 0) return "A" + this.AppointmentID.ToString();
                return "";
            }
        }

        /// <summary>
        /// The associated reservation
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   10/20/2008  Created
        /// </history>
        public ReservationBase Reservation()
        {
            if (_reservation == null)
            {
                if (this.SubscriptionID != 0)
                {
                    _reservation = new SubscriptionManager().GetByID(this.SubscriptionID);
                }
                else if (this.AppointmentID != 0)
                {
                    _reservation = new AppointmentManager().GetByID(this.AppointmentID);
                }
            }
            return _reservation;
        }

        /// <summary>
        /// Flags this trip as dispatched with provided timestamp
        /// </summary>
        /// <param name="TimeStamp">Dispatch Time</param>
        /// <history>
        ///     [Tim Hibbard]   07/09/2008  Created
        /// </history>
        public void Dispatch(DateTime TimeStamp)
        {
            this.DummyFields.MT_D_1 = TimeStamp;
        }

        /// <summary>
        /// Flags this trip as dispatched with right now as timestamp
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   07/09/2008  Created
        /// </history>
        public void Dispatch()
        {
            this.Dispatch(DateTime.Now);
        }

        /// <summary>
        /// Flags this trip as picked up with provided timestamp
        /// </summary>
        /// <param name="TimeStamp">PickUp Time</param>
        /// <history>
        ///     [Tim Hibbard]   07/09/2008  Created
        /// </history>
        public void PickUp(DateTime TimeStamp)
        {
            this.PickUpStamp = TimeStamp;
        }

        /// <summary>
        /// Flags this trip as picked up with right now as timestamp
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   07/09/2008  Created
        /// </history>
        public void PickUp()
        {
            this.PickUp(DateTime.Now);
        }

        /// <summary>
        /// Flags this trip as dropped off with provided timestamp
        /// </summary>
        /// <param name="TimeStamp">DropOff Time</param>
        /// <history>
        ///     [Tim Hibbard]   07/09/2008  Created
        /// </history>
        public void DropOff(DateTime TimeStamp)
        {
            this.DropOffStamp = TimeStamp;
        }

        /// <summary>
        /// Flags this trip as dropped off with right now as timestamp
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   07/09/2008  Created
        /// </history>
        public void DropOff()
        {
            this.DropOff(DateTime.Now);
        }

        /// <summary>
        /// Calculates the distance and duration of this trip
        /// </summary>
        /// <returns>True if successfully completed</returns>
        /// <history>
        ///     [Tim Hibbard]   12/27/2007  Commented
        /// </history>
        public bool CalculateDriveTime()
        {
            return this.CalculateDriveTime(false);
        }

        public bool CalculateDriveTime(bool FailOnZeroDistance)
        {
            bool rv = false;

            base.OnStatusChanged(new StatusEventArgs("Validating address", 1, 5));
            string errorMessage = "";
            if (!this.DropOffLocation.IsCompleteForMapping)
            {
                base.OnStatusChanged(new StatusEventArgs(true));
                errorMessage = "Drop Off Location is incomplete";
                Logger.Log("CalculateDriveTime error: {0}", errorMessage);
                throw new DriveTimeCouldNotBeCalculatedException(DriveTimeCouldNotBeCalculatedException.InvalidLocation.DropOff,
                                                                 DriveTimeCouldNotBeCalculatedException.FailureReason.IncompleteAddress
                                                                 , errorMessage);
            }
            if (!this.PickUpLocation.IsCompleteForMapping)
            {
                base.OnStatusChanged(new StatusEventArgs(true));
                errorMessage = "Pick Up Location is incomplete";
                Logger.Log("CalculateDriveTime error: {0}", errorMessage);
                throw new DriveTimeCouldNotBeCalculatedException(DriveTimeCouldNotBeCalculatedException.InvalidLocation.PickUp,
                                                                 DriveTimeCouldNotBeCalculatedException.FailureReason.IncompleteAddress,
                                                                 errorMessage);
            }
            base.OnStatusChanged(new StatusEventArgs("Geocoding drop off address", 2, 5));
            if (!this.DropOffLocation.Geocode(false))
            {
                base.OnStatusChanged(new StatusEventArgs(true));
                errorMessage = "Drop Off Location is invalid";
                Logger.Log("CalculateDriveTime error: {0}", errorMessage);
                throw new DriveTimeCouldNotBeCalculatedException(DriveTimeCouldNotBeCalculatedException.InvalidLocation.DropOff,
                                                                 DriveTimeCouldNotBeCalculatedException.FailureReason.UnableToGeocode,
                                                                 errorMessage);
            }

            base.OnStatusChanged(new StatusEventArgs("Geocoding pick up address", 3, 5));
            if (!this.PickUpLocation.Geocode(false))
            {
                base.OnStatusChanged(new StatusEventArgs(true));
                errorMessage = "Pick Up Location is invalid";
                Logger.Log("CalculateDriveTime error: {0}", errorMessage);
                throw new DriveTimeCouldNotBeCalculatedException(DriveTimeCouldNotBeCalculatedException.InvalidLocation.PickUp,
                                                                 DriveTimeCouldNotBeCalculatedException.FailureReason.UnableToGeocode,
                                                                 errorMessage);
            }

            //this is a change

            base.OnStatusChanged(new StatusEventArgs("Calculating drivetime", 4, 5));
            var results = DriveTimeManager.CalculateDriveTime(new DriveTime() { ToLocation = this.DropOffLocation, FromLocation = this.PickUpLocation });

            this.TravelDistance = results.Distance;

            double travelTime = results.Duration.TotalMinutes * Managers.FileSpecsManager.Instance.RF;

            //if (FileSpecsManager.Instance.MPHCityMinor > 0)
            //{
            //    travelTime = Math.Round((this.TravelDistance / FileSpecsManager.Instance.MPHCityMinor) * 60, 0);
            //}


            var calcTime = this.CalculateTravelTime(this.TravelDistance);

            if (calcTime > 0)
            {
                travelTime = Math.Round(calcTime, 0);
            }

            if (travelTime == 0 && this.TravelDistance > 0)
            {
                travelTime = 1;
            }

            if (!this.ManuallySetDuration)
            {
                this.TravelTime = travelTime;
            }

            rv = true;

            base.OnStatusChanged(new StatusEventArgs(true));

            if (FailOnZeroDistance)
            {
                rv = this.TravelDistance > 0;
            }

            return rv;
        }

        public void CalculateTravelTime()
        {
            this.TravelTime = Math.Round(this.CalculateTravelTime(this.TravelDistance));
        }

        public double CalculateTravelTime(double distance)
        {

            return ParaPlan.GIS.Helpers.DistanceCalculator.GetTimeHelper(distance);

            var fs = FileSpecsManager.Instance;
            double mDis = fs.MPHCityMinorDistance;
            double mMPH = fs.MPHCityMinor;
            double MDis = fs.MPHCityMajorDistance;
            double MMPH = fs.MPHCityMajor;
            double hw = fs.MPHHighway;

            if (fs.MPHCityMinor == 0 || fs.MPHCityMajor == 0 | fs.MPHHighway == 0)
            {
                return 0;
            }

            var travelTime = 0d;
            var distanceLeft = distance;

            var minor = mDis * 2;
            if (distanceLeft < minor)
            {
                minor = distanceLeft;
            }

            travelTime = travelTime + ((minor / mMPH) * 60);
            distanceLeft = distanceLeft - minor;

            if (distanceLeft <= 0)
            {
                return travelTime;
            }

            var major = MDis * 2;
            if (distanceLeft < major)
            {
                major = distanceLeft;
            }

            travelTime = travelTime + ((major / MMPH) * 60);
            distanceLeft = distanceLeft - major;

            if (distanceLeft <= 0)
            {
                return travelTime;
            }

            //everything left over is highway. calc and return

            travelTime = travelTime + ((distanceLeft / hw) * 60);

            return travelTime;

        }

        public void CalculateZoneFee()
        {

            if (this.IsBuildingGet())
            {
                return;
            }

            if (this.TripProgram.PaymentMethod.ID != 4)
            {
                return;
            }

            if (this.ManuallySetFees)
            {
                return;
            }

            //make sure zones are calculated
            if (string.IsNullOrWhiteSpace(this.PickUpLocation.Zone.Name))
            {
                this.PickUpLocation.Zone = GeoZoneManager.GetZone(this.PickUpLocation);
            }

            if (string.IsNullOrWhiteSpace(this.DropOffLocation.Zone.Name))
            {
                this.DropOffLocation.Zone = GeoZoneManager.GetZone(this.DropOffLocation);
            }

            var billing = ZoneBillingManager.CalculateZone(this.PickUpLocation.Zone, this.DropOffLocation.Zone, this.TripProgram);


            if (billing == null)
            {
                return;
            }

            this.TripBilling.ClientCoPay = billing.CoPay;
            this.TripBilling.ProgramBilled = billing.FixedFee;


        }

        /// <summary>
        /// Removes any scheduling information associated with this trip
        /// </summary>
        /// <returns>True if successful</returns>
        /// <history>
        ///     [Tim Hibbard]   06/17/2008  Created
        /// </history>
        public bool Unschedule()
        {
            return Unschedule(false);
        }

        /// <summary>
        /// Removes any scheduling information associated with this trip
        /// </summary>
        /// <returns>True if successful</returns>
        /// <history>
        ///     [Tim Hibbard]   05/24/2013  Created
        /// </history>
        public bool Unschedule(bool FromREST)
        {
            //if this is already cancelled or no-showed, then just move on
            if (this.Cancelled)
            {
                return false;
            }
            if (this.NoGo)
            {
                return false;
            }

            if (!FromREST)
            {
                TripScheduleHistoryManager.AddNew(this, TripScheduleHistoryActionType.Unscheduled, 0);
            }


            this.RemoveTripFromVehicle(false, this.FleetManager);

            var rv = false;
            try
            {
                this.FleetManager = new FleetManager();
                this.FleetManagerID = 0;
                this.Cancelled = false;
                this.CancelReason = "";
                this.NoGo = false;

                //if this trip was autoscheduled, then likely we
                //adjusted the times of the trip to make it fit
                //this resets the times based on the client request
                if (this.ScheduledByAutoSchedule)
                {
                    this.ScheduledByAutoSchedule = false;
                    this.CalculatePUDOTimeAndWindows();
                }



                rv = true;
            }
            catch (Exception)
            {
                rv = false;
            }
            return rv;
        }

        /// <summary>
        /// Cancels this trip with a cancel reason
        /// </summary>
        /// <param name="CancelCode">The reason for the cancellation</param>
        /// <returns>True if successfull</returns>
        /// <history>
        ///     [Tim Hibbard]   06/17/2008  Created
        ///     [Tim Hibbard]   10/06/2008  Added check for permissions
        /// </history>
        public bool Cancel(CancelCode CancelCode)
        {
            return this.Cancel(CancelCode, false);
        }


        public void SendTripToVehicle(bool SaveFirst)
        {


            if (this.TripDate.Date != DateTime.Now.Date)
            {
                return;
            }

            if (this.IsDirty && SaveFirst)
            {
                this.Save();
            }



            if (PPRegistryManager.Instance.MDT)
            {
                SendTripToRanger();
            }

            if (PPRegistryManager.Instance.Mobile)
            {
                SendTripToAPN();
            }


        }

        private void SendTripToRanger()
        {
            if (string.IsNullOrEmpty(this.FleetManager.Vehicle.GPSUnitID))
            {
                return;
            }
            var tripType = GPSMessageType.ManifestTrip;
            var sendTrip = true;
            var mdt = this.MDTTripStatus;
            #region switch (mdt.Status)
            switch (mdt.Status)
            {
                case "Updated":
                case "Received":
                case "Riding":
                case "Arrived":
                    mdt.Status = "Updated";
                    mdt.SentDS = DateTime.Now;
                    tripType = GPSMessageType.ChangedTrip;
                    break;
                case "NoShow":
                case "Removed":
                case "Rejected":
                case "New":
                    mdt.Status = "New";
                    mdt.SentDS = DateTime.Now;
                    tripType = GPSMessageType.NewTrip;
                    break;
                case "Completed":
                    sendTrip = false;
                    break;
                default:
                    break;
            }
            #endregion

            if (!sendTrip)
            {
                return;
            }

            if (tripType == GPSMessageType.ChangedTrip)
            {
                mdt.Status = "Updated";
            }
            else
            {
                mdt.Status = "New";
            }

            var work = new Action<MDTTripStatus, TripBase, GPSMessageType>((message, t, messagetype) =>
            {
                var x = new GPSMessage();
                x.MessageDirection = GPSMessageDirection.ToRanger;
                x.Message = t.ToXmlString();
                x.MessageType = messagetype;
                x.Save();
                message.Save();
            });

            work.BeginInvoke(mdt, this, tripType, null, null);
        }

        private void SendTripToAPN()
        {
            if (this.FleetManager.Driver.DriverID == 0)
            {
                return;
            }

            //don't send a trip that has been dropped off
            if (!this.DropOffStamp.IsMinValue())
            {
                return;
            }

            //don't send a trip that is not for today
            if (this.TripDate.Date != DateTime.Now.Date)
            {
                return;
            }

            var work = new Action<TripBase>((t) =>
            {
                var driverID = t.FleetManager.Driver.DriverID;
                var message = string.Format("Manifest change: {0} {1} {2} PU {3} DO",
                    t.Client.Name.FirstName, t.Client.Name.LastName, t.PickUpTime.ToShortTimeString(),
                    t.DropOffTime.ToShortTimeString());
                APNManager.AddToQueueWithDriverID(driverID, message, "", -1, 0, t.TripID, 0);
            });
            work.BeginInvoke(this, null, null);
        }

        private void SwitchMDT(string OldUnitID, string saveIdentifier)
        {
            if (!PPRegistryManager.Instance.MDT)
            {
                return;
            }

            if (this.TripDate.Date != DateTime.Now.Date)
            {
                return;
            }


            this.OldGPSID = OldUnitID;

            var work = new Action<TripBase, string>((t, innerSaveIdentifier) =>
            {
                if (t.IsDirty)
                {
                    t.Save(innerSaveIdentifier);
                }
                if (string.IsNullOrWhiteSpace(this.OldGPSID))
                {
                    t.SendTripToRanger();
                    //scoped to just sending to Ranger on
                    //10-22-2013 because using the generic
                    //SentTripToVehicle was sending to APN when agencies
                    //were MDT and iOS
                    //t.SendTripToVehicle(false);
                    return;
                }

                var swap = new GPSMessage();
                swap.MessageDirection = GPSMessageDirection.ToRanger;
                swap.MessageType = GPSMessageType.SwappedBetweenVehicles;
                swap.Message = t.ToXmlString();
                swap.Save();
                t.MDTTripStatus.Status = "New";
                t.MDTTripStatus.Save();


            });

            work.BeginInvoke(this, saveIdentifier, null, null);
        }

        public void RemoveTripFromVehicle(bool saveFirst, FleetManager FM)
        {
            if (this.TripDate.Date != DateTime.Now.Date)
            {
                return;
            }

            if (this.IsDirty && saveFirst)
            {
                this.Save();
            }

            this.Downloaded = null;

            if (PPRegistryManager.Instance.MDT)
            {

                this.RemoveTripFromRanger(FM.Vehicle.GPSUnitID);
            }

            if (PPRegistryManager.Instance.Mobile)
            {
                Logger.Log("Notifying {0} about trip change to {1}", FM.Driver.Name, this.ToString());
                this.RemoveTripFromAPN(FM.Driver.DriverID);
            }

            Logger.Log("Removed {0} from Vehicle {1}", this.ToString(), FM.Vehicle.ToString());

        }

        private void RemoveTripFromRanger(string GPSUnitID)
        {
            //04-15-2013 - Tim - We shouldn't care if the fleetmanager has a GPSUnitID
            //At this exact moment, it may or may not, but it doesn't impact what this
            //function needs to do
            //if (string.IsNullOrWhiteSpace(this.FleetManager.Vehicle.GPSUnitID))
            //{
            //    return;
            //}

            //04-15-2013 - Tim - We cannot remove a trip if this is not populated
            if (string.IsNullOrWhiteSpace(GPSUnitID))
            {
                return;
            }


            var removeTrip = true;
            var mdt = this.MDTTripStatus;

            switch (mdt.Status)
            {
                case "Completed":
                case "Riding":
                case "Rejected":
                    removeTrip = false;
                    break;
                default:
                    break;
            }

            if (!removeTrip)
            {
                return;
            }

            this.OldGPSID = GPSUnitID;

            var work = new Action<MDTTripStatus, TripBase>((message, t) =>
            {
                var x = new GPSMessage();
                x.MessageDirection = GPSMessageDirection.ToRanger;
                x.Message = t.ToXmlString();
                x.MessageType = GPSMessageType.RemoveTrip;
                x.Save();
                message.Status = "Removed";
                message.Save();
            });

            work.BeginInvoke(mdt, this, null, null);
        }

        private void RemoveTripFromAPN(int DriverID)
        {
            if (DriverID == 0)
            {
                return;
            }

            var work = new Action<TripBase>((t) =>
            {
                var message = string.Format("Removed {0} {1} {2} PU from manifest",
                    t.Client.Name.FirstName, t.Client.Name.LastName, t.PickUpTime.ToShortTimeString());
                APNManager.AddToQueueWithDriverID(DriverID, message, "", -1, 0, t.TripID, 0);
            });
            work.BeginInvoke(this, null, null);
        }


        public bool Cancel(CancelCode CancelCode, bool CancelAssociatedLegs)
        {
            return Cancel(CancelCode, CancelAssociatedLegs, false);
        }


        public bool Cancel(CancelCode CancelCode, bool CancelAssociatedLegs, bool FromREST)
        {
            var rv = false;
            Logger.Log("Canceling {0} because of {1}", this.ToString(), CancelCode.CancelReason);
            if (!Hub.CurrentUser.Permissions.CanCancelTrips)
            {
                if (FromREST && Hub.CurrentUser.Permissions.Equals(new UserPermissions()))
                {
                    Hub.CurrentUser.Populate();
                    if (!Hub.CurrentUser.Permissions.CanCancelTrips)
                    {
                        Logger.Log("This user {0} cannot cancel {1}", Hub.CurrentUser.UserName, this.ToString());
                        return false;
                    }
                }
                else
                {
                    Logger.Log("This user {0} cannot cancel {1}", Hub.CurrentUser.UserName, this.ToString());
                    return false;
                }

            }

            if (!FromREST)
            {
                TripScheduleHistoryManager.AddNew(this, TripScheduleHistoryActionType.Cancelled, 0);
            }

            this.RemoveTripFromVehicle(false, this.FleetManager);


            this.Cancelled = true;
            this.CancelDate = DateTime.Now;
            this.FleetManager = new FleetManager();
            this.FleetManagerID = 0;

            this.CancelCode = CancelCode.CancelID;
            this.CancelReason = CancelCode.CancelReason;
            this.CancelBackgroundColor = CancelCode.BackgroundColor;
            this.NoGo = false;
            rv = true;

            if (!CancelAssociatedLegs)
            {
                return rv;
            }

            this.DummyFields.LI_D_1 = 1;

            if (this is Appointment)
            {
                return rv;
            }

            this.AssociatedLegs.ForEach(t =>
            {
                t.Cancel(CancelCode, false);
                t.Save();
            });

            Logger.Log("Cancelled {0} with reason of {1}. Cancel success = {2}", this.ToString(), CancelCode.CancelReason, rv.ToString());

            return rv;
        }

        List<TripBase> _associatedLegs = null;
        [XmlIgnore]
        public List<TripBase> AssociatedLegs
        {
            get
            {
                if (_associatedLegs != null)
                {
                    return _associatedLegs;
                }

                if (this.AssignedSchedule == null)
                {
                    _associatedLegs = new TripBaseManager().GetAssociatedLegs(this);
                    return _associatedLegs;
                }

                if (this.SubscriptionID != 0)
                {
                    _associatedLegs = this.AssignedSchedule.Trips.Where(t => t.SubscriptionID == this.SubscriptionID).ToList();
                }
                else if (this.AppointmentID != 0)
                {
                    _associatedLegs = this.AssignedSchedule.Trips.Where(t => t.AppointmentID == this.AppointmentID).ToList();
                }

                return _associatedLegs;
            }

        }

        public bool Uncancel()
        {
            if (!Hub.CurrentUser.Permissions.CanCancelTrips)
            {
                return false;
            }

            if (!this.NoGo && !this.Cancelled)
            {
                return false;
            }

            TripScheduleHistoryManager.AddNew(this, TripScheduleHistoryActionType.Unscheduled, 0);

            this.NoGo = false;
            this.Cancelled = false;
            this.CancelReason = string.Empty;
            this.FleetManager = new FleetManager();

            return true;
        }

        /// <summary>
        /// Marks this trip as a no show a reason
        /// </summary>
        /// <param name="CancelReason">The reason for not showing up</param>
        /// <returns>True if successful</returns>
        /// <history>
        ///     [Tim Hibbard]   06/17/2008  Created
        /// </history>
        public bool NoShow(string NoShowReason)
        {
            return NoShow(NoShowReason, false);
        }

        /// <summary>
        /// Marks this trip as a no show a reason
        /// </summary>
        /// <param name="CancelReason">The reason for not showing up</param>
        /// <returns>True if successful</returns>
        /// <history>
        ///     [Tim Hibbard]   05/24/2013  Created
        /// </history>
        public bool NoShow(string NoShowReason, bool FromREST)
        {
            if (DateTime.Now.AddMinutes(15) < this.PickUpDateAndTime && !FromREST)
            {
                return false;
            }
            if (!FromREST)
            {
                TripScheduleHistoryManager.AddNew(this, TripScheduleHistoryActionType.Noshowed, 0);
            }

            this.RemoveTripFromVehicle(false, this.FleetManager);

            this.NoGo = true;
            this.NoGoReason = NoShowReason;
            this.Cancelled = false;
            this.CancelReason = "";

            return true;
            //try
            //{
            //    this.NoGo = true;
            //    this.FleetManager = new FleetManager();
            //    this.Cancelled = false;
            //    this.CancelReason = "";

            //    this.NoGoReason = NoShowReason;
            //    rv = true;
            //}
            //catch (Exception)
            //{
            //    rv = false;
            //}
            //return rv;
        }

        /// <summary>
        /// Marks this trip as a no-show without a reason
        /// </summary>
        /// <returns>True if successfull</returns>
        /// <history>
        ///     [Tim Hibbard]   06/17/2008  Created
        /// </history>
        public bool NoShow()
        {
            return this.NoShow("");
        }


        /// <summary>
        /// Assigns this trip to a route
        /// </summary>
        /// <param name="fm">FleetManager object to assign to</param>
        /// <param name="saveIdentifier">Set this to explicitly describe where the saving object is. This prevents unneeded database calls when updating from cache.</param>
        /// <returns>True if successful</returns>
        /// <history>
        ///     [Tim Hibbard]   06/17/2008  Created
        /// </history>
        public bool Schedule(FleetManager fm, string saveIdentifier)
        {
            return Schedule(fm, false, true, saveIdentifier);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fm"></param>
        /// <param name="FromREST"></param>
        /// <param name="saveIdentifier">Set this to explicitly describe where the saving object is. This prevents unneeded database calls when updating from cache.</param>
        /// <returns></returns>
        public bool Schedule(FleetManager fm, bool FromREST, string saveIdentifier)
        {
            return Schedule(fm, FromREST, true, saveIdentifier);
        }

        /// <summary>
        /// Assigns this trip to a route
        /// </summary>
        /// <param name="fm">FleetManager object to assign to</param>
        /// <param name="saveIdentifier">Set this to explicitly describe where the saving object is. This prevents unneeded database calls when updating from cache.</param>
        /// <returns>True if successful</returns>
        /// <history>
        ///     [Tim Hibbard]   05/24/2013  Created
        /// </history>
        public bool Schedule(FleetManager fm, bool FromREST, bool SendToMDT, string saveIdentifier)
        {
            var rv = false;
            if (!_justAutoScheduledFlag)
            {
                if (!FromREST)
                {
                    if (fm == null || fm.ID == 0)
                    {
                        TripScheduleHistoryManager.AddNew(this, TripScheduleHistoryActionType.Unscheduled, 0);
                    }
                    else
                    {
                        TripScheduleHistoryManager.AddNew(this, TripScheduleHistoryActionType.Scheduled, fm.ID);
                    }
                }

            }
            _justAutoScheduledFlag = false;


            try
            {
                //remove from old fm
                //this.FleetManager.AssignedTrips.Remove(this);

                if (this.NoGo)
                {
                    this.PickUpOdometer = 0;
                }

                this.FlagFleetManagerForVerify(this.FleetManager);
                this.FlagFleetManagerForVerify(fm);
                string oldGPSUnitID = this.FleetManager.Vehicle.GPSUnitID;
                int oldDriverID = this.FleetManager.Driver.DriverID;
                this.FleetManager = fm;

                if (SendToMDT)
                {

                    if (PPRegistryManager.Instance.Mobile)
                    {
                        this.RemoveTripFromAPN(oldDriverID);
                        this.SendTripToAPN();
                    }

                    if (PPRegistryManager.Instance.MDT)
                    {
                        this.SwitchMDT(oldGPSUnitID, saveIdentifier);
                    }
                }

                //add to new fleetmanager



                this.Cancelled = false;
                this.CancelReason = "";
                this.NoGo = false;
                rv = true;
            }
            catch (Exception)
            {
                rv = false;
            }
            return rv;
        }

        private void FlagFleetManagerForVerify(FleetManager fm)
        {
            if (FleetManager.IsNullOrEmpty(fm))
            {
                return;
            }
            fm.NeedsVerifying = true;
        }

        private bool _justAutoScheduledFlag = false;

        public bool Schedule(AutoScheduleTrip.ImpactedRoute ImpactedRoute, string saveIdentifier)
        {
            TripScheduleHistoryManager.AddNew(this, TripScheduleHistoryActionType.Autoscheduled, ImpactedRoute.FleetManager.ID);
            _justAutoScheduledFlag = true;
            var rv = this.Schedule(ImpactedRoute.FleetManager, saveIdentifier);
            if (rv)
            {
                this.PickUpTime = ImpactedRoute.PUTime;
                this.DropOffTime = ImpactedRoute.DOTime;
            }

            return rv;
        }

        #endregion

        #region --Constructors--
        /// <summary>
        /// Default empty constructor
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   06/08/2007  Created
        ///     [Tim Hibbard]   06/08/2007  Added WirePropertyChanged to bubble PropertyChanged events
        /// </history>
        public TripBase() : this(false)
        {

        }

        public TripBase(bool FromDataReader)
        {
            if (!FromDataReader) this.InitializeTrip();
            Filespecs fs = Managers.FileSpecsManager.Instance;

            this.DropOffLocation.City = fs.DefaultCity;
            this.DropOffLocation.State = fs.DefaultState;
            this.PickUpLocation.City = fs.DefaultCity;
            this.PickUpLocation.State = fs.DefaultState;
            this.AppointmentType = fs.DummyFields.TXT_D_7;
            this.ReturnType = fs.gCost4;
            this.TimeType = fs.gTimeType;
            if (fs.DefaultTripDateOffset >= 0)
            {
                this.TripDate = DateTime.Now.Date.AddDays(fs.DefaultTripDateOffset);
            }

            this.TravelTime = fs.maxRT;
        }

        /// <summary>
        /// Constructor that takes a fleetmanager object
        /// </summary>
        /// <param name="fleetManager"></param>
        /// <history>
        ///     [Tim Hibbard]   01/16/2008  Created
        /// </history>
        //public TripBase(FleetManager fleetManager) : this()
        //{
        //    this.IsBuildingSet(true);
        //    this.FleetManager = fleetManager;
        //    this.IsBuildingSet(false);
        //    //this.InitializeTrip();
        //}

        #endregion

        #region --Deconstructors--
        ~TripBase()
        {
            if (this._fleetManager != null)
            {
                this._fleetManager.PropertyChanged -= FleetManager_PropertyChanged;
            }
            if (this._riders != null)
            {
                this._riders.PropertyChanged -= Riders_PropertyChanged;
            }
            if (this._dummyFields != null)
            {
                this._dummyFields.PropertyChanged -= DummyFields_PropertyChanged;
            }
            if (this._tripProgram != null)
            {
                this._tripProgram.PropertyChanged -= _tripProgram_PropertyChanged;
            }
            if (this._subscriptionInformation != null)
            {
                this._subscriptionInformation.PropertyChanged -= SubscriptionInformation_PropertyChanged;
            }
            if (this._driver != null)
            {
                this._driver.PropertyChanged -= Driver_PropertyChanged;
            }
            if (this._pickUpPlace != null)
            {
                this._pickUpPlace.PropertyChanged -= PickUpPlace_PropertyChanged;
                this._pickUpPlace.StatusChanged -= StatusChangedHandler;
            }
            if (this._dropOffPlace != null)
            {
                this._dropOffPlace.PropertyChanged -= DropOffPlace_PropertyChanged;
                this._dropOffPlace.StatusChanged -= StatusChangedHandler;
            }
            if (this._user != null)
            {
                this._user.PropertyChanged -= User_PropertyChanged;
            }
            if (this._client != null)
            {
                this._client.PropertyChanged -= Client_PropertyChanged;
            }
            if (this._stops != null)
            {
                _stops.ForEach(s =>
                {
                    s.Trip = null;
                    s.PropertyChanged -= stop_PropertyChanged;

                });
            }
        }

        #endregion

        #region --Overrides--
        public bool Populate(EntitiesBase obj, bool RaiseEvents)
        {
            bool rv = false;
            var s = this.AssignedSchedule;

            rv = base.Build(obj, RaiseEvents);

            this.AssignedSchedule = s;
            this.IsDirty = false;
            return rv;
        }

        public override void SetDirty(string PropertyName)
        {
            if (PropertyName.StartsWith("FleetManager.", StringComparison.OrdinalIgnoreCase))
            {
                //I'm not sure why this is bypassing the setdirty function, but 
                //I need the dirty fields to reflect the fleetmanager.
                base.AddToDirtyFields(PropertyName);
                return;
            }
            if (PropertyName == "ValidColor")
            {
                //valid color is used internally to decide if a trip should be marked as
                //red on the screen. Usually, it will be called with an actual property that should
                //be saved, but sometimes we will use it to force a repaint. In that case, the object
                //should not be marked as dirty or flag a need to be saved
                base.AddToDirtyFields(PropertyName);
                return;
            }

            if (PropertyName == "SchedulingCanvasToolTip")
            {
                //SchedulingCanvasToolTip generates a string that describes a trip
                //that is displayed in scheduling
                //we are not sure if we want it be be dirty so we are just returning
                //to be continued...
                return;
            }

            base.SetDirty(PropertyName);
        }

        /// <summary>
        /// The visual representation of this object
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   11/18/2008  Created
        /// </history>
        public override System.Drawing.Image DisplayImage
        {
            get
            {
                return ParaPlan.Properties.Resources.ppTrip;
            }
        }

        /// <summary>
        /// The action items associated with this object
        /// </summary>
        /// <history>
        ///     [Tim Hibbard]   10/11/2007  Created
        /// </history>
        [XmlIgnore]
        public override List<ActionItem> ActionItems
        {
            get
            {
                List<ActionItem> rv = new List<ActionItem>();

                ActionItem viewClient = new ActionItem();
                viewClient.ToolTipText = "View client";
                viewClient.DisplayImage = ParaPlan.Properties.Resources.ppClient;
                viewClient.OnClickAction = delegate (EntitiesBase ent)
                {
                    Client client = ((TripBase)ent).Client;
                    return new ActionItemArgs(client);
                };

                rv.Add(viewClient);

                return rv;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.TripDate.ToShortDateString());
            sb.Append(" ");
            sb.Append(this.TripTime.ToShortTimeString());
            sb.Append(" ");
            sb.Append(Client.Name.FullName);
            sb.Append(" ");
            sb.Append(this.PickUpTime.ToShortTimeString());
            sb.Append(" ");
            sb.Append(this.PickUpLocation.DisplayStringShort);
            sb.Append("/");
            sb.Append(this.DropOffTime.ToShortTimeString());
            sb.Append(" ");
            sb.Append(this.DropOffLocation.DisplayStringShort);
            return sb.ToString();
        }

        private int _myHashCode = 0;

        public int MyHashCode
        {
            get
            {
                if (_myHashCode == 0)
                {
                    _myHashCode = this.Client.ClientID + this.ID + this.TripID + (this.TripDate.Month * this.TripDate.Day + this.TripDate.Year);
                }

                return _myHashCode;
            }
        }

        /// <summary>
        /// return base.GetHashCode();
        /// </summary>
        /// <returns>base.GetHashCode();</returns>
        /// <history>
        ///     [Tim Hibbard]   06/01/2007  Created
        /// </history>
        public override int GetHashCode()
        {
            //return base.GetHashCode();
            unchecked
            {
                int hash = 13;
                hash += hash * this.TripID;
                hash += hash * this.ID;
                hash += hash * this.SubscriptionID;
                hash += hash * this.AppointmentID;

                return hash;
            }
        }

        /// <summary>
        /// Checks this object for equality
        /// </summary>
        /// <param name="obj">A TripBase object to check</param>
        /// <returns>True if objects are the same</returns>
        /// <history>
        ///     [Tim Hibbard]   03/13/2007  Created
        ///     [Tim Hibbard]   03/19/2007  Modified to check DummyFields
        /// </history>
        public override bool Equals(object obj)
        {
            bool rv = true;

            if (!(obj is TripBase))
            {
                return false;
            }

            try
            {
                TripBase x = (TripBase)obj;
                if (!(x._fleetManagerID ?? 0).Equals(this._fleetManagerID ?? 0))
                {
                    return false;
                }
                if (base.IsDifferent(x._pickUpOdometer, this._pickUpOdometer))
                {
                    return false;
                }
                if (base.IsDifferent(x._dropOffOdometer, this._dropOffOdometer))
                {
                    return false;
                }
                if (!(x._age ?? 0).Equals(this._age ?? 0))
                {
                    return false;
                }
                if (!(x._ap ?? false).Equals(this._ap ?? false))
                {
                    return false;
                }
                if (!(x._appointmentID ?? 0).Equals(this._appointmentID ?? 0))
                {
                    return false;
                }
                if (!(x._appointmentType ?? string.Empty).Equals(this._appointmentType ?? string.Empty))
                {
                    return false;
                }
                if (!(x._billAmount ?? 0M).Equals(this._billAmount ?? 0M))
                {
                    return false;
                }
                if (!(x._billDate ?? DateTime.MinValue).Equals(this._billDate ?? DateTime.MinValue))
                {
                    return false;
                }
                if (!(x._billed ?? false).Equals(this._billed ?? false))
                {
                    return false;
                }
                if (!(x._billTo ?? string.Empty).Equals(this._billTo ?? string.Empty))
                {
                    return false;
                }
                if (!(x._cancelCode ?? 0).Equals(this._cancelCode ?? 0))
                {
                    return false;
                }
                if (!(x._cancelDate ?? DateTime.MinValue).Equals(this._cancelDate ?? DateTime.MinValue))
                {
                    return false;
                }
                if (!(x._cancelled ?? false).Equals(this._cancelled ?? false))
                {
                    return false;
                }
                if (!(x._cancelReason ?? string.Empty).Equals(this._cancelReason ?? string.Empty))
                {
                    return false;
                }
                if (!(x._checkAmount ?? 0M).Equals(this._checkAmount ?? 0M))
                {
                    return false;
                }
                if (!(x._checkNumber ?? 0).Equals(this._checkNumber ?? 0))
                {
                    return false;
                }
                if (!(x._riders ?? new Riders()).Equals(this._riders ?? new Riders()))
                {
                    return false;
                }
                if (!(x._client ?? new Client()).Equals(this._client ?? new Client()))
                {
                    return false;
                }
                if (!(x._clientPrograms ?? string.Empty).Equals(this._clientPrograms ?? string.Empty))
                {
                    return false;
                }
                //if (!(x._cost1 ?? 0M).Equals(this._cost1 ?? 0M))
                //{
                //    return false;
                //}
                //if (!(x._cost2 ?? 0M).Equals(this._cost2 ?? 0M))
                //{
                //    return false;
                //}
                if (!(x._cost3 ?? 0M).Equals(this._cost3 ?? 0M))
                {
                    return false;
                }
                if (!(x._cost4 ?? string.Empty).Equals(this._cost4 ?? string.Empty))
                {
                    return false;
                }
                if (!(x._dummyFields ?? new DummyFields()).Equals(this._dummyFields ?? new DummyFields()))
                {
                    return false;
                }
                if (!(x._donations ?? 0D).Equals(this._donations ?? 0D))
                {
                    return false;
                }
                if (!(x._driver ?? new Driver()).Equals(this._driver ?? new Driver()))
                {
                    return false;
                }
                if (!(x._dropOffPlace ?? new Place()).Equals(this._dropOffPlace ?? new Place()))
                {
                    return false;
                }
                if (!x._dropOffStamp.TimeEquals(this._dropOffStamp))
                {
                    return false;
                }
                if (!x._dropOffTime.TimeEquals(this._dropOffTime))
                {
                    return false;
                }

                //removing this because two brand new trips would not be equal, and I
                //think they should be
                //if (!(x._entryDate).Equals(this._entryDate))
                //{
                //    return false;
                //}
                if (!(x._excludeFromAutoSchedule ?? false).Equals(this._excludeFromAutoSchedule ?? false))
                {
                    return false;
                }
                if (!(x._fleetManager ?? new FleetManager()).Equals(this._fleetManager ?? new FleetManager()))
                {
                    return false;
                }
                if (!(x._groupID ?? 0).Equals(this._groupID ?? 0))
                {
                    return false;
                }
                if (!(x._id ?? 0).Equals(this._id ?? 0))
                {
                    return false;
                }
                if (!(x._invoiceID ?? string.Empty).Equals(this._invoiceID ?? string.Empty))
                {
                    return false;
                }
                if (!(x._mileage ?? 0D).Equals(this._mileage ?? 0D))
                {
                    return false;
                }
                if (!(x._noGo ?? false).Equals(this._noGo ?? false))
                {
                    return false;
                }
                if (!(x._noGoCode ?? 0).Equals(this._noGoCode ?? 0))
                {
                    return false;
                }
                if (!(x._noGoReason ?? string.Empty).Equals(this._noGoReason ?? string.Empty))
                {
                    return false;
                }
                if (!(x._paid ?? false).Equals(this._paid ?? false))
                {
                    return false;
                }
                if (!(x._payDate ?? DateTime.MinValue).Equals(this._payDate ?? DateTime.MinValue))
                {
                    return false;
                }
                if (!(x._payee ?? string.Empty).Equals(this._payee ?? string.Empty))
                {
                    return false;
                }
                if (!(x._pickUpPlace ?? new Place()).Equals(this._pickUpPlace ?? new Place()))
                {
                    return false;
                }
                if (!x._pickUpStamp.TimeEquals(this._pickUpStamp))
                {
                    return false;
                }
                if (!x._pickUpTime.TimeEquals(this._pickUpTime))
                {
                    return false;
                }
                if (!x._returnTime.TimeEquals(this._returnTime))
                {
                    return false;
                }
                if (!(x._returnType ?? string.Empty).Equals(this._returnType ?? string.Empty))
                {
                    return false;
                }
                if (!(x._scheduledByAutoSchedule ?? false).Equals(this._scheduledByAutoSchedule ?? false))
                {
                    return false;
                }
                if (!(x._subscriptionID ?? 0).Equals(this._subscriptionID ?? 0))
                {
                    return false;
                }
                if (!(x._subscriptionInformation ?? new SubscriptionInformation()).Equals(this._subscriptionInformation ?? new SubscriptionInformation()))
                {
                    return false;
                }
                if (!(x._tickets ?? 0D).Equals(this._tickets ?? 0D))
                {
                    return false;
                }
                if (!(x._timeType ?? string.Empty).Equals(this._timeType ?? string.Empty))
                {
                    return false;
                }
                if (!(x._toDrop ?? string.Empty).Equals(this._toDrop ?? string.Empty))
                {
                    return false;
                }
                if (!(x._toPick ?? string.Empty).Equals(this._toPick ?? string.Empty))
                {
                    return false;
                }
                if (base.IsDifferent(x._travelDistance, this._travelDistance))
                {
                    return false;
                }
                if (base.IsDifferent(x._travelTime, this._travelTime))
                {
                    return false;
                }
                if (!(x._tripComment ?? string.Empty).Equals(this._tripComment ?? string.Empty))
                {
                    return false;
                }
                if (!(x._tripDate.Date).Equals(this._tripDate.Date))
                {
                    return false;
                }
                if (!(x._tripID ?? 0).Equals(this._tripID ?? 0))
                {
                    return false;
                }
                if (!(x._tripProgram ?? new ClientProgram()).Equals(this._tripProgram ?? new ClientProgram()))
                {
                    return false;
                }
                if (!x._tripTime.TimeEquals(this._tripTime))
                {
                    return false;
                }
                if (!(x._tripType ?? string.Empty).Equals(this._tripType ?? string.Empty))
                {
                    return false;
                }
                if (!x._TWa.TimeEquals(this._TWa))
                {
                    return false;
                }
                if (!x._TWb.TimeEquals(this._TWb))
                {
                    return false;
                }
                if (!x._TWc.TimeEquals(this._TWc))
                {
                    return false;
                }
                if (!x._TWd.TimeEquals(this._TWd))
                {
                    return false;
                }
                if (!(x._user ?? new User()).Equals(this._user ?? new User()))
                {
                    return false;
                }
                //if (!x._vehicle.Equals(this._vehicle))
                //{
                //    return false;
                //}
                if (!(x._went ?? false).Equals(this._went ?? false))
                {
                    return false;
                }
                if (!(x._wp ?? false).Equals(this._wp ?? false))
                {
                    return false;
                }
                if (base.IsDifferent(x._tripBilling, this._tripBilling))
                {
                    return false;
                }
                if (base.IsDifferent(x._brokerTripID, this._brokerTripID))
                {
                    return false;
                }
                if (base.IsDifferent(x._brokerClientID, this._brokerClientID))
                {
                    return false;
                }
                if (base.IsDifferent(x._brokerTripStatus, this._brokerTripStatus))
                {
                    return false;
                }



            }
            catch (Exception)
            {
                rv = false;
            }
            return rv;
        }

        #endregion



        #region --Validation--
        private DateTime TripTime1159()
        {
            return new DateTime(this.TripTime.Year, this.TripTime.Month, this.TripTime.Day, 23, 59, 0);
        }

        private DateTime TripTime1201()
        {
            return new DateTime(this.TripTime.Year, this.TripTime.Month, this.TripTime.Day, 0, 1, 0);
        }

        /// <summary>
        /// Generates the rules for this class
        /// </summary>
        /// <returns>Generic list of rules</returns>
        /// <history>
        ///     [Tim Hibbard]   04/02/2008  Created
        ///     [Tim Hibbard]   04/10/2008  Modified to append base rules after initial rules have been set
        ///     [Tim Hibbard]   06/13/2008  Added a check to see if the PU and DO locations are the same
        ///     [Tim Hibbard]   07/09/2009  Added a check to make sure ReturnTime is after TripTime
        /// </history>
        protected override List<Rule> CreateRules()
        {
            var rules = new List<Rule>();

            rules.Add(new TimeMustBePopulatedRule("ReturnTime",
                                                  delegate
                                                  {
                                                      var rv = true;
                                                      if (this.ReturnType == "Roundtrip: Pick up at ->" && !this.ReturnTime.IsMinValue())
                                                      {
                                                          rv = this.ReturnTime.TimeOfDay != DateTime.MinValue.TimeOfDay;
                                                      }
                                                      return rv;
                                                  }));
            rules.Add(new SimpleRule("ReturnTime", "Return Time must be after Trip Time",
                                     delegate
                                     {
                                         return (this.ReturnType == "Roundtrip: Pick up at ->" && !this.ReturnTime.IsMinValue()) ?
                                                this.ReturnTime.TimeOfDay >= this.TripTime.TimeOfDay :
                                                true;
                                     }));
            rules.Add(new SimpleRule("DropOffPlace", "Pick up and drop off location must be different",
                                     delegate
                                     {
                                         return !(this.PickUpLocation.AddressEquals(this.DropOffLocation));
                                     }));
            rules.Add(new SimpleRule("PickUpPlace", "Pick Up Location must be valid",
                                     () =>
                                     {
                                         return this.PickUpLocation.IsCompleteForMapping;
                                     }));
            rules.Add(new SimpleRule("DropOffPlace", "Drop Off Location must be valid",
                                     () =>
                                     {
                                         return this.DropOffLocation.IsCompleteForMapping;
                                     }));
            rules.Add(new SimpleRule("TripProgram", "Trip must have associated program",
                                     delegate
                                     {
                                         return !(string.IsNullOrEmpty(this.TripProgram.ProgramName));
                                     }));
            rules.Add(new SimpleRule("TripDate", "Date is not valid",
                                     delegate
                                     {
                                         return !(this.TripDate.IsMinValue());
                                     }));

            rules.Add(new SimpleRule("PickUpTime", string.Format("Pick Up Time must be between Time Windows ({0} - {1})", this.TWa.ToShortTimeString(), this.TWb.ToShortTimeString()), //"Pick Up Time must be between Time Windows",
                                     delegate
                                     {
                                         if (this.TripDate.Date < DateTime.Now.Date)
                                         {
                                             return true;
                                         }
                                         //hasn't been set...no point evaluating or flooding the user with errors
                                         if (this.PickUpTime.IsMinValue())
                                         {
                                             return true;
                                         }
                                         if (this.TripTime >= this.TripTime1159())
                                         {
                                             return true;
                                         }
                                         if (this.TripTime < this.TripTime1201())
                                         {
                                             return true;
                                         }
                                         if (this.IsFlex)
                                         {
                                             return true;
                                         }
                                         //4/4/13 - Did add minutes to either side of PickUpTime and removed = to account for seconds
                                         return this.PickUpTime.AddMinutes(1).TimeOfDay > this.TWa.TimeOfDay && this.PickUpTime.AddMinutes(-1).TimeOfDay < this.TWb.TimeOfDay;
                                     }));

            rules.Add(new SimpleRule("DropOffTime", string.Format("Drop Off Time must be between Time Windows ({0} - {1})", this.TWc.ToShortTimeString(), this.TWd.ToShortTimeString()),  //"Drop Off Time must be between Time Windows",
                                     delegate
                                     {
                                         if (this.TripDate.Date < DateTime.Now.Date)
                                         {
                                             return true;
                                         }
                                         //hasn't been set...no point evaluating or flooding the user with errors
                                         if (this.DropOffTime.IsMinValue())
                                         {
                                             return true;
                                         }
                                         if (this.TripTime >= this.TripTime1159())
                                         {
                                             return true;
                                         }
                                         if (this.TripTime < this.TripTime1201())
                                         {
                                             return true;
                                         }
                                         if (this.IsFlex)
                                         {
                                             return true;
                                         }
                                         //4/4/13 - Did add minutes to either side of DropOffTime and removed = to account for seconds
                                         var rv = this.DropOffTime.AddMinutes(1).TimeOfDay > this.TWc.TimeOfDay && this.DropOffTime.AddMinutes(-1).TimeOfDay < this.TWd.TimeOfDay;
                                         return rv;
                                     }));

            rules.Add(new SimpleRule("PickUpTime", "Pick Up Time must be before Drop Off Time",
                                     () =>
                                     {
                                         //if (FileSpecsManager.Instance.DefaultReturnTime == DefaultReturnTime.ElevenFiftyNinePM &&
                                         //    this.TripTime.TimeEquals(DateTime.Parse("11:59 PM")))
                                         //{
                                         //    //this is a return trip of an unknown callback
                                         //    return true;
                                         //}
                                         if (this.TripTime >= this.TripTime1159())
                                         {
                                             return true;
                                         }
                                         if (this.TripTime < this.TripTime1201())
                                         {
                                             return true;
                                         }
                                         return this.PickUpTime.IsMinValue() ? true : this.PickUpTime.TimeOfDay < this.DropOffTime.TimeOfDay;
                                     }));

            rules.Add(new SimpleRule("PickUpTime", "Pick Up Time must be after start time of assigned route",
                                     delegate
                                     {
                                         if (this.TripDate.Date < DateTime.Now.Date)
                                         {
                                             return true;
                                         }
                                         if (this.FleetManager.Schedule == null)
                                         {
                                             return true;
                                         }
                                         return this.FleetManager.ID == 0 || this.PickUpTime.IsMinValue() ?
                                                true :
                                                this.PickUpTime.TimeOfDay >= this.FleetManager.WorkDayHours.Start.TimeOfDay;
                                     }));

            rules.Add(new SimpleRule("FleetManager", "Client must be on wheelchair equiped vehicle",
                                     () =>
                                     {
                                         if (this.TripDate.Date < DateTime.Now.Date)
                                         {
                                             return true;
                                         }
                                         if (this.FleetManager.Schedule == null)
                                         {
                                             return true;
                                         }
                                         if (!this.IsScheduled)
                                         {
                                             return true;
                                         }
                                         if (this.FleetManager.Vehicle.VehicleID == 0)
                                         {
                                             return true;
                                         }
                                         if (!this.Client.SpecialNeeds.HasWheelChair && !this.Client.SpecialNeeds.NeedsWheelChairVan)
                                         {
                                             return true;
                                         }

                                         return this.FleetManager.Vehicle.FixedWheelchairSpots > 0;
                                     }));

            rules.Add(new SimpleRule("PickUpTime", "Pick Up Time must be before end time of assigned route",
                                     delegate
                                     {
                                         if (this.TripDate.Date < DateTime.Now.Date)
                                         {
                                             return true;
                                         }
                                         if (this.FleetManager.Schedule == null)
                                         {
                                             return true;
                                         }
                                         return this.FleetManager.ID == 0 || this.PickUpTime.IsMinValue() ?
                                                true :
                                                this.PickUpTime.TimeOfDay <= this.FleetManager.WorkDayHours.End.TimeOfDay;
                                     }));
            //rules.Add(new SimpleRule("Riders", "More riders than vehicle can handle", 
            //                         delegate
            //                         { 
            //                             var rv = true; 
            //                             if (this.FleetManager.Vehicle.VehicleID != 0) 
            //                             { 
            //                                 rv = this.FleetManager.Vehicle.FixedAmbulatorySeats >= this.Riders.TotalRiders; 
            //                             }
            //                             return rv; 
            //                         }));
            rules.Add(new SimpleRule("Riders", "Vehicle Seat Capacities Exceeded",
                                     delegate
                                     {
                                         //todo: decide if I should be a rule or not
                                         return true;
                                         if (this.FleetManager == null)
                                         {
                                             return true;
                                         }
                                         //set convertable seats for testing
                                         //var prevCount = 0;
                                         //if (this.FleetManager.DirtyFields != null)
                                         //{
                                         //    prevCount = this.FleetManager.DirtyFields.Count;
                                         //}
                                         //this.FleetManager.Vehicle.ConvertableSeats = 2;
                                         //if (prevCount == 0 && this.FleetManager.DirtyFields.Count == 1)
                                         //{
                                         //    this.FleetManager.IsDirty = false;
                                         //}
                                         if (this.FleetManager.Vehicle.ConvertibleSeats == -1)
                                         {
                                             return true;
                                         }
                                         if (this.TripDate.Date < DateTime.Now.Date)
                                         {
                                             return true;
                                         }
                                         if (this.FleetManager.Schedule == null)
                                         {
                                             return true;
                                         }
                                         if (this.Stops.Count == 0)
                                         {
                                             return true;
                                         }

                                         if (this.FleetManager.Vehicle.VehicleID != 0)
                                         {
                                             bool overCapacity = false;
                                             DateTime stopTime = DateTime.MinValue;
                                             var stops = this.FleetManager.AssignedStops;

                                             Console.WriteLine("Checking Capacity {0} {1}", this.ToString(), string.Join(",", this.DirtyFields));

                                             foreach (var stop in stops)
                                             {
                                                 stopTime = DateTime.Parse(this.FleetManager.WorkDate.ToShortDateString() + " " + stop.StopTime.ToShortTimeString());
                                                 if (stopTime >= this.PickUpDateAndTime && stopTime < this.DropOffDateAndTime && stop.StopTypeShort == "PU")
                                                 {
                                                     overCapacity = this.FleetManager.IsOverCapacity(stopTime);
                                                 }
                                             }
                                             //we only want to check our neighbors if we are over capacity or we are 
                                             //making a change that requests valid color refresh
                                             if ((overCapacity || this.DirtyFields.Contains("ValidColor"))
                                                 && !Hub.TripIsCheckingNeighborsForValidity
                                                 && this.FleetManager.DirtyFields.Count == 0
                                                 && !Hub.SchedulingCanvasIsLoading
                                                 && this.TripID != 0)
                                             {
                                                 Hub.TripIsCheckingNeighborsForValidity = true;
                                                 foreach (var t in this.FleetManager.AssignedTrips)
                                                 {
                                                     if (t.TripID == this.TripID)
                                                     {
                                                         continue;
                                                     }
                                                     t.OnPropertyChanged("ValidColor");
                                                     t.OnPropertyChanged("SchedulingCanvasToolTip");


                                                 }
                                                 Hub.TripIsCheckingNeighborsForValidity = false;
                                             }
                                             if (overCapacity) return false;
                                         }
                                         return true;
                                     }));



            //rules.Add(new SimpleRule("Riders", "This client requires a PCA",
            //    () =>
            //    {
            //        return this.Client.SpecialNeeds.PersonalCareAttendant ? this.Riders.PCA == true : true;
            //    }));

            //rules.Add(new SimpleRule("Riders", "Extra riders must be more than zero when Client has a PCA",
            //    delegate
            //    {
            //        var rv = true;
            //        if (this.Client.SpecialNeeds.PersonalCareAttendant)
            //        {
            //            rv = this.Riders.Other > 0;
            //        }
            //        return rv;
            //    }));
            rules.AddRange(base.CreateRules());

            return rules;
        }
        #endregion
    }
}
