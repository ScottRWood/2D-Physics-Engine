using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using NEA_Physics_Engine.Physics.Properties;
using NEA_Physics_Engine.Input;
using NEA_Physics_Engine.Rendering;
using NEA_Physics_Engine.Utilities;
using NEA_Physics_Engine.Physics;

namespace NEA_Physics_Engine.Physics
{
    class PhysicsManager : Interfaces.Updateable
    {
        //Attributes
        //my_CollisionListeners (CollisionInfoDelegate): objects checking for collisions
        //my_CollisionFunctionParameters (IsCollidingDelegate): objects to be passed to the collision functions
        //my_ResolveFunctionParameters (CollisionResolveDelegate): objects to be passed to the collision resolution function
        //my_Bodies (List of PhysicsBody): list of physics bodies in the scene
        //my_Gravity (Vector2): the gravity vector
        //my_GravityNormal (Vector2): vector perpendicular to gravity
        //bodySelected (PhysicsBody): object currently selected (used for testing purposes)
        //showDebug (Boolean): boolean used to determine whether to display debug information
        //lastMousePosition (Vector2): the position of the mouse on the last update
        //my_InputManager (InputManager): an instance of the input manager
        
        //Methods
        //RegisterListener: add an element to my_CollisionListeners
        //UnregisterListener: remove an element from my_CollisionListeners
        //Update: run on update to check for collisions and resolve them
        //AddBody: add a physics body to my_Bodies
        //Clear: clear my_Bodies
        //isColliding: check whether two objects are colliding
        //isCollidingCircleCircle: check whether two circles are colliding
        //isCollidingConvexConvex: check whether two polygons are colliding
        //isCollidingConvexCircle: check whether a polygon and a circle are colliding
        //isCollidingCircleConvex: check whether a circle and a polygon are colliding
        //ResolveSolid: resolves collisions between two solid objects
        //Gravity: get/set my_Gravity
        
        //Projection structure used for tracking possible collisions
        private struct Projection
        {
            public Projection(float minimum, float maximum)
            {
                Minimum = minimum;
                Maximum = maximum;
            }
            public float Minimum;
            public float Maximum;
            public bool Overlap(Projection other, out float amount)
            {
                if (Maximum >= other.Minimum && other.Maximum >= Minimum)
                {
                    if (Maximum < other.Maximum)
                        amount = Math.Abs(Maximum - other.Minimum);
                    else
                        amount = Math.Abs(other.Maximum - Minimum);
                    return true;
                }
                else
                {
                    amount = -1;
                    return false;
                }
            }
        }

        private Projection Project(Vector2 position, Vector2[] vertices, Vector2 axis)
        {
            float minimum = Vector2.Dot(position + vertices[0], axis);
            float maximum = minimum;
            //Find maximum and minimum
            for (int i = 1; i < vertices.Length; i++)
            {
                // NOTE: the axis must be normalized to get accurate projections
                float p = Vector2.Dot(position + vertices[i], axis);
                if (p < minimum)
                    minimum = p;
                else if (p > maximum)
                    maximum = p;
            }
            return new Projection(minimum, maximum);
        }
        
        //Structure for storing details about the collision
        public struct CollisionInfo
        {
            public Vector2 CollisionNormal;
            public Vector2 Overlapping;
            public List<Vector2> CollisionPoints;
            public List<Vector2> IntersectionPoints;
        }

        private delegate bool IsCollidingDelegate(PhysicsBody bodyA, PhysicsBody bodyB, out CollisionInfo collisionInfo);
        private delegate void CollisionResolveDelegate(PhysicsBody bodyA, PhysicsBody bodyB, CollisionInfo collisionInfo);
        public delegate void CollisionInfoDelegate(CollisionInfo collisionInfo, PhysicsBody bodyA, PhysicsBody bodyB);
        private CollisionInfoDelegate my_CollisionListeners = null;
        private IsCollidingDelegate[,] my_CollisionFunctionParameters;
        private CollisionResolveDelegate[] my_ResolveFunctionParameters;
        //Store a list of objects
        private List<PhysicsBody> my_Bodies;

