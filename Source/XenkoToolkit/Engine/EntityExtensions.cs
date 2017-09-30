using SiliconStudio.Xenko.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XenkoToolkit.Engine
{
    /// <summary>
    /// Extensions for <see cref="Entity"/>.
    /// </summary>
    public static class EntityExtensions
    {

        /// <summary>
        /// Performs a breadth first search of a collection of entities and there children for a component of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of component.</typeparam>
        /// <param name="entities">The collection of entities.</param>
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>The component or null if does no exist.</returns>
        /// <exception cref="ArgumentNullException">The entities argument was <c>null</c>.</exception>
        public static T FindComponent<T>(this IEnumerable<Entity> entities, bool includeDisabled = false) where T : EntityComponent
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            //breadth first
            var queue = new Queue<Entity>(entities);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();


                var component = current.Get<T>();

                var isEnabled = ((component as ActivableEntityComponent)?.Enabled).GetValueOrDefault(true);
                if (component != null && (isEnabled || includeDisabled))
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
        public static T GetComponentInChildren<T>(this Entity entity, bool includeDisabled = false) where T : EntityComponent
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

                var isEnabled = ((component as ActivableEntityComponent)?.Enabled).GetValueOrDefault(true);
                if (component != null && (isEnabled || includeDisabled))
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
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInChildren<T>(this Entity entity, bool includeDisabled = false) where T : EntityComponent
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
                    var isEnabled = ((component as ActivableEntityComponent)?.Enabled).GetValueOrDefault(true);
                    if (component != null && (isEnabled || includeDisabled))
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
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>The component or <c>null</c> if does no exist.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static T GetComponentInParent<T>(this Entity entity, bool includeDisabled = false) where T : EntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var current = entity;

            do
            {
                var component = current.Get<T>();

                var isEnabled = ((component as ActivableEntityComponent)?.Enabled).GetValueOrDefault(true);
                if (component != null && (isEnabled || includeDisabled))
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
        /// <param name="includeDisabled">Should search include <see cref="ActivableEntityComponent"/> where <see cref="ActivableEntityComponent.Enabled"/> is <c>false</c>.</param>
        /// <returns>An iteration on the components.</returns>
        /// <exception cref="ArgumentNullException">The entity was <c>null</c>.</exception>
        public static IEnumerable<T> GetComponentsInParent<T>(this Entity entity, bool includeDisabled = false) where T : EntityComponent
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var current = entity;

            do
            {
                foreach (var component in current.GetAll<T>())
                {
                    var isEnabled = ((component as ActivableEntityComponent)?.Enabled).GetValueOrDefault(true);
                    if (component != null && (isEnabled || includeDisabled))
                    {
                        yield return component;
                    }
                }


            } while ((current = current.GetParent()) != null);
        }
    }

}
