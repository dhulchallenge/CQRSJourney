<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CarRentalsCloudServices" generation="1" functional="0" release="0" Id="66e2420f-a88c-4824-bda2-062a03c8c40b" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CarRentalsCloudServicesGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="CarRentalsAPI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/LB:CarRentalsAPI:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="CarRentalsAPI:DbContext.BlobStorage" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:DbContext.BlobStorage" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:DbContext.booking" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:DbContext.booking" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:DbContext.SqlBus" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:DbContext.SqlBus" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:Diagnostics.LogLevelFilter" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:Diagnostics.LogLevelFilter" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:Diagnostics.PerformanceCounterSampleRate" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:Diagnostics.PerformanceCounterSampleRate" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:Diagnostics.ScheduledTransferPeriod" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:Diagnostics.ScheduledTransferPeriod" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:InstrumentationEnabled" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:InstrumentationEnabled" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:MaintenanceMode" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:MaintenanceMode" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPIInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapCarRentalsAPIInstances" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:DbContext.BlobStorage" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:DbContext.BlobStorage" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:DbContext.booking" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:DbContext.booking" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:DbContext.BookingProcess" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:DbContext.BookingProcess" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:DbContext.CarRentals" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:DbContext.CarRentals" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:DbContext.EventStore" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:DbContext.EventStore" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:DbContext.MessageLog" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:DbContext.MessageLog" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:DbContext.SqlBus" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:DbContext.SqlBus" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:Diagnostics.LogLevelFilter" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:Diagnostics.LogLevelFilter" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:Diagnostics.PerformanceCounterSampleRate" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:Diagnostics.PerformanceCounterSampleRate" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:Diagnostics.ScheduledTransferPeriod" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:Diagnostics.ScheduledTransferPeriod" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:InstrumentationEnabled" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:InstrumentationEnabled" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:MaintenanceMode" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:MaintenanceMode" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessorInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/MapWorkerRoleCommandProcessorInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:CarRentalsAPI:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCarRentalsAPI:DbContext.BlobStorage" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/DbContext.BlobStorage" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:DbContext.booking" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/DbContext.booking" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:DbContext.SqlBus" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/DbContext.SqlBus" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:Diagnostics.LogLevelFilter" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/Diagnostics.LogLevelFilter" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:Diagnostics.PerformanceCounterSampleRate" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/Diagnostics.PerformanceCounterSampleRate" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:Diagnostics.ScheduledTransferPeriod" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/Diagnostics.ScheduledTransferPeriod" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:InstrumentationEnabled" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/InstrumentationEnabled" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:MaintenanceMode" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/MaintenanceMode" />
          </setting>
        </map>
        <map name="MapCarRentalsAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapCarRentalsAPIInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPIInstances" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:DbContext.BlobStorage" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/DbContext.BlobStorage" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:DbContext.booking" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/DbContext.booking" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:DbContext.BookingProcess" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/DbContext.BookingProcess" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:DbContext.CarRentals" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/DbContext.CarRentals" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:DbContext.EventStore" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/DbContext.EventStore" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:DbContext.MessageLog" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/DbContext.MessageLog" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:DbContext.SqlBus" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/DbContext.SqlBus" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:Diagnostics.LogLevelFilter" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/Diagnostics.LogLevelFilter" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:Diagnostics.PerformanceCounterSampleRate" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/Diagnostics.PerformanceCounterSampleRate" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:Diagnostics.ScheduledTransferPeriod" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/Diagnostics.ScheduledTransferPeriod" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:InstrumentationEnabled" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/InstrumentationEnabled" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:MaintenanceMode" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/MaintenanceMode" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessor/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessorInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessorInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="CarRentalsAPI" generation="1" functional="0" release="0" software="D:\Tesco\CQRSJourney\CarRentalsCloudServices\csx\Debug\roles\CarRentalsAPI" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="DbContext.BlobStorage" defaultValue="" />
              <aCS name="DbContext.booking" defaultValue="" />
              <aCS name="DbContext.SqlBus" defaultValue="" />
              <aCS name="Diagnostics.LogLevelFilter" defaultValue="" />
              <aCS name="Diagnostics.PerformanceCounterSampleRate" defaultValue="" />
              <aCS name="Diagnostics.ScheduledTransferPeriod" defaultValue="" />
              <aCS name="InstrumentationEnabled" defaultValue="" />
              <aCS name="MaintenanceMode" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;CarRentalsAPI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CarRentalsAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRoleCommandProcessor&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPIInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPIUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPIFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WorkerRoleCommandProcessor" generation="1" functional="0" release="0" software="D:\Tesco\CQRSJourney\CarRentalsCloudServices\csx\Debug\roles\WorkerRoleCommandProcessor" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="DbContext.BlobStorage" defaultValue="" />
              <aCS name="DbContext.booking" defaultValue="" />
              <aCS name="DbContext.BookingProcess" defaultValue="" />
              <aCS name="DbContext.CarRentals" defaultValue="" />
              <aCS name="DbContext.EventStore" defaultValue="" />
              <aCS name="DbContext.MessageLog" defaultValue="" />
              <aCS name="DbContext.SqlBus" defaultValue="" />
              <aCS name="Diagnostics.LogLevelFilter" defaultValue="" />
              <aCS name="Diagnostics.PerformanceCounterSampleRate" defaultValue="" />
              <aCS name="Diagnostics.ScheduledTransferPeriod" defaultValue="" />
              <aCS name="InstrumentationEnabled" defaultValue="" />
              <aCS name="MaintenanceMode" defaultValue="" />
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRoleCommandProcessor&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CarRentalsAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRoleCommandProcessor&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessorInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessorUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/WorkerRoleCommandProcessorFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="CarRentalsAPIUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyUpdateDomain name="WorkerRoleCommandProcessorUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="CarRentalsAPIFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyFaultDomain name="WorkerRoleCommandProcessorFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="CarRentalsAPIInstances" defaultPolicy="[1,1,1]" />
        <sCSPolicyID name="WorkerRoleCommandProcessorInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="01daba95-67dd-4e6f-8e3d-8bf4173c4cf5" ref="Microsoft.RedDog.Contract\ServiceContract\CarRentalsCloudServicesContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="37cc0cce-3477-47d6-896b-81677e8261fc" ref="Microsoft.RedDog.Contract\Interface\CarRentalsAPI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CarRentalsCloudServices/CarRentalsCloudServicesGroup/CarRentalsAPI:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>