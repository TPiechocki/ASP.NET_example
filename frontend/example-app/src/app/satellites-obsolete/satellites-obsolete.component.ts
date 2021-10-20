import { Component, OnInit } from '@angular/core';
import { SatelliteObsolete, SatellitesObsoleteService } from '@example/webapi-contract';
import { take } from 'rxjs/operators';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-satellites-obsolete',
  templateUrl: './satellites-obsolete.component.html',
  styleUrls: ['./satellites-obsolete.component.scss']
})
export class SatellitesObsoleteComponent implements OnInit {

  satellites: SatelliteObsolete[] = [];

  constructor(
    private authService: AuthService,
    private satellitesService: SatellitesObsoleteService
    ) { }

  ngOnInit(): void {
    const result = this.satellitesService.get()

    result.pipe(take(1)).subscribe(x => {
      this.satellites = x;
    }, _ => {
      this.authService.logout();
    })
  }

}
