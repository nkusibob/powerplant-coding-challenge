using NUnit.Framework;
using PowerChallengeBusiness.Models;
using PowerChallengeBusiness.Operations;
using powerplant_coding_challenge.SwaggerRequest;
using System.Collections.Generic;
using System.Linq;

namespace PowerPlantChallengetest
{
    public class PowerOperationTest
    {
        private IEnumerable<Power> resList;
        private IEnumerable<Power> resList1;
        private IEnumerable<Power> resList2;
        private IEnumerable<Power> resList3;
        private IEnumerable<Power> resList4;



        [Test]
        public void GetPower()
        {
            //Arrange

            SwaggerPayLoadExample payLoadExample = new SwaggerPayLoadExample();
            Payload payload = payLoadExample.GetExamples();

            MockPayLoadExample1 mockExample1 = new MockPayLoadExample1();
            Payload payload1 = mockExample1.GetExamples();

            MockPayLoadExample2 mockExample2 = new MockPayLoadExample2();
            Payload payload2 = mockExample2.GetExamples();

            MockPayLoadExample3 mockExample3 = new MockPayLoadExample3();
            Payload payload3 = mockExample3.GetExamples();

            MockPayLoadExample4 mockExample4 = new MockPayLoadExample4();
            Payload payload4 = mockExample4.GetExamples();

            decimal expectedPayload = payload.Load;
            decimal expectedPayload1 = payload1.Load;
            decimal expectedPayload2 = payload2.Load;
            decimal expectedPayload3 = payload3.Load;
            decimal expectedPayload4 = payload4.Load;


            PowerOperation pwrOp = new PowerOperation();
            FuelCostOperation _fuelOp = new FuelCostOperation();

            //Act

            resList = pwrOp.GetPower(payload, _fuelOp);
            resList1 = pwrOp.GetPower(payload1, _fuelOp);
            resList2 = pwrOp.GetPower(payload2, _fuelOp);
            resList3 = pwrOp.GetPower(payload3, _fuelOp);
            resList4 = pwrOp.GetPower(payload4, _fuelOp);




            decimal itemSum  = resList.Sum(x => x.PowerResult);
            decimal itemSum1 = resList1.Sum(x => x.PowerResult);
            decimal itemSum2 = resList2.Sum(x => x.PowerResult);
            decimal itemSum3 = resList3.Sum(x => x.PowerResult);
            decimal itemSum4 = resList4.Sum(x => x.PowerResult);



            //Assert

            Assert.AreEqual(expectedPayload, itemSum);
            Assert.AreEqual(expectedPayload1, itemSum1);
            Assert.AreEqual(expectedPayload2, itemSum2);
            Assert.AreEqual(expectedPayload3, itemSum3);
            Assert.AreEqual(expectedPayload4, itemSum4);



        }
        private IEnumerable<Power>  AdjustPmin(IEnumerable<Power> list,List<PowerPlant> powerPlants)
        {
            foreach (var item in list)
            {
                if (powerPlants.Any(x => x.Name.Equals(item.PowerPlantName)))
                {
                    item.PowerResult += powerPlants.Where(x => x.Name == item.PowerPlantName).FirstOrDefault().Pmin;
                }
            }
            return list;
        }



    }
}