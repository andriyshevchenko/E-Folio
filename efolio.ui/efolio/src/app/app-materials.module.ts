import { NgModule } from '@angular/core';
import {
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatInputModule,
    MatFormFieldModule,
    MatGridListModule,
    MatTableModule,
    MatProgressSpinnerModule,
    MatCheckboxModule,
    MatSelectModule,
    MatSnackBarModule
} from '@angular/material';
import { MatCardModule } from '@angular/material/card';

@NgModule({
    exports: [
        MatToolbarModule,
        MatButtonModule,
        MatSidenavModule,
        MatIconModule,
        MatListModule,
        MatInputModule,
        MatFormFieldModule,
        MatCardModule,
        MatGridListModule,
        MatSnackBarModule,
        MatSelectModule,
        MatCheckboxModule,
        MatProgressSpinnerModule,
        MatTableModule
    ]
})
export class AppMaterialsModule { }
