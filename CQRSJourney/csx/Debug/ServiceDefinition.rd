<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CQRSJourney" generation="1" functional="0" release="0" Id="38700e83-f30e-4b42-a2d2-9f5f62b467cf" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CQRSJourneyGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="CarRentalsAPI:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CQRSJourney/CQRSJourneyGroup/LB:CarRentalsAPI:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="CarRentalsAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CQRSJourney/CQRSJourneyGroup/MapCarRentalsAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="CarRentalsAPIInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CQRSJourney/CQRSJourneyGroup/MapCarRentalsAPIInstances" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/CQRSJourney/CQRSJourneyGroup/MapWorkerRoleCommandProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WorkerRoleCommandProcessorInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CQRSJourney/CQRSJourneyGroup/MapWorkerRoleCommandProcessorInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:CarRentalsAPI:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CQRSJourney/CQRSJourneyGroup/CarRentalsAPI/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapCarRentalsAPI:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CQRSJourney/CQRSJourneyGroup/CarRentalsAPI/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapCarRentalsAPIInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CQRSJourney/CQRSJourneyGroup/CarRentalsAPIInstances" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessor:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/CQRSJourney/CQRSJourneyGroup/WorkerRoleCommandProcessor/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWorkerRoleCommandProcessorInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CQRSJourney/CQRSJourneyGroup/WorkerRoleCommandProcessorInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="CarRentalsAPI" generation="1" functional="0" release="0" software="D:\Tesco\CQRSJourney\CQRSJourney\csx\Debug\roles\CarRentalsAPI" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;CarRentalsAPI&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CarRentalsAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRoleCommandProcessor&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CQRSJourney/CQRSJourneyGroup/CarRentalsAPIInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CQRSJourney/CQRSJourneyGroup/CarRentalsAPIUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CQRSJourney/CQRSJourneyGroup/CarRentalsAPIFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
        <groupHascomponents>
          <role name="WorkerRoleCommandProcessor" generation="1" functional="0" release="0" software="D:\Tesco\CQRSJourney\CQRSJourney\csx\Debug\roles\WorkerRoleCommandProcessor" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WorkerRoleCommandProcessor&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;CarRentalsAPI&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;r name=&quot;WorkerRoleCommandProcessor&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CQRSJourney/CQRSJourneyGroup/WorkerRoleCommandProcessorInstances" />
            <sCSPolicyUpdateDomainMoniker name="/CQRSJourney/CQRSJourneyGroup/WorkerRoleCommandProcessorUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/CQRSJourney/CQRSJourneyGroup/WorkerRoleCommandProcessorFaultDomains" />
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
    <implementation Id="425551ea-4a88-4370-87ab-3895d0759f14" ref="Microsoft.RedDog.Contract\ServiceContract\CQRSJourneyContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="347a7a26-7132-4f31-a03d-6f298497dbb0" ref="Microsoft.RedDog.Contract\Interface\CarRentalsAPI:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/CQRSJourney/CQRSJourneyGroup/CarRentalsAPI:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>