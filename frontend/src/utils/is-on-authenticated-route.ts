import { routes } from '~constants/routes';

export const isOnAuthenticatedRoute = (path: string) => {
  return routes.some(
    (route) => path.startsWith(route.path) && route.isProtected,
  );
};
