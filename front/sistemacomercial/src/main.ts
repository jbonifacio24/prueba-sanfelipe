import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter, Routes } from '@angular/router';
import { App } from './app/app';
import { CompraComponent } from './app/compra/compra.component';
import { VentaComponent } from './app/venta/venta.component';

const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'compra' },
  { path: 'compra', component: CompraComponent },
  { path: 'venta', component: VentaComponent }
];

bootstrapApplication(App, {
  providers: [
    provideRouter(routes)
  ]
})
  .catch(err => console.error(err));
