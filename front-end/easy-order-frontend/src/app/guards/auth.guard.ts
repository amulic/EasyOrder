import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth/auth.service';
import { inject } from '@angular/core';
import { Observable } from 'rxjs';
import { map, catchError, of, switchMap, take } from 'rxjs';

export const authGuard: CanActivateFn = (route, state) => {
  const authService = inject(AuthService);
  const router = inject(Router);

  // Check if user is already authenticated
  return authService.user$.pipe(
    take(1),  // Only take the first emission
    switchMap(user => {
      if (user) {
        // User is already authenticated
        return of(true);
      } else {
        // If not authenticated, make an API call to validate the user session
        return authService.getMe().pipe(
          map(() => true),  // If the API call succeeds, allow access
          catchError(() => {
            // If API call fails (e.g., 401 Unauthorized), navigate to login
            router.navigate(['login']);
            return of(false);
          })
        );
      }
    })
  );
};