        //Set gravitational constant and gravitational normal
        private static Vector2 my_Gravity = new Vector2(0, 20.0f);
        private static Vector2 my_GravityNormal = mathsUtility.leftPerpendicular(my_Gravity);

        PhysicsBody bodySelected = null;
        private bool showDebug = false;
        Vector2 lastMousePosition;
        InputManager my_InputManager = InputManager.Instance;

        public void RegisterListener(CollisionInfoDelegate parameter_Listener)
        {
            my_CollisionListeners += parameter_Listener;
        }
        public void UnregisterListener(CollisionInfoDelegate parameter_Listener)
        {
            my_CollisionListeners -= parameter_Listener;
        }


        public virtual void Update()
        {
            CollisionInfo collisionInfo;

            //Get mouse position from an instance of the input manager
            Vector2 mousePosition = my_InputManager.GetMousePosition();
            
            //Check for gravity manipulation inputs
            if (my_InputManager.KeyIsPressed(Keys.W))
            {
                my_Gravity = new Vector2(my_Gravity.X, my_Gravity.Y - 0.25f);
            }
            if (my_InputManager.KeyIsPressed(Keys.S))
            {
                my_Gravity = new Vector2(my_Gravity.X, my_Gravity.Y + 0.25f);
            }
            if (my_InputManager.KeyIsPressed(Keys.A))
            {
                my_Gravity = new Vector2(my_Gravity.X - 0.25f, my_Gravity.Y);
            }
            if (my_InputManager.KeyIsPressed(Keys.D))
            {
                my_Gravity = new Vector2(my_Gravity.X + 0.25f, my_Gravity.Y);
            }
            if (my_InputManager.KeyIsPressed(Keys.Enter))
            {
                my_Gravity = new Vector2(0, 20.0f);
            }


            if (showDebug == true)
            {
                //Draw debug strings
                if (bodySelected != null)
                {
                    RenderManager.Instance.DrawString("   Position: " + bodySelected.Position);
                    RenderManager.Instance.DrawString("   Linear Velocity: " + bodySelected.linearVelocity);
                    RenderManager.Instance.DrawString("   Mass: " + bodySelected.Mass);
                    RenderManager.Instance.DrawString("   Restitution: " + bodySelected.Restitution);
                    RenderManager.Instance.DrawString("   Static Friction: " + bodySelected.StaticFriction);
                    RenderManager.Instance.DrawString("   Dynamic Friction: " + bodySelected.DynamicFriction);
                    RenderManager.Instance.DrawString("   Angular Velocity: " + bodySelected.angularVelocity);
                    RenderManager.Instance.DrawString("   Material: " + bodySelected.ShapeDefine.Material);
                    RenderManager.Instance.DrawString("   Volume: " + bodySelected.ShapeDefine.Volume);
                }
                RenderManager.Instance.DrawString("   Gravity: " + my_Gravity);
            }

            if (bodySelected != null)
            {
                if (InputManager.Instance.KeyIsPressed(Keys.Right))
                    bodySelected.AddForce(new Vector2(50 * bodySelected.Mass, 0), bodySelected.Position, FORCE_TYPE.IMPULSE);
                if (InputManager.Instance.KeyIsPressed(Keys.Left))
                    bodySelected.AddForce(new Vector2(-50 * bodySelected.Mass, 0), bodySelected.Position, FORCE_TYPE.IMPULSE);
                if (InputManager.Instance.KeyIsPressed(Keys.Up))
                    bodySelected.AddForce(new Vector2(0, -50 * bodySelected.Mass), bodySelected.Position, FORCE_TYPE.IMPULSE);
                if (InputManager.Instance.KeyIsPressed(Keys.Down))
                    bodySelected.AddForce(new Vector2(0, 50 * bodySelected.Mass), bodySelected.Position, FORCE_TYPE.IMPULSE);
            }
            
            
            //Debug toggle
            if (my_InputManager.KeyWasPressed(Keys.M))
            {
                if (showDebug == false)
                    showDebug = true;
                else
                    showDebug = false;
            }


            //Add forces for each object
            foreach (PhysicsBody bodyA in my_Bodies)
            {
                if (bodyA.isStatic() == false)
                {
                    foreach (PhysicsBody bodyB in my_Bodies)
                    {
                        //Check if bodies are colliding and resolve if neccesary
                        if (bodyA.GetID() != bodyB.GetID() && isColliding(bodyA, bodyB, out collisionInfo))
                        {
                            my_ResolveFunctionParameters[bodyB.Material].Invoke(bodyA, bodyB, collisionInfo);
                        }
                    }
                    
                    //Select object for debug
                    if (my_InputManager.LeftMouseIsPressed() && Math.Abs(mousePosition.X - bodyA.Position.X) < 10
                                            && Math.Abs(mousePosition.Y - bodyA.Position.Y) < 10)
                    {
                        bodySelected = bodyA;
                    }

                    if (bodyA.ShapeDefine.Gravity == true)
                    {
                        
                        if (bodyA.Shape == 2)
                        {
                            Vector2 CentreOfGravity = new Vector2();
                            for (int i = 0; i < bodyA.PolygonDefine.VerticesCount; i++)
                            {
                                CentreOfGravity += bodyA.PolygonDefine.Vertices[i];
                            }
                            CentreOfGravity *= 1 / bodyA.PolygonDefine.VerticesCount;
                            bodyA.AddForce((my_Gravity) * bodyA.Mass, bodyA.Position, FORCE_TYPE.IMPULSE);
                        }
                        else
                        {
                            //Add gravity
                            bodyA.AddForce((my_Gravity) * bodyA.Mass, bodyA.Position, FORCE_TYPE.IMPULSE);
                        }
                    }
                        
                    
                    //Update properties of body
                    bodyA.Update();
                }
                //By next update, current mouse position will be previous mouse position
                lastMousePosition = mousePosition;
            }
        }

