using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using NUnit.Framework;

namespace Assets.Test.Editor
{
    class CartControlTest
    {
        private GameObject Cart;
        private CartControl CC;

        [SetUp]
        public void Setup()
        {
            Cart = new GameObject();
            Cart.transform.position = new Vector3(0, 0, 0);
            Cart.AddComponent<Rigidbody>();
            CC = Cart.AddComponent<CartControl>();
            CC.Player = new GameObject();
            CC.CartFric = 0.5f;
            CC.CartRot = 0.5f;
            CC.InteractionRadius = 1f;
            CC.MaxCos = 0.8f;
            CC.Init();
        }

        [TearDown]
        public void cleanup()
        {
        }

        [Test]
        public void InitTest()
        {
            Assert.True(CC.InitialY == 0f && CC.PlayerRot == -1);
        }

        [Test]
        public void OutOfRangeTest()
        {
            CC.Player.transform.position = new Vector3(CC.InteractionRadius * 2, 0, 0);
            Assert.False(CC.PlayerIsInRange());
        }

        [Test]
        public void InRangeTest()
        {
            CC.Player.transform.position = new Vector3(CC.InteractionRadius / 2, 0, 0);
            Assert.True(CC.PlayerIsInRange());
        }

        [Test]
        public void NotBehindTest()
        {
            CC.Player.transform.position = -Cart.transform.right;
            Assert.False(CC.PlayerIsBehind());
        }

        [Test]
        public void BehindTest()
        {
            CC.Player.transform.position = Cart.transform.right;
            //System.Diagnostics.Debug.WriteLine(CC.Player.transform.position.ToString());
            Assert.True(CC.PlayerIsBehind());
        }

        [Test]
        public void CartFricTest()
        {
            Cart.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);
            Cart.GetComponent<Rigidbody>().angularVelocity = new Vector3(1, 0, 0);
            CC.ApplyFriction();
            Assert.True(Cart.GetComponent<Rigidbody>().velocity.x == CC.CartFric);
        }

        [Test]
        public void CartRotTest()
        {
            Cart.GetComponent<Rigidbody>().velocity = new Vector3(1, 0, 0);
            Cart.GetComponent<Rigidbody>().angularVelocity = new Vector3(1, 0, 0);
            CC.ApplyFriction();
            Assert.True(Cart.GetComponent<Rigidbody>().angularVelocity.x == CC.CartRot);
        }

        [Test]
        public void MoveCartTest()
        {
            CC.Player.transform.eulerAngles = new Vector3(0, -1, 0);
            CC.Player.transform.rotation = new Quaternion(1, 2, 3, 0);

            CC.MoveCart();
            Assert.True(Cart.GetComponent<Rigidbody>().velocity == CC.Player.transform.forward * CC.InteractionRadius);
        }

    }
}
