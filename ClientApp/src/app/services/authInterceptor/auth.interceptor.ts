import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router, private toastr: ToastrService) { }

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const token = localStorage.getItem('authToken');

        if (token) {
            req = req.clone({
                setHeaders: { Authorization: `Bearer ${token}` }
            });
        }
        else {
            this.router.navigate(['login']);
        }
        return next.handle(req).pipe(
            catchError(
                (err:HttpErrorResponse) => {
                    if(err.status === 401) {
                        this.toastr.error('You must be logged in', 'Error');
                        return throwError("You must be logged in");
                    }
                    else if(err.status === 403) {
                        this.toastr.error('You are not allowed to access this resource', 'Error');
                        this.router.navigate(['forbidden']);
                        return throwError("You are not allowed to access this resource");
                    }
                    else if(err.status === 500) {
                        const errorMessageRegex = /System.Exception:\s([^]+?)\s+at/;
                        const matches = err.error.match(errorMessageRegex);
                        if (matches && matches.length > 1) {
                            const errorMessage = matches[1];
                            this.toastr.error(errorMessage, 'Error');
                            return throwError(errorMessage);
                          }
                        return throwError("Something went wrong");
                    }
                    this.toastr.error(err.error, 'Error');
                    return throwError("Something went wrong");
                }
            )
        );
    }
}