        public void AddBody(PhysicsBody phys_body)
        {
            //Add a new object to the objects list
            my_Bodies.Add(phys_body);
        }

        public void Clear()
        {
            //Clear the objects list
            my_Bodies.Clear();
        }

        //----------------*******COLLISION*******----------------
        private bool isColliding(PhysicsBody bodyA, PhysicsBody bodyB, out CollisionInfo collisionInfo)
        {
            return my_CollisionFunctionParameters[bodyA.Shape, bodyB.Shape].Invoke(bodyA, bodyB, out collisionInfo);
        }

        private bool isCollidingCircleCircle(PhysicsBody bodyA, PhysicsBody bodyB, out CollisionInfo collisionInfo)
        {
            collisionInfo.CollisionPoints = new List<Vector2>();
            collisionInfo.IntersectionPoints = new List<Vector2>();
            
            //Get distance between centres of circles
            Vector2 distance = bodyA.Position - bodyB.Position;
            //Get sum of radii
            float widths = bodyA.CircleDefine.Radius + bodyB.CircleDefine.Radius;
            
            //if the distance between centres is less than the width then they are intersecting 
            if (distance.LengthSquared() < widths * widths)
            {
                if (distance.X == 0 && distance.Y == 0)
                {
                    //Since the circles have equal positions a "Special distance" is required to create a collision normal
                    Vector2 specialDistance = bodyA.LastPosition - bodyB.Position;
                    if (specialDistance.X == 0 && specialDistance.Y == 0)
                        specialDistance.X = 1;
                    else
                        specialDistance.Normalize();

                    collisionInfo.Overlapping = specialDistance * widths;
                    collisionInfo.CollisionNormal = specialDistance;
                    collisionInfo.CollisionPoints = new List<Vector2>();
                }
                else
                {
                    //Check how much the circles are intersecting and set collision info accordingly
                    float overlappingAmount = widths - distance.Length();
                    distance.Normalize();
                    collisionInfo.Overlapping = distance * overlappingAmount;
                    collisionInfo.CollisionNormal = distance;
                    collisionInfo.CollisionPoints.Add(bodyB.Position + (collisionInfo.CollisionNormal * bodyB.CircleDefine.Radius));
                }
                return true;
            }
            else
            {
                collisionInfo.CollisionNormal = new Vector2();
                collisionInfo.Overlapping = new Vector2();
                return false;
            }
        }

