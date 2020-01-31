using System.Collections.Generic;

namespace MadaoEcs {
    public static class EcsGroupController {

        private static Dictionary<ulong, HashSet<IEntity>> groupsEntities = new Dictionary<ulong, HashSet<IEntity>>();

        public static void RegisterGroupInstance(IEntity entity, GroupComponent group) {
            if (groupsEntities.TryGetValue(group.GroupId, out var entitiesIds)) {
                entitiesIds.Add(entity);
            } else {
                groupsEntities.Add(group.GroupId, new HashSet<IEntity> { entity });
            }
        }

        public static void RemoveGroupInstance(IEntity entity, GroupComponent group) {
            if (groupsEntities.TryGetValue(group.GroupId, out var entitiesIds)) {
                entitiesIds.Remove(entity);
                if (entitiesIds.Count == 0) {
                    groupsEntities.Remove(entity.Id);
                }
            }
        }

        public static HashSet<IEntity> GetGrouppedEntities(GroupComponent groupComponent)  {
            return GetGrouppedEntities(groupComponent.GroupId);
        }

        public static HashSet<IEntity> GetGrouppedEntities(ulong groupId) {
            return groupsEntities[groupId];
        }
    }
}
