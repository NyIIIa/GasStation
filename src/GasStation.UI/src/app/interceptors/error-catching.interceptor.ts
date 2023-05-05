import {Injectable} from '@angular/core';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {catchError} from "rxjs/operators";
import {ToastrService} from "ngx-toastr";

@Injectable()
export class ErrorCatchingInterceptor implements HttpInterceptor {

  constructor(private toast: ToastrService) {
  }
  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          let errorMsg = '';

          if (error.error instanceof ErrorEvent) { //'This is client side error'
            errorMsg = `Error: ${error.error.message}`;
          } else { //'This is server side error'

            if (error.error.errors == null) {
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
