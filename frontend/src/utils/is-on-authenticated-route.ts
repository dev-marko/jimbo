import { routes } from "~constants/routes";

export const isOnAuthenticatedRoute = (path: string) => {
    return routes.some(route => route.path === path && route.isProteced);
} 