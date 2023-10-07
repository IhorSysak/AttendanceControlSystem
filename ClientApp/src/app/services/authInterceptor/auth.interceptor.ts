import { HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(private router: Router) { }

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
                    console.log(err.status);
                    if(err.status === 401) {
                        console.log("You must be logged in");
                    }
                    else if(err.status === 403) {
                        console.log("Forbidden");
                    }
                    return throwError("Something went wrong");
                }
            )
        );
    }
}