        private bool isCollidingConvexConvex(PhysicsBody bodyA, PhysicsBody bodyB, out CollisionInfo collisionInfo)
        {
            collisionInfo.CollisionNormal = new Vector2();
            collisionInfo.Overlapping = new Vector2();
            collisionInfo.CollisionPoints = new List<Vector2>();
            collisionInfo.IntersectionPoints = new List<Vector2>();

            List<Vector2> verticesInside = new List<Vector2>();

            Vector2 edge;
            Vector2 edgeNormal;

            Vector2 shortestOverlapAxis = bodyA.PolygonDefine.GetVertex(0);

            Projection projectionA;
            Projection projectionB;
            float shortestOverlapAmount = int.MaxValue;
            float overlapAmount = 0;
            
            //Check every vertex in the first body
            for (int i = 0; i < bodyA.PolygonDefine.VerticesCount; i++)
            {
                //Get the vector the side that is being checked
                edge = bodyA.PolygonDefine.GetVertex((i + 1 == bodyA.PolygonDefine.VerticesCount ? 0 : i + 1)) - bodyA.PolygonDefine.GetVertex(i);
                //Get its normal direction
                edgeNormal = mathsUtility.rightPerpendicular(edge);
                edgeNormal.Normalize();
                
                //Create projections
                projectionA = Project(bodyA.Position, bodyA.PolygonDefine.Vertices, edgeNormal);
                projectionB = Project(bodyB.Position, bodyB.PolygonDefine.Vertices, edgeNormal);
                
                //If projections don't overlap then not colliding
                if (!projectionA.Overlap(projectionB, out overlapAmount))
                    return false;
                else if (overlapAmount < shortestOverlapAmount)
                {
                    //Make sure the shortest overlap is found
                    shortestOverlapAxis = edgeNormal;
                    shortestOverlapAmount = overlapAmount;
                }
                
                //Check side against every vertex in B
                for (int j = 0; j < bodyB.PolygonDefine.VerticesCount; j++)
                {
                    //Represent the 2 lines as 4 points
                    Vector2 point1 = (bodyB.PolygonDefine.GetVertex(j) + bodyB.Position);
                    Vector2 point2 = (bodyB.PolygonDefine.GetVertex((j + 1 == bodyB.PolygonDefine.VerticesCount ? 0 : j + 1)) + bodyB.Position);
                    Vector2 point3 = (bodyA.PolygonDefine.GetVertex(i) + bodyA.Position);
                    Vector2 point4 = (bodyA.PolygonDefine.GetVertex((i + 1 == bodyA.PolygonDefine.VerticesCount ? 0 : i + 1)) + bodyA.Position);
                    Vector2 intersectionPoint;
                    
                    //Find if the sides are intersecting and if so add the intersection point to the list
                    if (mathsUtility.LineLineIntersection(point1, point2, point3, point4, out intersectionPoint))
                        collisionInfo.IntersectionPoints.Add(intersectionPoint);
                }
                
                //Find the vertices of B which are inside A
                if (i == 0)
                {
                    for (int j = 0; j < bodyB.PolygonDefine.VerticesCount; j++)
                    {
                        Vector2 vertex = (bodyB.PolygonDefine.GetVertex(j) + bodyB.Position) - (bodyA.PolygonDefine.GetVertex(i) + bodyA.Position);
                        float dot = Vector2.Dot(vertex, edgeNormal);

                        if (dot < 0)
                            verticesInside.Add(bodyB.PolygonDefine.GetVertex(j) + bodyB.Position);
                    }
                }
                else
                {
                    for (int j = 0; j < verticesInside.Count; j++)
                    {
                        Vector2 vertex = verticesInside[j] - (bodyA.PolygonDefine.GetVertex(i) + bodyA.Position);
                        float dot = Vector2.Dot(vertex, edgeNormal);

                        if (dot > 0)
                        {
                            verticesInside.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }

            for (int i = 0; i < bodyB.PolygonDefine.VerticesCount; i++)
            {
                //Calculate the Vector of B's side and find the normal
                edge = bodyB.PolygonDefine.GetVertex((i + 1 == bodyB.PolygonDefine.VerticesCount ? 0 : i + 1)) - bodyB.PolygonDefine.GetVertex(i);
                edgeNormal = mathsUtility.rightPerpendicular(edge);
                edgeNormal.Normalize();

                //Just swap
                projectionB = Project(bodyA.Position, bodyA.PolygonDefine.Vertices, edgeNormal);
                projectionA = Project(bodyB.Position, bodyB.PolygonDefine.Vertices, edgeNormal);

                if (!projectionA.Overlap(projectionB, out overlapAmount))
                    return false;
                else if (overlapAmount < shortestOverlapAmount)
                {
                    shortestOverlapAxis = edgeNormal;
                    shortestOverlapAmount = overlapAmount;
                }

                if (i == 0)
                {
                    for (int j = 0; j < bodyA.PolygonDefine.VerticesCount; j++)
                    {
                        Vector2 vertex = (bodyA.PolygonDefine.GetVertex(j) + bodyA.Position) - (bodyB.PolygonDefine.GetVertex(i) + bodyB.Position);
                        float dot = Vector2.Dot(vertex, edgeNormal);

                        if (dot < 0)
                            verticesInside.Add(bodyA.PolygonDefine.GetVertex(j) + bodyA.Position);
                    }
                }
                else
                {
                    for (int j = 0; j < verticesInside.Count; j++)
                    {
                        Vector2 vertex = verticesInside[j] - (bodyB.PolygonDefine.GetVertex(i) + bodyB.Position);
                        float dot = Vector2.Dot(vertex, edgeNormal);
                        if (dot > 0)
                        {
                            verticesInside.RemoveAt(j);
                            j--;
                        }
                    }
                }
            }
            
            //Set collision info
            collisionInfo.CollisionNormal = shortestOverlapAxis;
            collisionInfo.Overlapping = shortestOverlapAxis * shortestOverlapAmount;
            collisionInfo.CollisionPoints.AddRange(verticesInside);

            return true;
        }

        private bool isCollidingConvexCircle(PhysicsBody polygon, PhysicsBody circle, out CollisionInfo collisionInfo)
        {
            collisionInfo.CollisionPoints = new List<Vector2>();
            collisionInfo.IntersectionPoints = new List<Vector2>();

            //Used when circle inside polygon
            Vector2 closestProjection = new Vector2(ProgramManager.Instance.ScreenWidth, ProgramManager.Instance.ScreenHeight);
            bool insidePolygon = true;
            for (int i = 0; i < polygon.PolygonDefine.VerticesCount; i++)
            {
                //Vector from current edge to center of circle
                Vector2 vectorToCircle = circle.Position - (polygon.Position + polygon.PolygonDefine.GetVertex(i));
                Vector2 currentEdge = polygon.PolygonDefine.GetVertex(i + 1 == polygon.PolygonDefine.VerticesCount ? 0 : i + 1) - polygon.PolygonDefine.GetVertex(i);
                //Length of current edge squared
                float currentEdgeLengthSquared = currentEdge.LengthSquared();
                currentEdge.Normalize();
                float circleToEdgeProjection = Vector2.Dot(vectorToCircle, currentEdge);

                if (circleToEdgeProjection > 0)
                {
                    if ((circleToEdgeProjection * circleToEdgeProjection) < currentEdgeLengthSquared)
                    {
                        Vector2 vectorCircleProjectionEdge = currentEdge * circleToEdgeProjection;
                        Vector2 projectionToCircle = vectorToCircle - vectorCircleProjectionEdge;
                        float projectionToCircleLengthSquared = projectionToCircle.LengthSquared();

                        if (projectionToCircleLengthSquared < closestProjection.LengthSquared())
                            closestProjection = projectionToCircle;

                        if (projectionToCircleLengthSquared < circle.CircleDefine.Radius * circle.CircleDefine.Radius)
                        {
                            collisionInfo.CollisionNormal = - projectionToCircle;
                            collisionInfo.CollisionNormal.Normalize();
                            collisionInfo.CollisionPoints.Add(circle.Position + (collisionInfo.CollisionNormal * circle.CircleDefine.Radius));
                            collisionInfo.Overlapping = collisionInfo.CollisionNormal * (circle.CircleDefine.Radius - projectionToCircle.Length());
                            return true;
                        }
                    }
                }
                else if (vectorToCircle.LengthSquared() < circle.CircleDefine.Radius * circle.CircleDefine.Radius)
                {
                    collisionInfo.CollisionNormal = - vectorToCircle;
                    collisionInfo.CollisionNormal.Normalize();
                    collisionInfo.CollisionPoints.Add(polygon.PolygonDefine.GetVertex(i) + polygon.Position);
                    collisionInfo.Overlapping = collisionInfo.CollisionNormal * (circle.CircleDefine.Radius - vectorToCircle.Length());
                    return true;
                }
                Vector2 edgeNormal = mathsUtility.rightPerpendicular(currentEdge);

                if (Vector2.Dot(vectorToCircle, edgeNormal) > 0)
                    insidePolygon = false;
                else if (i == polygon.PolygonDefine.VerticesCount - 1)
                {
                    collisionInfo.CollisionNormal = vectorToCircle;
                    collisionInfo.CollisionNormal.Normalize();
                    collisionInfo.Overlapping = vectorToCircle * (vectorToCircle.Length() + circle.CircleDefine.Radius);
                    collisionInfo.CollisionPoints.Add(polygon.PolygonDefine.GetVertex(0) + polygon.Position);
                    return insidePolygon;
                }
            }
            collisionInfo.CollisionNormal = new Vector2();
            collisionInfo.Overlapping = new Vector2();
            return false;
        }

        private bool isCollidingCircleConvex(PhysicsBody bodyA, PhysicsBody bodyB, out CollisionInfo collisionInfo)
        {
            //Dummy, redirect to isCollidingConvexCircle-function.
            return isCollidingConvexCircle(bodyB, bodyA, out collisionInfo);
        }

        private void ResolveSolid(PhysicsBody bodyA, PhysicsBody bodyB, CollisionInfo collisionInfo)
        {
            //Make sure normal is in right direction
            if (Vector2.Dot(bodyA.Position - bodyB.Position, collisionInfo.CollisionNormal) < 0)
            {
                collisionInfo.CollisionNormal = -collisionInfo.CollisionNormal;
                collisionInfo.Overlapping = -collisionInfo.Overlapping;
            }
            //Translate minimum distance so not overlapping
            bodyA.Position += collisionInfo.Overlapping;

            float j = 0;
            float jt = 0;
            float CoefficientOfFriction;
            Vector2 collisionPointRelativeVelocityAtoB = Vector2.UnitX;
            Vector2 collisionTangent = new Vector2();
            //Not 100% accurate if lands on flat surface because collision-points are calculated one at a time, would be true even if done via a queue
            foreach (Vector2 collisionPoint in collisionInfo.CollisionPoints)
            {
                //First get distance-vector from each body-center to collision-point
                Vector2 collisionPointRadiusA = collisionPoint - bodyA.Position;
                Vector2 collisionPointRadiusB = collisionPoint - bodyB.Position;

                //Calculate vector perpendicular to the vector from mass-center to collision-point
                Vector2 collisionPointRadiusAPerpendicular = mathsUtility.rightPerpendicular(collisionPointRadiusA);
                Vector2 collisionPointRadiusBPerpendicular = mathsUtility.rightPerpendicular(collisionPointRadiusB);

                //Velocity at collisionpoint
                Vector2 collisionPointVelocityA = bodyA.linearVelocity;
                Vector2 collisionPointVelocityB = bodyB.linearVelocity;        

                //Relative velocity of bodyA's with respect to bodyB's collision-point velocity
                collisionPointRelativeVelocityAtoB = collisionPointVelocityA - collisionPointVelocityB;
                
                //Find a tangent to the collision (for frictional purposes)
                collisionTangent = collisionPointRelativeVelocityAtoB - Vector2.Dot(collisionPointRelativeVelocityAtoB, collisionInfo.CollisionNormal) * collisionInfo.CollisionNormal;
                collisionTangent.Normalize();


                //Calculate new impulse
                j = Vector2.Dot(-(1 + (bodyA.Restitution + bodyB.Restitution) * 0.5f) * collisionPointRelativeVelocityAtoB, collisionInfo.CollisionNormal)
                            / (Vector2.Dot(collisionInfo.CollisionNormal, (collisionInfo.CollisionNormal * (bodyA.InverseMass + bodyB.InverseMass))));
                
                //Calculate tangential force
                jt = -(1 + (bodyA.Restitution + bodyB.Restitution) * 0.5f ) * Vector2.Dot(collisionPointRelativeVelocityAtoB, collisionTangent)/(bodyA.InverseMass + bodyB.InverseMass);
                
                //Calculate coefficient of friction for the collision
                CoefficientOfFriction = (bodyA.StaticFriction + bodyB.StaticFriction) * 0.5f;
                
                //Check if tangential force is great enough to overcome friction
                if (Math.Abs(jt) >= j * CoefficientOfFriction)
                {
                    //Modify coefficient of friction to reflect dynamic friction
                    CoefficientOfFriction = (bodyA.DynamicFriction + bodyB.DynamicFriction) * 0.5f;
                    jt = -j * CoefficientOfFriction;   
                }
                
                //"Temp"-fix for j (make sure sign is correct)
                if (Vector2.Dot(j * collisionInfo.CollisionNormal, collisionInfo.CollisionNormal) < 0)
                    j = -j;

                //Apply force (torque manually calculated by the body itself)
                bodyA.AddForce(j * collisionInfo.CollisionNormal, collisionPoint, FORCE_TYPE.IMPULSE);

                if (Math.Abs(jt) < Limits.MAXIMUM_FORCE)
                    bodyA.AddForce(jt * collisionTangent, collisionPoint, FORCE_TYPE.IMPULSE);

                bodyB.AddForce(-j * collisionInfo.CollisionNormal, collisionPoint, FORCE_TYPE.IMPULSE);
            }
        }

        public Vector2 Gravity
        {
            get { return my_Gravity; }
            set { my_Gravity = value; }
        }

        //Set up instance
        private static volatile PhysicsManager my_Instance;
        private static object my_SyncRoot = new Object();
        private PhysicsManager()
        {
            my_Bodies = new List<PhysicsBody>();

            //Instantiate matrix of function pointers to different collision-functions
            my_CollisionFunctionParameters = new IsCollidingDelegate[Shape.NR_SHAPES, Shape.NR_SHAPES];
            //If two circles collide, use "CircleCircle" function
            my_CollisionFunctionParameters[Shape.CIRCLE, Shape.CIRCLE] = new IsCollidingDelegate(isCollidingCircleCircle);
            //If two convex shapes collide, use "ConvexConvex" function
            my_CollisionFunctionParameters[Shape.POLYGON, Shape.POLYGON] = new IsCollidingDelegate(isCollidingConvexConvex);
            //If two convex and circle shapes collide, use "ConvexCircle" function
            my_CollisionFunctionParameters[Shape.POLYGON, Shape.CIRCLE] = new IsCollidingDelegate(isCollidingConvexCircle);
            //If two circle and convex shapes collide, use "CircleConvex" function (Redirects to ConvexCircle)
            my_CollisionFunctionParameters[Shape.CIRCLE, Shape.POLYGON] = new IsCollidingDelegate(isCollidingCircleConvex);

            my_ResolveFunctionParameters = new CollisionResolveDelegate[Properties.Material.NR_MATERIALS];
            
            //Need to set for each possible material, set for being resolved as solids, could expand for interactions with liquids or non-rigid bodies
            my_ResolveFunctionParameters[Material.SOLID] = new CollisionResolveDelegate(ResolveSolid);
            my_ResolveFunctionParameters[Material.ICE] = new CollisionResolveDelegate(ResolveSolid);
            my_ResolveFunctionParameters[Material.WOOD] = new CollisionResolveDelegate(ResolveSolid);
            my_ResolveFunctionParameters[Material.METAL] = new CollisionResolveDelegate(ResolveSolid);
            my_ResolveFunctionParameters[Material.RUBBER] = new CollisionResolveDelegate(ResolveSolid);

            my_GravityNormal.Normalize();
        }
        public static PhysicsManager Instance
        {
            get
            {
                if (my_Instance == null)
                    lock (my_SyncRoot)
                        if (my_Instance == null)
                        {
                            my_Instance = new PhysicsManager();
                        }
                return my_Instance;
            }
        }
    }
}