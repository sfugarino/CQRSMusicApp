import { createFeatureSelector, createSelector } from '@ngrx/store';
import { ArtistsState } from './artist.reducers';

export const selectArtistsState =
  createFeatureSelector<ArtistsState>('artists');

export const selectArtistsLoading = createSelector(
  selectArtistsState,
  ({ loading }) => loading
);

export const selectArtists = createSelector(
  selectArtistsState,
  ({ artists }) => artists
);

export const selectArtistsErrorMessage = createSelector(
  selectArtistsState,
  ({ errorMessage }) => errorMessage
);

export const selectArtistsTotal = createSelector(selectArtists, sumArtists);
