import { useEffect, useState } from "react";
import * as signalR from "@microsoft/signalr";
import { Chart } from "react-google-charts";

let connection = new signalR.HubConnectionBuilder()
   .configureLogging(signalR.LogLevel.Debug)
   .withUrl("http://localhost:5001/covid-hub")
   .build();

const columnNames = [
   "Tarih",
   "Istanbul",
   "Ankara",
   "Izmir",
   "Antalya",
   "Manisa",
];

function App() {
   const [data, setData] = useState([]);
   useEffect(() => {
      connection.start().then((_) => {
         connection.invoke("GetCovidBilgi");
      });
   }, []);
   connection.on("ReceiveCovidBilgi", (result) => {
      let covidBilgi = [];
      result.forEach((item) => {
         covidBilgi.push([
            item.tarih,
            item.sayilar[0],
            item.sayilar[1],
            item.sayilar[2],
            item.sayilar[3],
            item.sayilar[4],
         ]);
      });
      setData([columnNames, ...covidBilgi]);
   });

   return (
      <Chart
         chartType="LineChart"
         data={data}
         width="100%"
         height="400px"
         legendToggle
      />
   );
}

export default App;
