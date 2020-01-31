namespace MadaoEcs {
    public static class EcsUidGenerator {

        private static ulong lastId = 0;

        public static ulong GetNewUid() {
            return lastId++;
        }

        public static void SetOffset(ulong offset) {
            lastId = offset;
        }
    }
}
