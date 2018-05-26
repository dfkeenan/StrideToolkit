using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XenkoToolkit.Collections;

namespace XenkoToolkit.Engine
{
    /// <summary>
    /// Extensions for <see cref="Entity"/>.
    /// </summary>
    public static class EntityExtensions
    {

        /// <summary>
        /// Performs a breadth first search of the entity and it's children for a component of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
       /// <returns>The component or null if does no exist.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static T GetComponentInChildren<T>(this Entity entity) where T : EntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //breadth first
            var queue = new Queue<Entity>();
            queue.Enqueue(entity);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();


                var component = current.Get<T>();

                if (component != null)
                {
                    return component;
                }

                var children = current.Transform.Children;

                for (int i = 0; i < children.Count; i++)
                {
                    queue.Enqueue(children[i].Entity);
                }
            }

            return null;
        }

        /// <summary>
        /// Performs a breadth first search of the entity and it's children for a component of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>The component or null if does no exist.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static T GetComponentInChildren<T>(this Entity entity, bool includeDisabled = false) where T : ActivableEntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //breadth first
            var queue = new Queue<Entity>();
            queue.Enqueue(entity);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();


                var component = current.Get<T>();

                if (component != null && (component.Enabled || includeDisabled))
                {
                    return component;
                }

                var children = current.Transform.Children;

                for (int i = 0; i < children.Count; i++)
                {
                    queue.Enqueue(children[i].Entity);
                }
            }

            return null;
        }

        /// <summary>
        /// Performs a depth first search of the entity and it's children for all components of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInChildren<T>(this Entity entity) where T : EntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //breadth first
            var queue = new Queue<Entity>();
            queue.Enqueue(entity);
            queue.EnqueueRange(entity.GetChildren());

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var component in current.GetAll<T>())
                {
                    yield return component;
                }
            }
        }

        /// <summary>
        /// Performs a depth first search of the entity and it's children for all components of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInChildren<T>(this Entity entity, bool includeDisabled = false) where T : ActivableEntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));


            //breadth first
            var queue = new Queue<Entity>();
            queue.Enqueue(entity);
            queue.EnqueueRange(entity.GetChildren());

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                foreach (var component in current.GetAll<T>())
                {
                    if (component.Enabled || includeDisabled)
                    {
                        yield return component;
                    }
                }
            }
        }



        /// <summary>
        /// Performs a depth first search of the entity and it's decendants for all components of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInDecendants<T>(this Entity entity) where T : EntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //depth first
            var stack = new Stack<Entity>();
            stack.Push(entity);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                foreach (var component in current.GetAll<T>())
                {
                    yield return component;
                }

                var children = current.Transform.Children;

                for (int i = children.Count - 1; i >= 0; i--)
                {
                    stack.Push(children[i].Entity);
                }
            }
        }

        /// <summary>
        /// Performs a depth first search of the entity and it's decendants for all components of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInDecendants<T>(this Entity entity, bool includeDisabled = false) where T : ActivableEntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            //depth first
            var stack = new Stack<Entity>();
            stack.Push(entity);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                foreach (var component in current.GetAll<T>())
                {
                    if (component.Enabled || includeDisabled)
                    {
                        yield return component;
                    }
                }

                var children = current.Transform.Children;

                for (int i = children.Count - 1; i >= 0; i--)
                {
                    stack.Push(children[i].Entity);
                }
            }
        }

        /// <summary>
        /// Performs a search of the entity and it's ancestors for a component of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns>The component or <c>null</c> if does no exist.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static T GetComponentInParent<T>(this Entity entity) where T : EntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var current = entity;

            do
            {
                var component = current.Get<T>();

                if (component != null)
                {
                    return component;
                }

            } while ((current = current.GetParent()) != null);

            return null;
        }

        /// <summary>
        /// Performs a search of the entity and it's ancestors for a component of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>The component or <c>null</c> if does no exist.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static T GetComponentInParent<T>(this Entity entity, bool includeDisabled = false) where T : ActivableEntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var current = entity;

            do
            {
                var component = current.Get<T>();

                if (component != null && (component.Enabled || includeDisabled))
                {
                    return component;
                }

            } while ((current = current.GetParent()) != null);

            return null;
        }

        /// <summary>
        /// Performs a search of the entity and it's ancestors for all components of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
       /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInParent<T>(this Entity entity) where T : EntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var current = entity;

            do
            {
                foreach (var component in current.GetAll<T>())
                {
                    yield return component;
                }


            } while ((current = current.GetParent()) != null);
        }

        /// <summary>
        /// Performs a search of the entity and it's ancestors for all components of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entity">The entity.</param>
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInParent<T>(this Entity entity, bool includeDisabled = false) where T : ActivableEntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var current = entity;

            do
            {
                foreach (var component in current.GetAll<T>())
                {
                    if (component.Enabled || includeDisabled)
                    {
                        yield return component;
                    }
                }


            } while ((current = current.GetParent()) != null);
        }
    }

}
