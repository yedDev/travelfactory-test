<template>
    <RouterView></RouterView>
</template>

<script>
import { defineComponent } from "vue";
import { RouterView } from "vue-router";

export default defineComponent({
    name: "default-content",
    components: {
        RouterView,
    },
    setup() {

        return {
        };
    },
});
</script>