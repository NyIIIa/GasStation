import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from "rxjs/operators";
import {ToastrService} from "ngx-toastr";
import {AuthService} from "../services/authentication/auth.service";
import {Router} from "@angular/router";

@Injectable()
export class ErrorCatchingInterceptor implements HttpInterceptor {

  constructor(private toast: ToastrService,
              private authService: AuthService,
              private router: Router) {
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMsg = '';
          if (error.status == 403) {
            this.toast.warning('You do not have enough rights to manipulate this source!'
              , 'Authentication information');
          }
          if (error.status == 401) {
            //TODO Implement refresh token instead of this logic
            if(this.authService.isLoggedIn()){
              this.authService.signOut();
              this.toast.warning('Your access token has expired! Please login again!');
              this.router.navigate(['/login']);
            }
            else {
              this.toast.warning('You are not authorized to manipulate this source!'
                , 'Authorization information');
            }
          }
          if (error.error instanceof ErrorEvent) { //'This is client side error'
            errorMsg = `Error: ${error.error.message}`;
          } else { //'This is server side error'

            if (!error.error.errors) {
              errorMsg = error.error.title;
            } else {
              let errorMessages = [];
              let errors = error.error.errors;
              for (let error in errors) {
                let fieldErrors = errors[error];
                errorMessages.push(fieldErrors);
              }
              errorMsg = errorMessages.join('\n');
            }
          }
          this.toast.error(errorMsg, 'The error has occurred!');


          return throwError(errorMsg);
        })
      )
  }
}
