import { HttpErrorResponse, HttpInterceptorFn, HttpRequest } from '@angular/common/http';
import { AuthService } from '../services/auth/auth.service';
import { inject } from '@angular/core';
import { catchError, map, switchMap, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { IUser } from '../models/IUser';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  
  req = req.clone({
    withCredentials:true
  })
  
   return next(req).pipe(
     catchError((err:any)=>{
       if(err instanceof HttpErrorResponse) {
         if(err.status === 401) {
            //TOASTER
           console.log('jarane sta radis');
           router.navigate(['login']);
         }
       }
       return throwError(()=>new Error("Some other error!"));
     })
   );

 
};
