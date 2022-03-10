#power plant code challenge
from this repo :https://github.com/gem-spaas/powerplant-coding-challenge
* choose powerplant-coding-challenge and powerplantWebSocket as Startup Projects and run it 
* build the solution and run PowerPlantChallengetest and it should pass ! 
* start the solution and  it  will launch a swagger page on port 8888 and websocket
* click on try it out 
* execute to display to result in swagger 
* a Web socket server will then display the payload and the list of powerplant to match it


the unit tests are based on those 3 payload examples,
for this same list of powerplants 
```javascript
"powerplants": [
    {
      "name": "gasfiredbig1",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 100,
      "pmax": 460
    },
    {
      "name": "gasfiredbig2",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmin": 100,
      "pmax": 460
    },
    {
      "name": "gasfiredsomewhatsmaller",
      "type": "gasfired",
      "efficiency": 0.37,
      "pmin": 40,
      "pmax": 210
    },
    {
      "name": "tj1",
      "type": "turbojet",
      "efficiency": 0.3,
      "pmin": 0,
      "pmax": 16
    },
    {
      "name": "windpark1",
      "type": "windturbine",
      "efficiency": 1,
      "pmin": 0,
      "pmax": 150
    },
    {
      "name": "windpark2",
      "type": "windturbine",
      "efficiency": 1,
      "pmin": 0,
      "pmax": 36
    }
    ]

```
you can keep the same powerplant list and parameter the load and the wind for each request
in other words you have 4 test plan here!
```javascript



{"
   load": 480,
  "fuels":
  {
    "gas": 13.4,
    "kerosine": 50.8,
    "co2": 20,
    "wind": 60
  },
  
  {
  "load": 480,
  "fuels":
  {
    "gas": 13.4,
    "kerosine": 50.8,
    "co2": 20,
    "wind": 0
  },

  {
  "load": 910,
  "fuels":
  {
    "gas": 13.4,
    "kerosine": 50.8,
    "co2": 20,
    "wind": 60
  },

  {
  "load": 11,
  "fuels":
  {
    "gas": 13.4,
    "kerosine": 50.8,
    "co2": 20,
    "wind": 0
  }
```

but for the last test you can use only 2 powerplants instead of the initial list of 6 powerplants !
```javascript
{
  "load": 500,
  "fuels": {
    "gas": 13.4,
    "kerosine": 50.8,
    "co2": 20,
    "wind": 60
  },
  "powerplants": [
    {
      "name": "gasfiredbig1",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmax": 460,
      "pmin": 100
    },
    {
      "name": "gasfiredbig2",
      "type": "gasfired",
      "efficiency": 0.53,
      "pmax": 460,
      "pmin": 100
    }
   
  ]
}
